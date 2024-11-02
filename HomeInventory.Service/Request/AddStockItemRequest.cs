using HomeInventory.Model;

namespace HomeInventory.Service.Request;

public record AddStockItemRequest(
    string Name,
    Sku Sku,
    int InventoryCount
);