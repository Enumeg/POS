using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Domain.Enums;
using POS.Domain.Interfaces;
using POS.Portal.Helpers;
using POS.Resources;
using POS.Domain.Services;
using Transaction = POS.Domain.Entities.Transaction;

namespace POS.Portal.Controllers.API
{
    public class TransactionsController : ApiController
    {
        private readonly ITransactionsService _transactionsServices;

        private readonly IStockService _stockService;

        public TransactionsController(ITransactionsService transactionsService, IStockService stockService)
        {
            var context = ContextCache.GetPosContext();
            _transactionsServices = transactionsService;
            _stockService = stockService;
            _transactionsServices.Initialize(context);
            _stockService.Initialize(context);
        }

        // GET: api/Transactions
        //public async Task<List<Transaction>> GetTransactions()
        //{
        //    return await _transactionsServices.GetAllTransactions();
        //}


        //// PUT: api/Transactions/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutTransaction(Transaction Transaction)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var result = await _transactionsServices.UpdateTransaction(Transaction);
        //        if (result == null)
        //            return NotFound();
        //        if (result == false)
        //            return BadRequest(Common.Duplicated);
        //        return Ok();
        //    }
        //    catch
        //    {
        //        return InternalServerError();
        //    }
        //}

        // POST: api/Transactions
        [ResponseType(typeof(Transaction))]
        public async Task<IHttpActionResult> PostTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                transaction.ShiftId = CookieHelper.ShiftId;
                var operation =
                    transaction.TransactionType == TransactionType.Purchase ||
                    transaction.TransactionType == TransactionType.SaleBack
                        ? Operation.Put
                        : Operation.Take;
                foreach (var item in transaction.Details)
                {
                    await _stockService.UpdateStock(new Domain.Entities.Stock { Amount = item.Amount, ProductId = item.ProductId, PointId = transaction.PointId }, operation);
                }
                var result = await _transactionsServices.AddTransaction(transaction);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Transactions/5
        //[ResponseType(typeof(Transaction))]
        //public async Task<IHttpActionResult> DeleteTransaction(int id, bool removeRelatedEntities = false)
        //{
        //    try
        //    {
        //        var result = await _transactionsServices.DeleteTransaction(id, removeRelatedEntities);
        //        if (result == false)
        //            return NotFound();
        //        if (result == null)
        //            return BadRequest();
        //        return Ok();
        //    }
        //    catch
        //    {
        //        return InternalServerError();
        //    }
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transactionsServices.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}