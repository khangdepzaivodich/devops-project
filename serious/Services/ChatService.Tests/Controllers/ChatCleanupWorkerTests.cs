using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChatService.ChatAPI;
using ChatService.ChatAPI.Models;
using ChatService.ChatAPI.Services;
using ChatService.ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ChatService.Tests.Controllers
{
    public class ChatCleanupWorkerTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldProcessIdleSessions_AndStopOnCancellation()
        {
            // Arrange
            var mockProvider = new Mock<IServiceProvider>();
            var mockScope = new Mock<IServiceScope>();
            var mockScopeFactory = new Mock<IServiceScopeFactory>();
            var mockScopeProvider = new Mock<IServiceProvider>();

            mockProvider.Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                        .Returns(mockScopeFactory.Object);
            mockScopeFactory.Setup(x => x.CreateScope())
                            .Returns(mockScope.Object);
            mockScope.Setup(x => x.ServiceProvider)
                     .Returns(mockScopeProvider.Object);

            var mockMongo = new Mock<IChatMongoService>();
            var mockHubContext = new Mock<IHubContext<ChatHub>>();
            var mockClients = new Mock<IHubClients>();
            var mockClientProxy = new Mock<IClientProxy>();

            mockHubContext.Setup(h => h.Clients).Returns(mockClients.Object);
            mockClients.Setup(c => c.Group(It.IsAny<string>())).Returns(mockClientProxy.Object);

            mockScopeProvider.Setup(x => x.GetService(typeof(IChatMongoService)))
                             .Returns(mockMongo.Object);
            mockScopeProvider.Setup(x => x.GetService(typeof(IHubContext<ChatHub>)))
                             .Returns(mockHubContext.Object);

            // Mock idle guest sessions
            var guestSession = new PhienTroChuyen { Id = Guid.NewGuid(), ClientType = "GUEST" };
            mockMongo.Setup(m => m.GetIdleGuestSessionsAsync(It.IsAny<TimeSpan>()))
                     .ReturnsAsync(new List<PhienTroChuyen> { guestSession });
            mockMongo.Setup(m => m.XoaPhienChatAsync(guestSession.Id))
                     .ReturnsAsync(true);

            // Mock idle assigned sessions
            var idleSession = new PhienTroChuyen { Id = Guid.NewGuid(), TrangThai = "ASSIGNED" };
            mockMongo.Setup(m => m.GetIdleSessionsAsync(It.IsAny<TimeSpan>()))
                     .ReturnsAsync(new List<PhienTroChuyen> { idleSession });
            mockMongo.Setup(m => m.CapNhatTrangThaiPhienAsync(idleSession.Id, "CLOSED", "ASSIGNED"))
                     .ReturnsAsync(true);

            var mockLogger = new Mock<ILogger<ChatCleanupWorker>>();

            var worker = new ChatCleanupWorker(mockProvider.Object, mockLogger.Object);
            var cts = new CancellationTokenSource();

            // Act
            var runTask = worker.StartAsync(cts.Token);
            
            // Allow brief moment for ExecuteAsync to call DoWorkAsync
            await Task.Delay(100);
            cts.Cancel();
            await runTask;

            // Assert
            mockMongo.Verify(m => m.GetIdleGuestSessionsAsync(It.IsAny<TimeSpan>()), Times.AtLeastOnce);
            mockMongo.Verify(m => m.XoaPhienChatAsync(guestSession.Id), Times.AtLeastOnce);
            mockMongo.Verify(m => m.GetIdleSessionsAsync(It.IsAny<TimeSpan>()), Times.AtLeastOnce);
            mockMongo.Verify(m => m.CapNhatTrangThaiPhienAsync(idleSession.Id, "CLOSED", "ASSIGNED"), Times.AtLeastOnce);
        }
    }
}
