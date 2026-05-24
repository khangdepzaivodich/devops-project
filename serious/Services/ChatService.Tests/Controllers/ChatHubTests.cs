using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChatService.ChatAPI;
using ChatService.ChatAPI.Models;
using ChatService.ChatAPI.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Xunit;

namespace ChatService.Tests.Controllers
{
    public class ChatHubTests
    {
        private readonly Mock<IChatMongoService> _mockMongo;
        private readonly Mock<IChatRedisService> _mockRedis;
        private readonly Mock<IHubCallerClients> _mockClients;
        private readonly Mock<IGroupManager> _mockGroups;
        private readonly Mock<HubCallerContext> _mockContext;
        private readonly Mock<IClientProxy> _mockClientProxy;
        private readonly ChatHub _hub;

        public ChatHubTests()
        {
            _mockMongo = new Mock<IChatMongoService>();
            _mockRedis = new Mock<IChatRedisService>();
            _mockClients = new Mock<IHubCallerClients>();
            _mockGroups = new Mock<IGroupManager>();
            _mockContext = new Mock<HubCallerContext>();
            _mockClientProxy = new Mock<IClientProxy>();

            _hub = new ChatHub(_mockMongo.Object, _mockRedis.Object)
            {
                Clients = _mockClients.Object,
                Groups = _mockGroups.Object,
                Context = _mockContext.Object
            };

            _mockContext.Setup(c => c.ConnectionId).Returns("conn-1");
            _mockClients.Setup(c => c.Group(It.IsAny<string>())).Returns(_mockClientProxy.Object);
            _mockClients.Setup(c => c.All).Returns(_mockClientProxy.Object);
            _mockClientProxy.Setup(c => c.SendCoreAsync(It.IsAny<string>(), It.IsAny<object?[]?>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            _mockGroups.Setup(g => g.AddToGroupAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            _mockGroups.Setup(g => g.RemoveFromGroupAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        }

        [Fact]
        public void ChatHub_Constructor_ShouldInitializeSuccessfully()
        {
            _hub.Should().NotBeNull();
        }

        [Fact]
        public async Task RegisterStaff_ShouldRegisterOnline_AndJoinAdminGroup()
        {
            // Arrange
            var staffId = "staff-1";
            var staffName = "Staff Name";
            var staffAvatar = "avatar-url";
            _mockRedis.Setup(r => r.RegisterStaffOnlineAsync(staffId)).Returns(Task.CompletedTask);
            _mockRedis.Setup(r => r.SetStaffNameAsync(staffId, staffName)).Returns(Task.CompletedTask);
            _mockRedis.Setup(r => r.SetStaffAvatarAsync(staffId, staffAvatar)).Returns(Task.CompletedTask);

            // Act
            await _hub.RegisterStaff(staffId, staffName, staffAvatar);

            // Assert
            _mockRedis.Verify(r => r.RegisterStaffOnlineAsync(staffId), Times.Once);
            _mockRedis.Verify(r => r.SetStaffNameAsync(staffId, staffName), Times.Once);
            _mockRedis.Verify(r => r.SetStaffAvatarAsync(staffId, staffAvatar), Times.Once);
            _mockGroups.Verify(g => g.AddToGroupAsync("conn-1", "AdminGroup", default), Times.Once);
        }

        [Fact]
        public async Task CreateNewChatSession_ShouldCreate_ForGuest()
        {
            // Arrange
            var userId = Guid.Empty.ToString();
            _mockRedis.Setup(r => r.GetNextGuestNumberWithDateAsync(It.IsAny<string>())).ReturnsAsync(1);
            _mockMongo.Setup(m => m.TaoPhienAsync(It.IsAny<PhienTroChuyen>())).Returns(Task.CompletedTask);

            // Act
            var result = await _hub.CreateNewChatSession(userId, "GUEST");

            // Assert
            result.Should().NotBeNullOrEmpty();
            _mockMongo.Verify(m => m.TaoPhienAsync(It.Is<PhienTroChuyen>(p => p.ClientType == "GUEST" && p.HoTen == "Khách #1")), Times.Once);
            _mockGroups.Verify(g => g.AddToGroupAsync("conn-1", result, default), Times.Once);
        }

        [Fact]
        public async Task JoinChatSession_ShouldJoinGroup_AndReopenIfClosed()
        {
            // Arrange
            var phienId = Guid.NewGuid();
            var phien = new PhienTroChuyen { Id = phienId, TrangThai = "CLOSED" };
            _mockMongo.Setup(m => m.GetPhienByIdAsync(phienId)).ReturnsAsync(phien);
            _mockMongo.Setup(m => m.CapNhatTrangThaiPhienAsync(phienId, "WAITING", null)).ReturnsAsync(true);

            // Act
            await _hub.JoinChatSession(phienId.ToString());

            // Assert
            _mockGroups.Verify(g => g.AddToGroupAsync("conn-1", phienId.ToString(), default), Times.Once);
            _mockMongo.Verify(m => m.CapNhatTrangThaiPhienAsync(phienId, "WAITING", null), Times.Once);
        }

        [Fact]
        public async Task MarkAsRead_ShouldResetUnread()
        {
            // Arrange
            var phienId = Guid.NewGuid();
            _mockMongo.Setup(m => m.ResetUnreadAsync(phienId)).Returns(Task.CompletedTask);

            // Act
            await _hub.MarkAsRead(phienId.ToString());

            // Assert
            _mockMongo.Verify(m => m.ResetUnreadAsync(phienId), Times.Once);
        }

        [Fact]
        public async Task UpgradeSession_ShouldMapToUser_AndUpgradePhien()
        {
            // Arrange
            var phienId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var hoTen = "Real Name";
            _mockRedis.Setup(r => r.MapSessionToUserAsync(phienId.ToString(), userId.ToString(), hoTen)).Returns(Task.CompletedTask);
            _mockMongo.Setup(m => m.UpgradePhienAsync(phienId, userId, hoTen, null)).Returns(Task.CompletedTask);

            // Act
            await _hub.UpgradeSession(phienId.ToString(), userId.ToString(), hoTen);

            // Assert
            _mockRedis.Verify(r => r.MapSessionToUserAsync(phienId.ToString(), userId.ToString(), hoTen), Times.Once);
            _mockMongo.Verify(m => m.UpgradePhienAsync(phienId, userId, hoTen, null), Times.Once);
        }

        [Fact]
        public async Task LeaveChatSession_ShouldRemoveFromGroup()
        {
            // Arrange
            var phienId = Guid.NewGuid();

            // Act
            await _hub.LeaveChatSession(phienId.ToString());

            // Assert
            _mockGroups.Verify(g => g.RemoveFromGroupAsync("conn-1", phienId.ToString(), default), Times.Once);
        }

        [Fact]
        public async Task SendMessage_ShouldSaveAndBroadcast()
        {
            // Arrange
            var phienId = Guid.NewGuid();
            var phien = new PhienTroChuyen { Id = phienId, TrangThai = "ACTIVE", StaffID = "staff-1" };
            var message = new HoiThoai
            {
                MaPhien = phienId,
                SenderID = Guid.NewGuid(),
                SenderType = "USER",
                NoiDung = "Hello"
            };

            _mockMongo.Setup(m => m.GetPhienByIdAsync(phienId)).ReturnsAsync(phien);
            _mockRedis.Setup(r => r.GetSessionMappingAsync(phienId.ToString())).ReturnsAsync((null, null));
            _mockMongo.Setup(m => m.GuiTinNhanAsync(message)).Returns(Task.CompletedTask);

            // Act
            await _hub.SendMessage(message);

            // Assert
            _mockMongo.Verify(m => m.GuiTinNhanAsync(message), Times.Once);
        }

        [Fact]
        public async Task AssignSession_ShouldAssignStaff_AndSendUpdates()
        {
            // Arrange
            var phienId = Guid.NewGuid();
            var staffId = "staff-1";
            var staffName = "Staff";
            _mockRedis.Setup(r => r.GetStaffNameAsync(staffId)).ReturnsAsync(staffName);
            _mockMongo.Setup(m => m.CapNhatThongTinStaffPhienAsync(phienId, staffId, staffName, null)).Returns(Task.CompletedTask);
            _mockMongo.Setup(m => m.CapNhatTrangThaiPhienAsync(phienId, "ASSIGNED", null)).ReturnsAsync(true);

            // Act
            await _hub.AssignSession(phienId.ToString(), staffId);

            // Assert
            _mockMongo.Verify(m => m.CapNhatThongTinStaffPhienAsync(phienId, staffId, staffName, null), Times.Once);
            _mockMongo.Verify(m => m.CapNhatTrangThaiPhienAsync(phienId, "ASSIGNED", null), Times.Once);
        }

        [Fact]
        public async Task CloseSession_ShouldClose_AndNotify()
        {
            // Arrange
            var phienId = Guid.NewGuid();
            _mockMongo.Setup(m => m.CapNhatTrangThaiPhienAsync(phienId, "CLOSED", null)).ReturnsAsync(true);

            // Act
            await _hub.CloseSession(phienId.ToString(), "staff-1");

            // Assert
            _mockMongo.Verify(m => m.CapNhatTrangThaiPhienAsync(phienId, "CLOSED", null), Times.Once);
        }

        [Fact]
        public async Task ReopenSession_ShouldReopen_AndNotify()
        {
            // Arrange
            var phienId = Guid.NewGuid();
            _mockMongo.Setup(m => m.CapNhatTrangThaiPhienAsync(phienId, "ASSIGNED", "CLOSED")).ReturnsAsync(true);

            // Act
            await _hub.ReopenSession(phienId.ToString(), "staff-1");

            // Assert
            _mockMongo.Verify(m => m.CapNhatTrangThaiPhienAsync(phienId, "ASSIGNED", "CLOSED"), Times.Once);
        }
    }
}
