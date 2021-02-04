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
        private readonly IShiftsService _shiftsService;

        public ShiftFilter()
        {
            _shiftsService = new ShiftsService();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (CookieHelper.TenantId != 0)
            {
                _shiftsService.Initialize(ContextCache.GetPosContext());
                if (_shiftsService.IsShiftClosed(CookieHelper.ShiftId))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        new { Message = Common.ShiftClosed},
                        actionContext.ControllerContext.Configuration.Formatters.JsonFormatter
                    );
                }

            }
        }

    }
}