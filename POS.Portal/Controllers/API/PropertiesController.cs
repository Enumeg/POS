using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Interfaces;
using POS.Portal.Helpers;
using POS.Resources;
using Property = POS.Domain.Entities.Property;

namespace POS.Portal.Controllers.API
{
    public class PropertiesController : ApiController
    {
        private readonly IPropertiesService _propertiesService;

        public PropertiesController(IPropertiesService propertiesService)
        {
            var context = ContextCache.GetPosContext();
            _propertiesService = propertiesService;
            _propertiesService.Initialize(context);
        }

        // GET: api/Properties
        public async Task<List<Property>> GetProperties()
        {
            return await _propertiesService.GetAllProperties();
        }

        // GET: api/Properties/5
        public async Task<List<Property>> GetProperties(int categoryId)
        {
            return await _propertiesService.GetCategoryProperties(categoryId);
        }
        // PUT: api/Properties/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProperty(Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _propertiesService.UpdateProperty(property);
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

        // POST: api/Properties
        [ResponseType(typeof(Property))]
        public async Task<IHttpActionResult> PostProperty(Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _propertiesService.AddProperty(property);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(property);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Properties/5
        [ResponseType(typeof(Property))]
        public async Task<IHttpActionResult> DeleteProperty(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _propertiesService.DeleteProperty(id, removeRelatedEntities);
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
                _propertiesService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}