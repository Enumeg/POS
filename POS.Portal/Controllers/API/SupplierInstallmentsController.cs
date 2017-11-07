using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Portal.Helpers;
using POS.Resources;
using POS.Domain.Services;
namespace POS.Portal.Controllers.API
{
    public class SupplierInstallmentsController : ApiController
    {
        private readonly ISupplierInstallmentsService _banksService;

        public SupplierInstallmentsController(ISupplierInstallmentsService banksService)
        {
            var context = ContextCache.GetPosContext();
            _banksService = banksService;
            _banksService.Initialize(context);
        }

        // GET: api/SupplierInstallments
        public async Task<List<SupplierInstallment>> GetSupplierInstallments()
        {
            return await _banksService.GetAllSupplierInstallments();
        }


        // PUT: api/SupplierInstallments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSupplierInstallment(SupplierInstallment bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _banksService.UpdateSupplierInstallment(bank);
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

        // POST: api/SupplierInstallments
        [ResponseType(typeof(SupplierInstallment))]
        public async Task<IHttpActionResult> PostSupplierInstallment(SupplierInstallment bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _banksService.AddSupplierInstallment(bank);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(bank);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/SupplierInstallments/5
        [ResponseType(typeof(SupplierInstallment))]
        public async Task<IHttpActionResult> DeleteSupplierInstallment(int id)
        {
            try
            {
                var result = await _banksService.DeleteSupplierInstallment(id);
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