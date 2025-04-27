using HomeInventory.Domain.Model;

namespace HomeInventory.Domain.Integration;

public interface IStockItemRepository
{
    Task Create(StockItem stockItem);
    Task<List<StockItem>> GetStockItems();
    Task<List<StockItem>> GetLowInventoryStockItems();
    Task<StockItem?> SingleOrDefault(Sku sku);
}