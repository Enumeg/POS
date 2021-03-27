using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Domain.Interfaces;
using POS.Resources;

namespace POS.Portal.Controllers.API
{
    public class UnitsController : ApiController
    {
        private readonly IUnitsService _unitsService;

        public UnitsController(IUnitsService unitsService)
        {
            _unitsService = unitsService;
        }

        // GET: api/Units
        public async Task<List<Unit>> GetUnits()
        {
            return await _unitsService.GetAllUnits();
        }


        // PUT: api/Units/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUnit(Unit unit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _unitsService.UpdateUnit(unit);
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

        // POST: api/Units
        [ResponseType(typeof(Unit))]
        public async Task<IHttpActionResult> PostUnit(Unit unit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _unitsService.AddUnit(unit);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(unit);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Units/5
        [ResponseType(typeof(Unit))]
        public async Task<IHttpActionResult> DeleteUnit(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _unitsService.DeleteUnit(id, removeRelatedEntities);
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
                _unitsService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}