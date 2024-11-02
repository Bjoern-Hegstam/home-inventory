namespace HomeInventory.Domain.Model;

public class StockItem
{
    public required StockItemId StockItemId { get; init; }
    public required string Name { get; init; }
    public required Sku Sku { get; init; }
    public int InventoryCount { get; init; }
    public int DesiredCount { get; init; }

    // EF managed properties
    public int Id { get; private set; }
}