using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using POS.Domain.Infrastructure;
using POS.Portal.Helpers;

namespace POS.Portal.Controllers
{
    public class HomeController : Controller
    {
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
    }
}