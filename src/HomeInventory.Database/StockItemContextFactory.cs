using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HomeInventory.Database;

public class StockItemContextFactory : IDesignTimeDbContextFactory<StockItemContext>
{
    public StockItemContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StockItemContext>();
        var connectionString = args.Length > 0
            ? args[0]
            : throw new InvalidOperationException("Unable to determine db connection string");

        optionsBuilder.UseNpgsql(connectionString);
        return new StockItemContext(optionsBuilder.Options);
    }
}