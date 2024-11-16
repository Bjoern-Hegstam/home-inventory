using HomeInventory.ApiModel.Shared;

namespace HomeInventory.ApiModel.Request;

public class ApiAddStockItemRequest
{
    public required string Name { get; init; }
    public required ApiSku Sku { get; init; }
}