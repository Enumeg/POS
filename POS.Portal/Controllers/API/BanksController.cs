using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Portal.Helpers;
using POS.Resources;
using POS.Domain.Services;
using Bank = POS.Domain.Entities.Bank;
namespace POS.Portal.Controllers.API
{
    public class BanksController : ApiController
    {
        private readonly IBanksService _banksService;

        public BanksController(IBanksService banksService)
        {
            _banksService = banksService;
        }

        // GET: api/Banks
        public async Task<List<Bank>> GetBanks()
        {
            return await _banksService.GetAllBanks();
        }


        // PUT: api/Banks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBank(Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _banksService.UpdateBank(bank);
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

        // POST: api/Banks
        [ResponseType(typeof(Bank))]
        public async Task<IHttpActionResult> PostBank(Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _banksService.AddBank(bank);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(bank);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Banks/5
        [ResponseType(typeof(Bank))]
        public async Task<IHttpActionResult> DeleteBank(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _banksService.DeleteBank(id, removeRelatedEntities);
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
                _banksService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}