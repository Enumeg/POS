using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Portal.Helpers;
using POS.Resources;
using POS.Domain.Services;
using Installment = POS.Domain.Entities.Installment;

namespace POS.Portal.Controllers.API
{
    public class InstallmentsController : ApiController
    {
        private readonly IInstallmentsService _banksService;

        public InstallmentsController(IInstallmentsService banksService)
        {
            var context = ContextCache.GetPosContext();
            _banksService = banksService;
            _banksService.Initialize(context);
        }

        // GET: api/Installments
        public async Task<List<Installment>> GetInstallments()
        {
            return await _banksService.GetAllInstallments();
        }


        // PUT: api/Installments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInstallment(Installment bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _banksService.UpdateInstallment(bank);
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

        // POST: api/Installments
        [ResponseType(typeof(Installment))]
        public async Task<IHttpActionResult> PostInstallment(Installment bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _banksService.AddInstallment(bank);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(bank);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Installments/5
        [ResponseType(typeof(Installment))]
        public async Task<IHttpActionResult> DeleteInstallment(int id)
        {
            try
            {
                var result = await _banksService.DeleteInstallment(id);
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