using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OrderingService.Ordering.API.Data;
using OrderingService.Ordering.API.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntegrationTests
{
    public class OrderingIntegrationTests : IClassFixture<WebApplicationFactory<OrderingService.Ordering.API.OrderingControllers.DonHangController>>
    {
        private readonly WebApplicationFactory<OrderingService.Ordering.API.OrderingControllers.DonHangController> _factory;

        public OrderingIntegrationTests(WebApplicationFactory<OrderingService.Ordering.API.OrderingControllers.DonHangController> factory)
        {
            TestJwtSettings.ConfigureTestEnvironment();
            _factory = factory;
        }

        [Fact]
        public async Task GetOrders_ShouldReturnOk_WithList()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = TestJwtSettings.GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await client.GetAsync("/api/DonHang?page=1&pageSize=10");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<PagedDonHangResult>();
            result.Should().NotBeNull();
        }
    }
}
