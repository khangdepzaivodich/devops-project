using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ChatService.ChatAPI.Models;
using ChatService.ChatAPI.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace IntegrationTests
{
    public class ChatIntegrationTests : IClassFixture<WebApplicationFactory<ChatService.ChatAPI.Controllers.ChatController>>
    {
        private readonly WebApplicationFactory<ChatService.ChatAPI.Controllers.ChatController> _factory;
        private readonly Mock<IChatMongoService> _mockMongo;
        private readonly Mock<IChatRedisService> _mockRedis;

        public ChatIntegrationTests(WebApplicationFactory<ChatService.ChatAPI.Controllers.ChatController> factory)
        {
            TestJwtSettings.ConfigureTestEnvironment();
            _mockMongo = new Mock<IChatMongoService>();
            _mockRedis = new Mock<IChatRedisService>();
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove existing singleton registrations
                    var descriptors = services.Where(d => d.ServiceType == typeof(IChatMongoService) || d.ServiceType == typeof(IChatRedisService)).ToList();
                    foreach (var d in descriptors)
                    {
                        services.Remove(d);
                    }

                    services.AddSingleton<IChatMongoService>(_mockMongo.Object);
                    services.AddSingleton<IChatRedisService>(_mockRedis.Object);
                });
            });
        }

        [Fact]
        public async Task GetDanhSachPhien_ShouldReturnOk_WithSessions()
        {
            // Arrange
            var client = _factory.CreateClient();
            var list = new List<PhienTroChuyen>
            {
                new PhienTroChuyen { Id = Guid.NewGuid(), TrangThai = "ACTIVE" }
            };
            _mockMongo.Setup(s => s.GetDanhSachPhienAsync()).ReturnsAsync(list);

            // Act
            var response = await client.GetAsync("/api/Chat/chat-sessions");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<List<PhienTroChuyen>>();
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }
    }
}
