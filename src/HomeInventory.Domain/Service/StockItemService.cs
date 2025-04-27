using HomeInventory.Domain.Integration;
using HomeInventory.Domain.Model;

namespace HomeInventory.Domain.Service;

public class StockItemService(IStockItemRepository stockItemRepository) : IStockItemService
{
    public async Task<StockItem> AddStockItem(AddStockItemRequest request)
    {
        var stockItem = new StockItem
        {
            Id = request.Id,
            Name = request.Name,
            Sku = request.Sku,
            InventoryCount = request.InventoryCount
        };

        await stockItemRepository.Create(stockItem);
        return stockItem;
    }
    
    public Task<List<StockItem>> GetStockItems()
    {
        return stockItemRepository.GetStockItems(); 
    }
    
    public Task<StockItem?> FindBySku(Sku sku)
    {
        return stockItemRepository.SingleOrDefault(sku);
    }

    public Task<List<StockItem>> GetLowInventoryItems()
    {
        return stockItemRepository.GetLowInventoryStockItems();
    }
}