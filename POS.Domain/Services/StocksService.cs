using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace POS.Domain.Services
{
    public interface IStockService : IInitializer
    {
        Task<bool> UpdateStock(Stock stock, bool saveChanges = false);
        Task<decimal> GetStock(int productId, int pointId);
    }
    public class StockService : ServicesBase, IStockService
    {
        async Task<decimal> IStockService.GetStock(int productId, int pointId)
        {
            var stock = await Context.Stocks.SingleOrDefaultAsync(s => s.PointId == pointId && s.ProductId == productId);
            return stock?.Amount ?? 0;
        }

        async Task<bool> IStockService.UpdateStock(Stock stock, bool saveChanges)
        {
            var oldStock = await Context.Stocks.SingleOrDefaultAsync(s => s.PointId == stock.PointId && s.ProductId == stock.ProductId);
            if (oldStock != null)
                oldStock.Amount += stock.Amount;
            else
                Context.Stocks.Add(stock);

            return saveChanges ? (await Context.SaveChangesAsync()) > 0 : true;
        }
    }

}

