using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BasketService.BasketAPI.Models;
using BasketService.BasketAPI.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace IntegrationTests
{
    public class BasketIntegrationTests : IClassFixture<WebApplicationFactory<BasketService.BasketAPI.Controllers.BasketController>>
    {
        private readonly WebApplicationFactory<BasketService.BasketAPI.Controllers.BasketController> _factory;
        private readonly Mock<IBasketService> _mockBasketService;

        public BasketIntegrationTests(WebApplicationFactory<BasketService.BasketAPI.Controllers.BasketController> factory)
        {
            TestJwtSettings.ConfigureTestEnvironment();
            _mockBasketService = new Mock<IBasketService>();
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove existing registration if any
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IBasketService));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddSingleton<IBasketService>(_mockBasketService.Object);
                });
            });
        }

        [Fact]
        public async Task GetBasket_ShouldReturnOk_WithShoppingCart()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = TestJwtSettings.GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var userName = "tester";
            var cart = new ShoppingCart(userName);
            _mockBasketService.Setup(s => s.GetBasketAsync(userName)).ReturnsAsync(cart);

            // Act
            var response = await client.GetAsync($"/api/Basket/{userName}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedCart = await response.Content.ReadFromJsonAsync<ShoppingCart>();
            returnedCart.Should().NotBeNull();
            returnedCart!.UserName.Should().Be(userName);
        }
    }
}
