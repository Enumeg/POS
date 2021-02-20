using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using POS.Domain.Interfaces;
using POS.Domain.Services;
using POS.Portal.Helpers;
using POS.Resources;

namespace POS.Portal.Filters
{
    public class ShiftFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (CookieHelper.TenantId != 0)
            {
                IShiftsService shiftsService = new ShiftsService(ContextCache.GetPosContext());
                if (shiftsService.IsShiftClosed(CookieHelper.ShiftId))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        new { Message = Common.ShiftClosed },
                        actionContext.ControllerContext.Configuration.Formatters.JsonFormatter
                    );
                }

            }
        }

    }
}