using HomeInventory.ApiModel.Request;
using HomeInventory.ApiModel.Response;
using HomeInventory.ApiModel.Shared;
using HomeInventory.Domain.Model;
using HomeInventory.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.WebApi.Controller;

[Route("api/stock-item")]
[ApiController]
public class StockItemController(IStockItemService stockItemService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ApiStockItem>> AddStockItem(ApiAddStockItemRequest apiRequest)
    {
        var request = new AddStockItemRequest
        {
            Id = new StockItemId(),
            Name = apiRequest.Name,
            Sku = new Sku(apiRequest.Sku.Value),
            InventoryCount = apiRequest.InventoryCount
        };

        StockItem stockItem = await stockItemService.AddStockItem(request);

        var apiStockItem = new ApiStockItem
        {
            Id = new ApiStockItemId(stockItem.Id.Id),
            Name = stockItem.Name,
            Sku = new ApiSku(stockItem.Sku.Value),
            InventoryCount = stockItem.InventoryCount,
            DesiredCount = stockItem.DesiredCount
        };

        return CreatedAtAction(nameof(FindBySku), new { sku = apiStockItem.Sku }, apiStockItem);
    }

    [HttpGet]
    public async Task<ActionResult<List<ApiStockItem>>> GetStockItems()
    {
        List<StockItem> stockItems = await stockItemService.GetStockItems();

        List<ApiStockItem> apiStockItems = stockItems.Select(item => new ApiStockItem
        {
            Id = new ApiStockItemId(item.Id.Id),
            Name = item.Name,
            Sku = new ApiSku(item.Sku.Value),
            InventoryCount = item.InventoryCount,
            DesiredCount = item.DesiredCount
        }).ToList();

        return Ok(apiStockItems);
    }

    [HttpGet("{sku}")]
    public async Task<ActionResult<ApiStockItem>> FindBySku(string sku)
    {
        var stockItem = await stockItemService.FindBySku(new Sku(sku));
        if (stockItem == null)
        {
            return NotFound();
        }

        var apiItem = new ApiStockItem
        {
            Id = new ApiStockItemId(stockItem.Id.Id),
            Name = stockItem.Name,
            Sku = new ApiSku(stockItem.Sku.Value),
            InventoryCount = stockItem.InventoryCount,
            DesiredCount = stockItem.DesiredCount
        };

        return Ok(apiItem);
    }
}