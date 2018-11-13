using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Portal.Helpers;
using POS.Resources;
using POS.Domain.Services;
using income = POS.Domain.Entities.Income;

namespace POS.Portal.Controllers.API
{
    public class IncomeController : ApiController
    {
        private readonly IIncomesService _incomeService;

        public IncomeController(IIncomesService incomeService)
        {
            var context = ContextCache.GetPosContext();
            _incomeService = incomeService;
            _incomeService.Initialize(context);
        }

        // GET: api/Income
        public async Task<List<Income>> GetIncome()
        {
            return await _incomeService.GetAllIncomes();
        }


        // PUT: api/Income/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putincome(income income)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _incomeService.UpdateIncome(income);
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

        // POST: api/Income
        [ResponseType(typeof(income))]
        public async Task<IHttpActionResult> Postincome(income income)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _incomeService.AddIncome(income);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(income);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Income/5
        [ResponseType(typeof(income))]
        public async Task<IHttpActionResult> Deleteincome(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _incomeService.DeleteIncome(id, removeRelatedEntities);
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
                _incomeService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}