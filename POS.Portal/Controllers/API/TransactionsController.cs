using POS.Domain.Enums;
using POS.Domain.Helpers;
using POS.Domain.Interfaces;
using POS.Domain.Services;
using POS.Portal.Helpers;
using POS.Resources;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Transaction = POS.Domain.Entities.Transaction;

namespace POS.Portal.Controllers.API
{
    public class TransactionsController : ApiController
    {
        private readonly ITransactionsService _transactionsServices;
        private readonly ISettingsService _settingsService;

        public TransactionsController(ITransactionsService transactionsService, ISettingsService settingsService)
        {
            _transactionsServices = transactionsService;
            _settingsService = settingsService;
        }
        [HttpGet]
        [Route("api/Transactions/NewSales")]
        public async Task<IHttpActionResult> GetNewSales()
        {
            return Ok(await GetNewNumber(TransactionType.Sale));
        }
        [HttpGet]
        [Route("api/Transactions/NewPurchase")]
        public async Task<IHttpActionResult> GetNewPurchase()
        {
            return Ok(await GetNewNumber(TransactionType.Purchase));
        }
        [ResponseType(typeof(Transaction))]
        public async Task<IHttpActionResult> PostTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!User.IsInRole(Roles.CanChangeSafe) && transaction.SafeId != CookieHelper.SafeId)
                    return BadRequest(Messages.SafeChanged);

                transaction.ShiftId = CookieHelper.ShiftId;
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

        private async Task<string> GetNewNumber(TransactionType transactionType)
        {
            var setting = _settingsService.GetSettings();
            var count = setting == null || setting.SalesPerYear == 0 ? 5 : setting.SalesPerYear;
            return await _transactionsServices.GetNewSales(count, transactionType);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transactionsServices.Dispose();
            }
            base.Dispose(disposing);
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



    }
}