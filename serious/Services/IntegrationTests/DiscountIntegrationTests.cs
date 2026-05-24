using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DiscountService.Discount.API.DiscountServices.Interfaces;
using DiscountService.Discount.API.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace IntegrationTests
{
    public class DiscountIntegrationTests : IClassFixture<WebApplicationFactory<DiscountService.Discount.API.DiscountControllers.MaGiamGiaController>>
    {
        private readonly WebApplicationFactory<DiscountService.Discount.API.DiscountControllers.MaGiamGiaController> _factory;
        private readonly Mock<IMaGiamGiaService> _mockDiscountService;

        public DiscountIntegrationTests(WebApplicationFactory<DiscountService.Discount.API.DiscountControllers.MaGiamGiaController> factory)
        {
            TestJwtSettings.ConfigureTestEnvironment();
            _mockDiscountService = new Mock<IMaGiamGiaService>();
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IMaGiamGiaService));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddSingleton<IMaGiamGiaService>(_mockDiscountService.Object);
                });
            });
        }

        [Fact]
        public async Task GetDiscountByCode_ShouldReturnOk_WhenExists()
        {
            // Arrange
            var client = _factory.CreateClient();
            var code = "PROMO10";
            var dto = new MaGiamGiaDto { MaGG = Guid.NewGuid(), MaCode = code };
            _mockDiscountService.Setup(s => s.GetDiscountByCodeAsync(code)).ReturnsAsync(dto);

            // Act
            var response = await client.GetAsync($"/api/MaGiamGia/code/{code}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<MaGiamGiaDto>();
            result.Should().NotBeNull();
            result!.MaCode.Should().Be(code);
        }
    }
}
