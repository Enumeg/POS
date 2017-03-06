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
    public class SuppliersController : ApiController
    {
        private readonly ISuppliersService _suppliersService;

        public SuppliersController(ISuppliersService suppliersService)
        {
            var context = ContextCache.GetPosContext();
            _suppliersService = suppliersService;
            _suppliersService.Initialize(context);
        }

        // GET: api/Suppliers
        public async Task<List<Supplier>> GetSuppliers()
        {
            return await _suppliersService.GetAllSuppliers();
        }


        // PUT: api/Suppliers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSupplier(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _suppliersService.UpdateSupplier(supplier);
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

        // POST: api/Suppliers
        [ResponseType(typeof(Supplier))]
        public async Task<IHttpActionResult> PostSupplier(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _suppliersService.AddSupplier(supplier);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(supplier);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Suppliers/5
        [ResponseType(typeof(Supplier))]
        public async Task<IHttpActionResult> DeleteSupplier(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _suppliersService.DeleteSupplier(id, removeRelatedEntities);
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
                _suppliersService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}