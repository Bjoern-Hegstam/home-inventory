using HomeInventory.Database;
using HomeInventory.Model;
using HomeInventory.Service.Request;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Service;

public class StockItemService(StockItemContext stockItemContext) : IStockItemService
{
    public async Task<StockItem> AddStockItem(AddStockItemRequest request)
    {
        var stockItem = new StockItem
        {
            Id = new StockItemId(),
            Name = request.Name,
            Sku = request.Sku,
            InventoryCount = request.InventoryCount
        };
        await stockItemContext.StockItems.AddAsync(stockItem);

        return stockItem;
    }
    
    public async Task<List<StockItem>> GetStockItems()
    {
        return await stockItemContext.StockItems.ToListAsync();
    }
    
    public async Task<StockItem?> FindBySku(Sku sku)
    {
        return await stockItemContext.StockItems.FirstOrDefaultAsync(s => s.Sku == sku);
    }
}