using HomeInventory.Model.Common;

namespace HomeInventory.Model;

public record StockItemId(Guid Id) : Identifier<StockItemId>(Id)
{
    public StockItemId() : this(Guid.NewGuid())
    {
    }
}