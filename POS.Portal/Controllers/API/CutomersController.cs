using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Domain.Interfaces;
using POS.Portal.Helpers;
using POS.Resources;

namespace POS.Portal.Controllers.API
{
    public class CustomersController : ApiController
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            var context = ContextCache.GetPosContext();
            _customersService = customersService;
            _customersService.Initialize(context);
        }

        // GET: api/Customers
        public async Task<List<Customer>> GetCustomers()
        {
            return await _customersService.GetAllCustomers();
        }


        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _customersService.UpdateCustomer(customer);
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

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _customersService.AddCustomer(customer);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(customer);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> DeleteCustomer(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _customersService.DeleteCustomer(id, removeRelatedEntities);
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
                _customersService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}