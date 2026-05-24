using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiscountService.Discount.API.DiscountControllers;
using DiscountService.Discount.API.DiscountServices.Interfaces;
using DiscountService.Discount.API.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DiscountService.Tests.Controllers
{
    public class MaGiamGiaControllerTests
    {
        private readonly Mock<IMaGiamGiaService> _mockService;
        private readonly MaGiamGiaController _controller;

        public MaGiamGiaControllerTests()
        {
            _mockService = new Mock<IMaGiamGiaService>();
            _controller = new MaGiamGiaController(_mockService.Object);
        }

        [Fact]
        public async Task GetDiscounts_ShouldReturnOk_WithPagedResult()
        {
            // Arrange
            var request = new DiscountPaginationRequest();
            var expectedResult = new PagedResult<MaGiamGiaDto>
            {
                Items = new List<MaGiamGiaDto> { new MaGiamGiaDto { MaGG = Guid.NewGuid(), MaCode = "PROMO10" } },
                TotalCount = 1,
                PageNumber = 1,
                PageSize = 10
            };
            _mockService.Setup(s => s.GetDiscountsAsync(request)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetDiscounts(request);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedResult = okResult.Value.Should().BeOfType<PagedResult<MaGiamGiaDto>>().Subject;
            returnedResult.TotalCount.Should().Be(1);
        }

        [Fact]
        public async Task GetDiscountById_ShouldReturnOk_WhenExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expected = new MaGiamGiaDto { MaGG = id, MaCode = "PROMO10" };
            _mockService.Setup(s => s.GetDiscountByIdAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetDiscountById(id);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returned = okResult.Value.Should().BeOfType<MaGiamGiaDto>().Subject;
            returned.MaGG.Should().Be(id);
        }

        [Fact]
        public async Task GetDiscountById_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetDiscountByIdAsync(id)).ReturnsAsync((MaGiamGiaDto?)null);

            // Act
            var result = await _controller.GetDiscountById(id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetDiscountByCode_ShouldReturnOk_WhenExists()
        {
            // Arrange
            var code = "PROMO10";
            var expected = new MaGiamGiaDto { MaGG = Guid.NewGuid(), MaCode = code };
            _mockService.Setup(s => s.GetDiscountByCodeAsync(code)).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetDiscountByCode(code);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returned = okResult.Value.Should().BeOfType<MaGiamGiaDto>().Subject;
            returned.MaCode.Should().Be(code);
        }

        [Fact]
        public async Task GetDiscountByCode_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            var code = "PROMO10";
            _mockService.Setup(s => s.GetDiscountByCodeAsync(code)).ReturnsAsync((MaGiamGiaDto?)null);

            // Act
            var result = await _controller.GetDiscountByCode(code);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateDiscount_ShouldReturnCreated_WhenSuccessful()
        {
            // Arrange
            var request = new CreateMaGiamGiaRequest { MaCode = "PROMO10", Loai = "PhanTram", SoTien = 10 };
            var created = new MaGiamGiaDto { MaGG = Guid.NewGuid(), MaCode = "PROMO10" };
            _mockService.Setup(s => s.CreateDiscountAsync(request)).ReturnsAsync(created);

            // Act
            var result = await _controller.CreateDiscount(request);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdResult.ActionName.Should().Be(nameof(MaGiamGiaController.GetDiscountById));
            createdResult.RouteValues?["maGG"].Should().Be(created.MaGG);
            createdResult.Value.Should().BeOfType<MaGiamGiaDto>();
        }

        [Fact]
        public async Task CreateDiscount_ShouldReturnBadRequest_WhenInvalidOperationExceptionThrown()
        {
            // Arrange
            var request = new CreateMaGiamGiaRequest { MaCode = "PROMO10" };
            _mockService.Setup(s => s.CreateDiscountAsync(request)).ThrowsAsync(new InvalidOperationException("Code already exists"));

            // Act
            var result = await _controller.CreateDiscount(request);

            // Assert
            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.Value.Should().NotBeNull();
            badRequest.Value!.ToString().Should().Contain("Code already exists");
        }

        [Fact]
        public async Task UpdateDiscount_ShouldReturnOk_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new CreateMaGiamGiaRequest { MaCode = "PROMO10" };
            var updated = new MaGiamGiaDto { MaGG = id, MaCode = "PROMO10" };
            _mockService.Setup(s => s.UpdateDiscountAsync(id, request)).ReturnsAsync(updated);

            // Act
            var result = await _controller.UpdateDiscount(id, request);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(updated);
        }

        [Fact]
        public async Task UpdateDiscount_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new CreateMaGiamGiaRequest { MaCode = "PROMO10" };
            _mockService.Setup(s => s.UpdateDiscountAsync(id, request)).ReturnsAsync((MaGiamGiaDto?)null);

            // Act
            var result = await _controller.UpdateDiscount(id, request);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateDiscount_ShouldReturnBadRequest_WhenInvalidOperationExceptionThrown()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new CreateMaGiamGiaRequest { MaCode = "PROMO10" };
            _mockService.Setup(s => s.UpdateDiscountAsync(id, request)).ThrowsAsync(new InvalidOperationException("Validation failed"));

            // Act
            var result = await _controller.UpdateDiscount(id, request);

            // Assert
            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.Value.Should().NotBeNull();
            badRequest.Value!.ToString().Should().Contain("Validation failed");
        }

        [Fact]
        public async Task UseDiscount_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            var code = "PROMO10";
            _mockService.Setup(s => s.DecrementDiscountQuantityAsync(code, 1)).ReturnsAsync(true);

            // Act
            var result = await _controller.UseDiscount(code, 1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UseDiscount_ShouldReturnBadRequest_WhenFailed()
        {
            // Arrange
            var code = "PROMO10";
            _mockService.Setup(s => s.DecrementDiscountQuantityAsync(code, 1)).ReturnsAsync(false);

            // Act
            var result = await _controller.UseDiscount(code, 1);

            // Assert
            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.Value.Should().NotBeNull();
            badRequest.Value!.ToString().Should().Contain("Invalid code, expired, or out of quantity");
        }

        [Fact]
        public async Task UseDiscountById_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DecrementDiscountQuantityByIdAsync(id, 1)).ReturnsAsync(true);

            // Act
            var result = await _controller.UseDiscountById(id, 1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UseDiscountById_ShouldReturnBadRequest_WhenFailed()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DecrementDiscountQuantityByIdAsync(id, 1)).ReturnsAsync(false);

            // Act
            var result = await _controller.UseDiscountById(id, 1);

            // Assert
            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.Value.Should().NotBeNull();
            badRequest.Value!.ToString().Should().Contain("Invalid ID, expired, or out of quantity");
        }
    }
}
