namespace HomeInventory.Model;

public class StockItem
{
    public required StockItemId Id { get; init; }
    public required string Name { get; init; }
    public required Sku Sku { get; init; }
    public int InventoryCount { get; init; }
    public int DesiredCount { get; init; }
}