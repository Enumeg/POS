using System.Collections.Generic;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using System.Threading.Tasks;
using POS.Domain.Enums;
using System.Linq;
using POS.Domain.Models;

namespace POS.Domain.Services
{
    public interface IStockService : IInitializer
    {
        Task<bool> UpdateStock(Stock stock, Operation operation, bool saveChanges = false);
        Task<decimal> GetStock(int productId, int pointId);
        Task<List<StockModel>> GetStocks(int? productId = null, int? pointId = null);
    }



    public class StockService : ServicesBase, IStockService
    {
        async Task<decimal> IStockService.GetStock(int productId, int pointId)
        {
            var stock = await Context.Stocks.SingleOrDefaultAsync(s => s.PointId == pointId && s.ProductId == productId);
            return stock?.Amount ?? 0;
        }
        async Task<List<StockModel>> IStockService.GetStocks(int? productId, int? pointId)
        {
            var stocks = Context.Stocks.Include(s => s.Product).Include(s => s.Product.Unit).Include(s => s.Product.Category).Include(s => s.Point).AsQueryable();
            if (productId.HasValue)
                stocks = stocks.Where(s => s.ProductId == productId);
            if (pointId.HasValue)
                stocks = stocks.Where(s => s.PointId == pointId);
            return (await stocks.ToListAsync()).Select(s => new StockModel(s)).ToList();
        }

        async Task<bool> IStockService.UpdateStock(Stock stock, Operation operation, bool saveChanges)
        {
            var amount = stock.Amount * (operation == Operation.Put ? 1 : -1);
            var oldStock = await Context.Stocks.SingleOrDefaultAsync(s => s.PointId == stock.PointId && s.ProductId == stock.ProductId);
            if (oldStock != null)
                oldStock.Amount += amount;
            else
            {
                stock.Amount = amount;
                Context.Stocks.Add(stock);
            }

            return !saveChanges || await Context.SaveChangesAsync() > 0;
        }
    }

}

