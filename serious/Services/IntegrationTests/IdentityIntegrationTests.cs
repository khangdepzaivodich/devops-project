using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using IdentityService.Identity.API.Data;
using IdentityService.Identity.API.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntegrationTests
{
    public class IdentityIntegrationTests : IClassFixture<WebApplicationFactory<IdentityService.Identity.API.IdentityControllers.AuthController>>
    {
        private readonly WebApplicationFactory<IdentityService.Identity.API.IdentityControllers.AuthController> _factory;

        public IdentityIntegrationTests(WebApplicationFactory<IdentityService.Identity.API.IdentityControllers.AuthController> factory)
        {
            TestJwtSettings.ConfigureTestEnvironment();
            _factory = factory;
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenUserDoesNotExist()
        {
            // Arrange
            var client = _factory.CreateClient();
            var loginRequest = new LoginRequest { Email = "nonexistent@example.com", MatKhau = "wrongpassword" };

            // Act
            var response = await client.PostAsJsonAsync("/api/Auth/login", loginRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Register_ShouldCreateUserSuccessfully()
        {
            // Arrange
            var client = _factory.CreateClient();
            var registerRequest = new RegisterRequest
            {
                Email = $"test_{Guid.NewGuid()}@example.com",
                MatKhau = "password123",
                HoTen = "Integration Tester",
                SoDienThoai = "0912345678"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/Auth/register", registerRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var resultStr = await response.Content.ReadAsStringAsync();
            resultStr.Should().Contain("true");
        }
    }
}
