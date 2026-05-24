using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CatalogService.Data;
using CatalogService.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntegrationTests
{
    public class CatalogIntegrationTests : IClassFixture<WebApplicationFactory<CatalogService.CatalogControllers.DanhMucController>>
    {
        private readonly WebApplicationFactory<CatalogService.CatalogControllers.DanhMucController> _factory;

        public CatalogIntegrationTests(WebApplicationFactory<CatalogService.CatalogControllers.DanhMucController> factory)
        {
            TestJwtSettings.ConfigureTestEnvironment();
            _factory = factory;
        }

        [Fact]
        public async Task GetDanhMuc_ShouldReturnOk_WithSeededData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/DanhMuc");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var categories = await response.Content.ReadFromJsonAsync<IEnumerable<DanhMucDTO>>();
            categories.Should().NotBeNull();
        }

        [Fact]
        public async Task GetLoaiDanhMuc_ShouldReturnOk()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/LoaiDanhMuc");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var types = await response.Content.ReadFromJsonAsync<IEnumerable<LoaiDanhMucDTO>>();
            types.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSanPham_ShouldReturnOk()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/SanPham");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
