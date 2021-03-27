using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Enums;
using POS.Domain.Infrastructure;

namespace POS.Domain.Services
{
    public interface ITransactionsService : IDisposable
    {
        Task<bool> AddTransaction(Transaction transaction);
        Task<string> GetNewSales(int salesPerYear, TransactionType transactionType);
    }
    public class TransactionsService : ServicesBase, ITransactionsService
    {

        private readonly IStockService _stockService;
        private readonly IIncomesService _incomesService;
        private readonly ISafeService _safeService;
        public TransactionsService(PosContext context, IIncomesService incomesService, IStockService stockService, ISafeService safeService) : base(context)
        {
            _incomesService = incomesService;
            _stockService = stockService;
            _safeService = safeService;
        }


        async Task<bool> ITransactionsService.AddTransaction(Transaction transaction)
        {
            var operation =
                transaction.TransactionType == TransactionType.Purchase ||
                transaction.TransactionType == TransactionType.SaleBack
                    ? Operation.Put
                    : Operation.Take;
            foreach (var item in transaction.Details)
            {
                item.TenantId = Context.TenantId;
                await _stockService.UpdateStock(new Stock { Amount = item.Amount, ProductId = item.ProductId, PointId = transaction.PointId }, operation);
            }

            if ((transaction.TransactionType == TransactionType.Sale || transaction.TransactionType == TransactionType.PurchaseBack) && transaction.Paid > 0)
            {
                await _incomesService.AddIncome(new Income
                {
                    AccountType = transaction.TransactionType == TransactionType.Sale ? AccountType.Sales : AccountType.PurchaesBack,
                    PersonId = transaction.PersonId,
                    Date = transaction.Date,
                    Value = transaction.Paid,
                    Description = transaction.Number,
                }, false);
                await _safeService.UpdateSafe(
                    new Safe { Id = transaction.SafeId ?? 0, CurrentBalance = (double)transaction.Paid },
                    operation == Operation.Put ? Operation.Take : Operation.Put);
            }
            return await CrudService.Add(transaction, p => p.Number == transaction.Number && p.PersonId == transaction.PersonId && p.TransactionType != transaction.TransactionType);
        }

        async Task<string> ITransactionsService.GetNewSales(int salesPerYear, TransactionType transactionType)
        {
            var year = DateTime.Now.Year;
            var sales = CrudService.Get<Transaction>(s => s.TransactionType == transactionType && s.Date.Year == year);
            if (await sales.AnyAsync())
                return (int.Parse(sales.Max(s => s.Number)) + 1).ToString();
            return year.ToString().PadRight(salesPerYear + 4, '0') + "1";
        }
    }
}
