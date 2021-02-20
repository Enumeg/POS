using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using POS.Domain.Infrastructure;
using POS.Domain.Services;
using POS.Portal.Helpers;

namespace POS.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISafeService _safeService;

        public HomeController(ISafeService safeService)
        {
            _safeService = safeService;
        }

        public ActionResult Index()
        {
            //CookieHelper.TenantId = 4;
            return View();          
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            HttpContext.GetOwinContext().Get<PosContext>();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> SelectSafe()
        {
            var safes = await _safeService.GetSafes(Domain.Enums.SafeType.Cashier);
            ViewBag.Safe = new SelectList(safes, "Id", "Name");
            return View();
        }
        [AllowAnonymous]
        public void ToggleLanguage()
        {
            Response.Cookies.Remove("Culture");
            Response.Cookies.Add(System.Threading.Thread.CurrentThread.CurrentUICulture.Name.StartsWith("ar")
                ? new HttpCookie("Culture") {Expires = DateTime.Now.AddDays(360), Value = "en-US"}
                : new HttpCookie("Culture") {Expires = DateTime.Now.AddDays(360), Value = "ar-EG"});
        }
    }
}