using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderingService.Ordering.API.OrderingControllers;
using OrderingService.Ordering.API.OrderingServices.Interfaces;
using OrderingService.Ordering.API.DTOs;
using Xunit;

namespace OrderingService.Tests.Controllers
{
    public class DonHangControllerTests
    {
        private readonly Mock<IDonHangService> _mockService;
        private readonly DonHangController _controller;

        public DonHangControllerTests()
        {
            _mockService = new Mock<IDonHangService>();
            _controller = new DonHangController(_mockService.Object);

            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim(ClaimTypes.Role, "USER")
            }, "TestAuthentication"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };
        }

        private void SetAdminUser()
        {
            var adminClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "adminuser"),
                new Claim(ClaimTypes.Role, "ADMIN")
            }, "TestAuthentication"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = adminClaims }
            };
        }

        [Fact]
        public async Task CreateDonHang_ShouldReturnCreated_WhenSuccessful()
        {
            // Arrange
            var request = new CreateDonHangRequest
            {
                MaTK = Guid.NewGuid(),
                HoTen = "Khang Nguyen",
                SoDienThoai = "0987654321",
                DiaChiGiaoHang = "123 Street",
                ChiTietDonHangs = new List<CreateChiTietDonHangRequest>
                {
                    new CreateChiTietDonHangRequest { MaCTSP = Guid.NewGuid(), SoLuong = 1, Gia_LuuTru = 100 }
                }
            };
            var expectedDto = new DonHangDto
            {
                MaDH = Guid.NewGuid(),
                MaTK = request.MaTK,
                HoTen = request.HoTen,
                SoDienThoai = request.SoDienThoai,
                DiaChiGiaoHang = request.DiaChiGiaoHang,
                TongTien = 100,
                TrangThaiDH = "PENDING",
                NgayDat = DateTime.UtcNow
            };

            _mockService.Setup(s => s.CreateDonHangAsync(request)).ReturnsAsync(expectedDto);

            // Act
            var result = await _controller.CreateDonHang(request);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdResult.ActionName.Should().Be(nameof(DonHangController.GetDonHangById));
            createdResult.RouteValues?["maDH"].Should().Be(expectedDto.MaDH);
            createdResult.Value.Should().Be(expectedDto);
        }

        [Fact]
        public async Task CreateDonHang_ShouldReturnBadRequest_WhenRequestIsNull()
        {
            // Act
            var result = await _controller.CreateDonHang(null!);

            // Assert
            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.Value.Should().Be("Invalid request.");
        }

        [Fact]
        public async Task CreateDonHang_ShouldReturnBadRequest_WhenMaTKIsEmpty()
        {
            // Arrange
            var request = new CreateDonHangRequest { MaTK = Guid.Empty };

            // Act
            var result = await _controller.CreateDonHang(request);

            // Assert
            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.Value.Should().Be("User is required.");
        }

        [Fact]
        public async Task CreateDonHang_ShouldReturnBadRequest_WhenCartIsEmpty()
        {
            // Arrange
            var request = new CreateDonHangRequest
            {
                MaTK = Guid.NewGuid(),
                ChiTietDonHangs = new List<CreateChiTietDonHangRequest>()
            };

            // Act
            var result = await _controller.CreateDonHang(request);

            // Assert
            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.Value.Should().Be("Cart is empty.");
        }

        [Theory]
        [InlineData("", "0987654321", "123 Street")]
        [InlineData("Khang", "", "123 Street")]
        [InlineData("Khang", "0987654321", "")]
        public async Task CreateDonHang_ShouldReturnBadRequest_WhenCustomerInfoIsMissing(string name, string phone, string address)
        {
            // Arrange
            var request = new CreateDonHangRequest
            {
                MaTK = Guid.NewGuid(),
                HoTen = name,
                SoDienThoai = phone,
                DiaChiGiaoHang = address,
                ChiTietDonHangs = new List<CreateChiTietDonHangRequest> { new CreateChiTietDonHangRequest() }
            };

            // Act
            var result = await _controller.CreateDonHang(request);

            // Assert
            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.Value.Should().Be("Customer information is required.");
        }

        [Fact]
        public async Task CreateDonHang_ShouldReturnForbidden_WhenUserIsAdmin()
        {
            // Arrange
            SetAdminUser();
            var request = new CreateDonHangRequest
            {
                MaTK = Guid.NewGuid(),
                HoTen = "Admin Client",
                SoDienThoai = "0987654321",
                DiaChiGiaoHang = "123 Street",
                ChiTietDonHangs = new List<CreateChiTietDonHangRequest> { new CreateChiTietDonHangRequest() }
            };

            // Act
            var result = await _controller.CreateDonHang(request);

            // Assert
            var forbiddenResult = result.Should().BeOfType<ObjectResult>().Subject;
            forbiddenResult.StatusCode.Should().Be(403);
            forbiddenResult.Value.Should().Be("Administrator is not allowed to place orders.");
        }

        [Fact]
        public async Task CreateDonHang_ShouldReturnInternalServerError_WhenServiceThrowsException()
        {
            // Arrange
            var request = new CreateDonHangRequest
            {
                MaTK = Guid.NewGuid(),
                HoTen = "Khang Nguyen",
                SoDienThoai = "0987654321",
                DiaChiGiaoHang = "123 Street",
                ChiTietDonHangs = new List<CreateChiTietDonHangRequest> { new CreateChiTietDonHangRequest() }
            };
            _mockService.Setup(s => s.CreateDonHangAsync(request)).ThrowsAsync(new Exception("Database connection failed"));

            // Act
            var result = await _controller.CreateDonHang(request);

            // Assert
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Database connection failed");
        }

        [Fact]
        public async Task GetDonHangById_ShouldReturnOk_WhenExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var expectedDto = new DonHangDto { MaDH = orderId, HoTen = "Khang" };
            _mockService.Setup(s => s.GetDonHangByIdAsync(orderId)).ReturnsAsync(expectedDto);

            // Act
            var result = await _controller.GetDonHangById(orderId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(expectedDto);
        }

        [Fact]
        public async Task GetDonHangById_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _mockService.Setup(s => s.GetDonHangByIdAsync(orderId)).ReturnsAsync((DonHangDto?)null);

            // Act
            var result = await _controller.GetDonHangById(orderId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetDonHangsByUserId_ShouldReturnOk_WithList()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var list = new List<DonHangDto> { new DonHangDto { MaTK = userId } };
            _mockService.Setup(s => s.GetDonHangsByUserIdAsync(userId)).ReturnsAsync(list);

            // Act
            var result = await _controller.GetDonHangsByUserId(userId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(list);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithPagedResult()
        {
            // Arrange
            var pagedResult = new PagedDonHangResult { Items = new List<DonHangDto>(), TotalCount = 0, Page = 1, PageSize = 20 };
            _mockService.Setup(s => s.GetAllDonHangsAsync(1, 20)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.GetAll(1, 20);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(pagedResult);
        }

        [Fact]
        public async Task UpdateDonHangStatus_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _mockService.Setup(s => s.UpdateDonHangStatusAsync(orderId, "SHIPPED")).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateDonHangStatus(orderId, "SHIPPED");

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateDonHangStatus_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _mockService.Setup(s => s.UpdateDonHangStatusAsync(orderId, "SHIPPED")).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateDonHangStatus(orderId, "SHIPPED");

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task SyncSalesCount_ShouldReturnOk_WhenSuccessful()
        {
            // Arrange
            _mockService.Setup(s => s.SyncSalesCountAsync()).ReturnsAsync(true);

            // Act
            var result = await _controller.SyncSalesCount();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be("Sales count synced successfully");
        }

        [Fact]
        public async Task SyncSalesCount_ShouldReturnInternalServerError_WhenFailed()
        {
            // Arrange
            _mockService.Setup(s => s.SyncSalesCountAsync()).ReturnsAsync(false);

            // Act
            var result = await _controller.SyncSalesCount();

            // Assert
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Failed to sync sales count");
        }
    }
}
