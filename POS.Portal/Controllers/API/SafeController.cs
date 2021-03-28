using POS.Domain.Helpers;
using POS.Domain.Services;
using POS.Portal.Helpers;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Resources;
using Point = POS.Domain.Entities.Point;

namespace POS.Portal.Controllers.API
{
    public class SafesController : ApiController
    {
        private readonly ISafeService _safeService;

        public SafesController(ISafeService safeService)
        {
            _safeService = safeService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetSafes()
        {
            var safes = await _safeService.GetSafes();
            return Ok(safes);
        }
        // api/get
        [HttpGet]
        [Route("api/safes/Select")]
        public async Task<IHttpActionResult> SafesSelectList()
        {
            var safes = await _safeService.GetSafes();
            safes = User.IsInRole(Roles.CanChangeSafe) ? safes : safes.Where(s => s.Id == CookieHelper.SafeId).ToList();
            return Ok(safes.Select(s => new { s.Id, s.Name }));
        }
        // PUT: api/Points/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPoint(Safe safe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _safeService.UpdateSafe(safe);
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

        // POST: api/Points
        [ResponseType(typeof(Point))]
        public async Task<IHttpActionResult> PostPoint(Safe safe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _safeService.AddSafe(safe);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(safe);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Points/5
        [ResponseType(typeof(Point))]
        public async Task<IHttpActionResult> DeletePoint(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _safeService.DeleteSafe(id, removeRelatedEntities);
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
                _safeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
