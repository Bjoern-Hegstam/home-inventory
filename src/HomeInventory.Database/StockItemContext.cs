using System.Reflection;
using HomeInventory.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Database;

public class StockItemContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<StockItem> StockItems { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}