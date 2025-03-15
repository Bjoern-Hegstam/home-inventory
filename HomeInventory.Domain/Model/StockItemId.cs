namespace HomeInventory.Domain.Model;

public record StockItemId(Guid Id) : Identifier<StockItemId>(Id)
{
    public StockItemId() : this(Guid.NewGuid())
    {
    }
}