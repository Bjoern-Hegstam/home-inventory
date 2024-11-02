using HomeInventory.Domain.Database;
using HomeInventory.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Domain.Service;

public class StockItemService(StockItemContext stockItemContext)
{
    public async Task<StockItem?> FindBySku(Sku sku)
    {
        return await stockItemContext.StockItems.FirstOrDefaultAsync(s => s.Sku == sku);
    }
}