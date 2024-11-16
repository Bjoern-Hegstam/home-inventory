using HomeInventory.Model;
using HomeInventory.Service.Request;

namespace HomeInventory.Service;

public interface IStockItemService
{
    Task<StockItem> AddStockItem(AddStockItemRequest request);
    Task<List<StockItem>> GetStockItems();
    Task<StockItem?> FindBySku(Sku sku);
    
}