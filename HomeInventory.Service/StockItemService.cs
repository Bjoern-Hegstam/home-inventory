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
        await stockItemContext.SaveChangesAsync();
        
        return stockItem;
    }
    
    public async Task<List<StockItem>> GetStockItems()
    {
        return await stockItemContext.StockItems.AsNoTracking().ToListAsync();
    }
    
    public async Task<StockItem?> FindBySku(Sku sku)
    {
        return await stockItemContext.StockItems.AsNoTracking().FirstOrDefaultAsync(s => s.Sku == sku);
    }

    public async Task<List<StockItem>> GetLowInventoryItems()
    {
        return await stockItemContext.StockItems.AsNoTracking()
            .Where(item => item.InventoryCount < item.DesiredCount)
            .ToListAsync();
    }
}