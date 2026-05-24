using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BasketService.BasketAPI.Controllers;
using BasketService.BasketAPI.DTOs;
using BasketService.BasketAPI.Models;
using BasketService.BasketAPI.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BasketService.Tests.Controllers
{
    public class BasketControllerTests
    {
        private readonly Mock<IBasketService> _mockBasketService;
        private readonly Mock<ILogger<BasketController>> _mockLogger;
        private readonly Mock<ICatalogService> _mockCatalogService;
        private readonly BasketController _controller;

        public BasketControllerTests()
        {
            _mockBasketService = new Mock<IBasketService>();
            _mockLogger = new Mock<ILogger<BasketController>>();
            _mockCatalogService = new Mock<ICatalogService>();
            _controller = new BasketController(_mockBasketService.Object, _mockLogger.Object, _mockCatalogService.Object);

            // Default regular user claims
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
        public async Task GetBasket_ShouldReturnOk_WithExistingBasket()
        {
            // Arrange
            var userName = "testuser";
            var existingCart = new ShoppingCart(userName)
            {
                Items = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem { ProductId = "p1", ProductName = "Product 1", Price = 10, Quantity = 2 }
                }
            };
            _mockBasketService.Setup(s => s.GetBasketAsync(userName)).ReturnsAsync(existingCart);

            // Act
            var result = await _controller.GetBasket(userName);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedCart = okResult.Value.Should().BeOfType<ShoppingCart>().Subject;
            returnedCart.UserName.Should().Be(userName);
            returnedCart.Items.Should().HaveCount(1);
            returnedCart.TotalPrice.Should().Be(20);
        }

        [Fact]
        public async Task GetBasket_ShouldReturnOk_WithNewBasket_WhenNotFound()
        {
            // Arrange
            var userName = "newuser";
            _mockBasketService.Setup(s => s.GetBasketAsync(userName)).ReturnsAsync((ShoppingCart?)null);

            // Act
            var result = await _controller.GetBasket(userName);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedCart = okResult.Value.Should().BeOfType<ShoppingCart>().Subject;
            returnedCart.UserName.Should().Be(userName);
            returnedCart.Items.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetBasket_ShouldReturnBadRequest_WhenUserNameIsNullOrEmpty(string? userName)
        {
            // Act
            var result = await _controller.GetBasket(userName!);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateBasket_ShouldReturnOk_WithUpdatedBasket_AndEnrichProductDetails()
        {
            // Arrange
            var cart = new ShoppingCart("testuser")
            {
                Items = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem { ProductId = "p1", Quantity = 2 }
                }
            };
            var request = new UpdateCartRequest { Cart = cart };
            var mockProduct = new CatalogItem { Id = "p1", Name = "Catalog Product 1", Price = 15, ImageUrl = "http://img" };
            
            _mockCatalogService.Setup(s => s.GetProductAsync("p1")).ReturnsAsync(mockProduct);
            _mockBasketService.Setup(s => s.UpdateBasketAsync(It.IsAny<ShoppingCart>())).Returns<ShoppingCart>(c => Task.FromResult<ShoppingCart?>(c));

            // Act
            var result = await _controller.UpdateBasket(request);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedCart = okResult.Value.Should().BeOfType<ShoppingCart>().Subject;
            returnedCart.Items[0].ProductName.Should().Be("Catalog Product 1");
            returnedCart.Items[0].Price.Should().Be(15);
            returnedCart.Items[0].ImageUrl.Should().Be("http://img");
            returnedCart.TotalPrice.Should().Be(30);
        }

        [Fact]
        public async Task UpdateBasket_ShouldReturnForbidden_WhenUserIsAdmin()
        {
            // Arrange
            SetAdminUser();
            var cart = new ShoppingCart("adminuser");
            var request = new UpdateCartRequest { Cart = cart };

            // Act
            var result = await _controller.UpdateBasket(request);

            // Assert
            var objectResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("Administrator is not allowed to update basket.");
        }

        [Fact]
        public async Task UpdateBasket_ShouldReturnBadRequest_WhenRequestOrCartIsNull()
        {
            // Act
            var resultNullRequest = await _controller.UpdateBasket(null!);
            var resultNullCart = await _controller.UpdateBasket(new UpdateCartRequest { Cart = null! });

            // Assert
            resultNullRequest.Result.Should().BeOfType<BadRequestObjectResult>();
            resultNullCart.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteBasket_ShouldReturnOk()
        {
            // Arrange
            var userName = "testuser";
            _mockBasketService.Setup(s => s.DeleteBasketAsync(userName)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteBasket(userName);

            // Assert
            result.Should().BeOfType<OkResult>();
            _mockBasketService.Verify(s => s.DeleteBasketAsync(userName), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task DeleteBasket_ShouldReturnBadRequest_WhenUserNameIsNullOrEmpty(string? userName)
        {
            // Act
            var result = await _controller.DeleteBasket(userName!);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
