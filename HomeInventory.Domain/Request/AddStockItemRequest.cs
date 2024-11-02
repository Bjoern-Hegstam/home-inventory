using HomeInventory.Domain.Model;

namespace HomeInventory.Domain.Request;

public record AddStockItemRequest(
    string Name,
    Sku Sku,
    int InventoryCount
);