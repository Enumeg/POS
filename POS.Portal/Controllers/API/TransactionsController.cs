using POS.Domain.Enums;
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
        private readonly IStockService _stockService;
        private readonly IIncomesService _incomesService;

        public TransactionsController(ITransactionsService transactionsService, IStockService stockService, ISettingsService settingsService, IIncomesService incomesService)
        {
            var context = ContextCache.GetPosContext();
            _transactionsServices = transactionsService;
            _stockService = stockService;
            _settingsService = settingsService;
            _incomesService = incomesService;
            _transactionsServices.Initialize(context);
            _stockService.Initialize(context);
            _settingsService.Initialize(context);
        }
        [HttpGet]
        [Route("api/Transactions/NewSales")]
        public async Task<IHttpActionResult> GetNewSales()
        {
            var setting = _settingsService.GetSettings();
            var count = setting == null || setting.SalesPerYear == 0 ? 5 : setting.SalesPerYear;
            return Ok(await _transactionsServices.GetNewSales(count));
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