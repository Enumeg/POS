using System;
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
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            var context = ContextCache.GetPosContext();
            _categoriesService = categoriesService;
            _categoriesService.Initialize(context);
        }

        // GET: api/Categories
        public async Task<List<Category>> GetCategories()
        {
            return await _categoriesService.GetAllCategories();
        }

       
        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }                      

            try
            {
               var result = await _categoriesService.UpdateCategory(category);
                if (result == null)
                    return NotFound();
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoriesService.AddCategory(category);             
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(category);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> DeleteCategory(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _categoriesService.DeleteCategory(id, removeRelatedEntities);
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
                _categoriesService.Dispose();
            }
            base.Dispose(disposing);
        }

       
    }
}