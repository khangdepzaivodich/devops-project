using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatService.ChatAPI.Controllers;
using ChatService.ChatAPI.DTOs;
using ChatService.ChatAPI.Models;
using ChatService.ChatAPI.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ChatService.Tests.Controllers
{
    public class ChatControllerTests
    {
        private readonly Mock<IChatMongoService> _mockMongoService;
        private readonly Mock<IChatRedisService> _mockRedisService;
        private readonly ChatController _controller;

        public ChatControllerTests()
        {
            _mockMongoService = new Mock<IChatMongoService>();
            _mockRedisService = new Mock<IChatRedisService>();
            _controller = new ChatController(_mockMongoService.Object, _mockRedisService.Object);
        }

        [Fact]
        public async Task GetDanhSachPhien_ShouldReturnOk_WithList()
        {
            // Arrange
            var list = new List<PhienTroChuyen>
            {
                new PhienTroChuyen { Id = Guid.NewGuid(), UserID = Guid.NewGuid(), TrangThai = "ACTIVE" }
            };
            _mockMongoService.Setup(s => s.GetDanhSachPhienAsync()).ReturnsAsync(list);

            // Act
            var result = await _controller.GetDanhSachPhien();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedList = okResult.Value.Should().BeAssignableTo<List<PhienTroChuyen>>().Subject;
            returnedList.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetPhien_ShouldReturnOk_WhenExists_WithoutFallback()
        {
            // Arrange
            var phienId = Guid.NewGuid();
            var phien = new PhienTroChuyen { Id = phienId, StaffID = "staff-1", StaffHoTen = "Staff Name" };
            _mockMongoService.Setup(s => s.GetPhienByIdAsync(phienId)).ReturnsAsync(phien);

            // Act
            var result = await _controller.GetPhien(phienId);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedPhien = okResult.Value.Should().BeOfType<PhienTroChuyen>().Subject;
            returnedPhien.Id.Should().Be(phienId);
            returnedPhien.StaffHoTen.Should().Be("Staff Name");
        }

        [Fact]
        public async Task GetPhien_ShouldReturnOk_WithFallback_WhenStaffNameMissingInDb()
        {
            // Arrange
            var phienId = Guid.NewGuid();
            var phien = new PhienTroChuyen { Id = phienId, StaffID = "staff-1", StaffHoTen = "" };
            _mockMongoService.Setup(s => s.GetPhienByIdAsync(phienId)).ReturnsAsync(phien);
            _mockRedisService.Setup(s => s.GetStaffNameAsync("staff-1")).ReturnsAsync("Redis Staff Name");
            _mockMongoService.Setup(s => s.CapNhatThongTinStaffPhienAsync(phienId, "staff-1", "Redis Staff Name", null)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.GetPhien(phienId);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedPhien = okResult.Value.Should().BeOfType<PhienTroChuyen>().Subject;
            returnedPhien.StaffHoTen.Should().Be("Redis Staff Name");
            _mockMongoService.Verify(s => s.CapNhatThongTinStaffPhienAsync(phienId, "staff-1", "Redis Staff Name", null), Times.Once);
        }

        [Fact]
        public async Task GetPhien_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            var phienId = Guid.NewGuid();
            _mockMongoService.Setup(s => s.GetPhienByIdAsync(phienId)).ReturnsAsync((PhienTroChuyen?)null);

            // Act
            var result = await _controller.GetPhien(phienId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetHoiThoai_ShouldReturnOk_WithMessages()
        {
            // Arrange
            var phienId = Guid.NewGuid();
            var list = new List<HoiThoai>
            {
                new HoiThoai { MaPhien = phienId, NoiDung = "Hello" }
            };
            _mockMongoService.Setup(s => s.GetTinNhanTheoPhienAsync(phienId)).ReturnsAsync(list);

            // Act
            var result = await _controller.GetHoiThoai(phienId);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedList = okResult.Value.Should().BeAssignableTo<List<HoiThoai>>().Subject;
            returnedList.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetLatestActiveSession_ShouldReturnOk_WithLatestSession()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var list = new List<PhienTroChuyen>
            {
                new PhienTroChuyen { Id = Guid.NewGuid(), UserID = userId, ThoiGianTao = DateTime.UtcNow.AddMinutes(-10) },
                new PhienTroChuyen { Id = Guid.NewGuid(), UserID = userId, ThoiGianTao = DateTime.UtcNow }
            };
            _mockMongoService.Setup(s => s.GetDanhSachPhienByUserIdAsync(userId)).ReturnsAsync(list);

            // Act
            var result = await _controller.GetLatestActiveSession(userId.ToString());

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedSession = okResult.Value.Should().BeOfType<PhienTroChuyen>().Subject;
            returnedSession.Id.Should().Be(list[1].Id);
        }

        [Fact]
        public async Task GetLatestActiveSession_ShouldReturnBadRequest_WhenUserIdInvalid()
        {
            // Act
            var result = await _controller.GetLatestActiveSession("invalid-guid");

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task TaoPhien_ShouldReturnOk_WithNewSession()
        {
            // Arrange
            var request = new CreateSessionRequest { UserId = Guid.NewGuid() };
            _mockMongoService.Setup(s => s.TaoPhienAsync(It.IsAny<PhienTroChuyen>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.TaoPhien(request);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            _mockMongoService.Verify(s => s.TaoPhienAsync(It.Is<PhienTroChuyen>(p => p.UserID == request.UserId && p.TrangThai == "ACTIVE")), Times.Once);
        }

        [Fact]
        public async Task GuiTinNhan_ShouldReturnOk_WithSentMessage()
        {
            // Arrange
            var request = new SendMessageRequest
            {
                MaPhien = Guid.NewGuid(),
                SenderID = Guid.NewGuid(),
                SenderType = "user",
                NoiDung = "Test msg"
            };
            _mockMongoService.Setup(s => s.GuiTinNhanAsync(It.IsAny<HoiThoai>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.GuiTinNhan(request);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            _mockMongoService.Verify(s => s.GuiTinNhanAsync(It.Is<HoiThoai>(m => m.MaPhien == request.MaPhien && m.NoiDung == request.NoiDung)), Times.Once);
        }

        [Fact]
        public async Task PingOnline_ShouldReturnOk()
        {
            // Arrange
            var request = new OnlinePingRequest { UserId = Guid.NewGuid() };
            _mockRedisService.Setup(s => s.SetUserOnlineAsync(request.UserId.ToString())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PingOnline(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _mockRedisService.Verify(s => s.SetUserOnlineAsync(request.UserId.ToString()), Times.Once);
        }

        [Fact]
        public async Task KiemTraOnline_ShouldReturnOk_WithStatus()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockRedisService.Setup(s => s.IsUserOnlineAsync(userId.ToString())).ReturnsAsync(true);

            // Act
            var result = await _controller.KiemTraOnline(userId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().NotBeNull();
            okResult.Value!.ToString().Should().Contain("IsOnline = True");
        }
    }
}
