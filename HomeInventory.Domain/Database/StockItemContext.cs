using HomeInventory.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Domain.Database;

public class StockItemContext : DbContext
{
    public DbSet<StockItem> StockItems { get; init; }
}