using HomeInventory.Domain.Model;

namespace HomeInventory.Domain.Service;

public class AddStockItemRequest
{
    public required StockItemId Id { get; init; }
    public required string Name { get; init; }
    public required Sku Sku { get; init; }
    public int InventoryCount { get; init; } = 1;
}