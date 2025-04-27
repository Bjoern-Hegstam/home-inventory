using HomeInventory.Domain.Integration;
using HomeInventory.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Database;

public class EfStockItemRepository(StockItemContext stockItemContext) : IStockItemRepository
{
    public async Task Create(StockItem stockItem)
    {
        await stockItemContext.StockItems.AddAsync(stockItem);
        await stockItemContext.SaveChangesAsync();
    }

    public Task<List<StockItem>> GetStockItems()
    {
        return stockItemContext.StockItems.AsNoTracking().ToListAsync();
    }

    public Task<List<StockItem>> GetLowInventoryStockItems()
    {
        return stockItemContext.StockItems.AsNoTracking()
            .Where(item => item.InventoryCount < item.DesiredCount)
            .ToListAsync();
    }

    public Task<StockItem?> SingleOrDefault(Sku sku)
    {
        return stockItemContext.StockItems.AsNoTracking()
            .Where(item => item.Sku == sku)
            .SingleOrDefaultAsync();
    }
}