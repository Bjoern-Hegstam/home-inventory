using HomeInventory.Model;

namespace HomeInventory.Service.Request;

public class AddStockItemRequest
{
    public required string Name { get; init; }
    public required Sku Sku { get; init; }
    public int InventoryCount { get; init; } = 1;
}