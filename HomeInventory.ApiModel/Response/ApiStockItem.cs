namespace HomeInventory.ApiModel;

public class ApiStockItem
{
    public required ApiStockItemId Id { get; init; }
    public required string Name { get; init; }
    public required ApiSku Sku { get; init; }
    public int InventoryCount { get; init; }
    public int DesiredCount { get; init; }
}