using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Portal.Helpers;
using POS.Resources;
using POS.Domain.Services;
using POS.Portal.Filters;
using expense = POS.Domain.Entities.Expense;

namespace POS.Portal.Controllers.API
{
    public class ExpensesController : ApiController
    {
        private readonly IExpensesService _expenseService;
        public ExpensesController(IExpensesService expenseService)
        {
            _expenseService = expenseService;
        }

        // GET: api/Expense
        public async Task<List<Expense>> GetExpense()
        {
            return await _expenseService.GetAllExpenses();
        }


        // PUT: api/Expense/5
        [ResponseType(typeof(void))]
        [ShiftFilter]
        public async Task<IHttpActionResult> Putexpense(expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _expenseService.UpdateExpense(expense);
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

        // POST: api/Expense
        [ResponseType(typeof(expense))]
        [ShiftFilter]
        public async Task<IHttpActionResult> Postexpense(expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                expense.ShiftId = CookieHelper.ShiftId;
                var result = await _expenseService.AddExpense(expense);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(expense);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Expense/5
        [ResponseType(typeof(expense))]
        [ShiftFilter]
        public async Task<IHttpActionResult> Deleteexpense(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _expenseService.DeleteExpense(id);
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
                _expenseService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}