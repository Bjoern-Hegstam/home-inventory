using System.Net.Http.Json;
using FluentAssertions;
using HomeInventory.ApiModel.Request;
using HomeInventory.ApiModel.Response;
using HomeInventory.ApiModel.Shared;
using HomeInventory.Database;
using HomeInventory.IntegrationTest.Framework.Core;
using HomeInventory.IntegrationTest.Setup;
using HomeInventory.Model;
using HomeInventory.Service;
using HomeInventory.Service.Request;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HomeInventory.IntegrationTest;

[Collection(nameof(IntegrationTestCollection))]
public class StockItemIntegrationTest(HomeInventoryIntegrationTestFixture fixture) : IntegrationTestBase(fixture)
{
    private HttpClient HttpClient => Fixture.GetRequiredService<HomeInventoryTestHost>().HttpClient;
    
    [Fact]
    public async Task CreateStockItem()
    {
        // Arrange
        var request = new ApiAddStockItemRequest
        {
            Name = "test-name",
            Sku = new ApiSku("test-sku"),
            InventoryCount = 17
        };

        // Act
        HttpResponseMessage httpResponse = await HttpClient.PostAsJsonAsync("api/stock-item", request);

        // Assert
        httpResponse.EnsureSuccessStatusCode();

        var stockItems = Fixture.GetRequiredService<StockItemContext>()
            .StockItems
            .ToList();
        stockItems.Count.Should().Be(1);
        StockItem persistedStockItem = stockItems.First();
        
        persistedStockItem.Name.Should().Be("test-name");
        persistedStockItem.Sku.Value.Should().Be("test-sku");
        persistedStockItem.InventoryCount.Should().Be(17);
        persistedStockItem.DesiredCount.Should().Be(0);
    }
    
    [Fact]
    public async Task GetStockItems_NoneExist()
    {
        // Act
        var apiStockItems = await HttpClient.GetFromJsonAsync<List<ApiStockItem>>("api/stock-item");

        // Assert
        apiStockItems.Should().BeEmpty();
    }

    [Fact]
    public async Task GetStockItems_SomeExist()
    {
        // Arrange
        var existingStockItem = new AddStockItemRequest
        {
            Name = "test-stock-item",
            Sku = new Sku("test-sku"),
            InventoryCount = 1,
        };
        await Fixture.GetRequiredService<IStockItemService>().AddStockItem(existingStockItem);

        // Act
        var apiStockItems = await HttpClient.GetFromJsonAsync<List<ApiStockItem>>("api/stock-item");

        // Assert
        apiStockItems.Should().HaveCount(1);
        ApiStockItem apiStockItem = apiStockItems.First();
        apiStockItem.Name.Should().Be("test-stock-item");
    }
}