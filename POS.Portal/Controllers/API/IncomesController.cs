using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Portal.Helpers;
using POS.Resources;
using POS.Domain.Services;
using POS.Portal.Filters;
using income = POS.Domain.Entities.Income;
using POS.Domain.Interfaces;

namespace POS.Portal.Controllers.API
{
    public class IncomeController : ApiController
    {
        private readonly IIncomesService _incomeService;
        private readonly IShiftsService _shiftsService;
        public IncomeController(IIncomesService incomeService, IShiftsService shiftsService)
        {
            _incomeService = incomeService;
            _shiftsService = shiftsService;
        }

        // GET: api/Income
        public async Task<List<Income>> GetIncome()
        {
            return await _incomeService.GetAllIncomes();
        }


        // PUT: api/Income/5
        [ResponseType(typeof(void))]
        [ShiftFilter]
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
        [ShiftFilter]
        public async Task<IHttpActionResult> Postincome(income income)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                income.ShiftId = CookieHelper.ShiftId;
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
        [ShiftFilter]
        public async Task<IHttpActionResult> Deleteincome(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _incomeService.DeleteIncome(id);
                switch (result)
                {
                    case false:
                        return NotFound();
                    case null:
                        return BadRequest();
                    default:
                        return Ok();
                }
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