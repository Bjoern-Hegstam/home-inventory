using HomeInventory.Domain.Model;

namespace HomeInventory.Domain.Service;

public interface IStockItemService
{
    Task<StockItem> AddStockItem(AddStockItemRequest request);
    Task<List<StockItem>> GetStockItems();
    Task<StockItem?> FindBySku(Sku sku);
    Task<List<StockItem>> GetLowInventoryItems();
}