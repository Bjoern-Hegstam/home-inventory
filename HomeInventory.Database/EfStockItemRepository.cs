using System.Linq.Expressions;
using HomeInventory.Domain.Integration;
using HomeInventory.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Database;

public class EfStockItemRepository(StockItemContext stockItemContext) : IStockItemRepository
{
    public async Task CreateAsync(StockItem stockItem)
    {
        await stockItemContext.StockItems.AddAsync(stockItem);
        await stockItemContext.SaveChangesAsync();
    }

    public Task<List<StockItem>> GetStockItems()
    {
        return stockItemContext.StockItems.AsNoTracking().ToListAsync();
    }

    public Task<List<StockItem>> GetStockItems(Expression<Func<StockItem, bool>> predicate)
    {
        return stockItemContext.StockItems.AsNoTracking()
            .Where(item => item.InventoryCount < item.DesiredCount)
            .ToListAsync();
    }

    public Task<StockItem?> FirstOrDefaultAsync(Expression<Func<StockItem, bool>> predicate)
    {
        return stockItemContext.StockItems.AsNoTracking().FirstOrDefaultAsync(predicate);
    }
}