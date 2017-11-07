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
    public class CustomerInstallmentsController : ApiController
    {
        private readonly ICustomerInstallmentsService _banksService;

        public CustomerInstallmentsController(ICustomerInstallmentsService banksService)
        {
            var context = ContextCache.GetPosContext();
            _banksService = banksService;
            _banksService.Initialize(context);
        }

        // GET: api/CustomerInstallments
        public async Task<List<CustomerInstallment>> GetCustomerInstallments()
        {
            return await _banksService.GetAllCustomerInstallments();
        }


        // PUT: api/CustomerInstallments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomerInstallment(CustomerInstallment bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _banksService.UpdateCustomerInstallment(bank);
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

        // POST: api/CustomerInstallments
        [ResponseType(typeof(CustomerInstallment))]
        public async Task<IHttpActionResult> PostCustomerInstallment(CustomerInstallment bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _banksService.AddCustomerInstallment(bank);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(bank);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/CustomerInstallments/5
        [ResponseType(typeof(CustomerInstallment))]
        public async Task<IHttpActionResult> DeleteCustomerInstallment(int id)
        {
            try
            {
                var result = await _banksService.DeleteCustomerInstallment(id);
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