using System.Linq.Expressions;
using HomeInventory.Domain.Model;

namespace HomeInventory.Domain.Integration;

public interface IStockItemRepository
{
    Task CreateAsync(StockItem stockItem);
    Task<List<StockItem>> GetStockItems();
    Task<List<StockItem>> GetStockItems(Expression<Func<StockItem, bool>> predicate);
    Task<StockItem?> FirstOrDefaultAsync(Expression<Func<StockItem, bool>> predicate);
}