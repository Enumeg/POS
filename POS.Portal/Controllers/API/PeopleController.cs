using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Domain.Interfaces;
using POS.Domain.Services;
using POS.Portal.Helpers;
using POS.Resources;
using Person = POS.Domain.Entities.Person;

namespace POS.Portal.Controllers.API
{
    public class PeopleController : ApiController
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            var context = ContextCache.GetPosContext();
            _peopleService = peopleService;
            _peopleService.Initialize(context);
        }

        // GET: api/People
        public async Task<List<Person>> GetPeople()
        {
            return await _peopleService.GetAllPeople();
        }
        public async Task<List<Person>> GetPeople(bool isCustomer)
        {
            return await _peopleService.GetPeople(isCustomer);
        }    
        // PUT: api/People/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _peopleService.UpdatePerson(person);
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

        // POST: api/People
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _peopleService.AddPerson(person);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(person);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> DeletePerson(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _peopleService.DeletePerson(id, removeRelatedEntities);
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
                _peopleService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}