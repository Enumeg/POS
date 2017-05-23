using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Domain.Interfaces;
using POS.Portal.Helpers;
using POS.Resources;
using POS.Domain.Services;
using BankAccount = POS.Domain.Entities.BankAccount;
namespace POS.Portal.Controllers.API
{
    public class BankAccountsController : ApiController
    {
        private readonly IBankAccountsService _bankAccountsService;

        public BankAccountsController(IBankAccountsService bankAccountsService)
        {
            var context = ContextCache.GetPosContext();
            _bankAccountsService = bankAccountsService;
            _bankAccountsService.Initialize(context);
        }

        // GET: api/BankAccounts
        public async Task<List<BankAccount>> GetBankAccounts()
        {
            return await _bankAccountsService.GetAllBankAccounts();
        }


        // PUT: api/BankAccounts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBankAccount(BankAccount bankAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _bankAccountsService.UpdateBankAccount(bankAccount);
                if (result == null)
                    return NotFound();
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }

        // POST: api/BankAccounts
        [ResponseType(typeof(BankAccount))]
        public async Task<IHttpActionResult> PostBankAccount(BankAccount bankAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _bankAccountsService.AddBankAccount(bankAccount);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(bankAccount);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/BankAccounts/5
        [ResponseType(typeof(BankAccount))]
        public async Task<IHttpActionResult> DeleteBankAccount(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _bankAccountsService.DeleteBankAccount(id, removeRelatedEntities);
                if (result == false)
                    return NotFound();
                if (result == null)
                    return BadRequest();
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bankAccountsService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}