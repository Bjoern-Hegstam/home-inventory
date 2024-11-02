namespace HomeInventory.Domain;

public record StockItemId(Guid Id);

public record StockItem
{
    public StockItemId Id { get; init; }
    public string Name { get; init; }
    public Sku Sku { get; init; }
    public int InventoryCount { get; init; }
    public int DesiredCount { get; init; }
}