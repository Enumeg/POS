using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Interfaces;
using POS.Portal.Helpers;
using POS.Resources;
using Point = POS.Domain.Entities.Point;

namespace POS.Portal.Controllers.API
{
    public class PointsController : ApiController
    {
        private readonly IPointsService _pointsService;

        public PointsController(IPointsService pointsService)
        {
            var context = ContextCache.GetPosContext();
            _pointsService = pointsService;
            _pointsService.Initialize(context);
        }

        // GET: api/Points
        public async Task<List<Point>> GetPoints()
        {
            return await _pointsService.GetAllPoints();
        }


        // PUT: api/Points/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPoint(Point point)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _pointsService.UpdatePoint(point);
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
        public async Task<IHttpActionResult> PostPoint(Point point)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _pointsService.AddPoint(point);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(point);
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
                var result = await _pointsService.DeletePoint(id, removeRelatedEntities);
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
                _pointsService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}