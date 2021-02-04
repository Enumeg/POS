using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Portal.Controllers
{
    public class IncomeController : Controller
    {
        // GET: IIncomesService
        public ActionResult Index()
        {
            return View();
        }
    }
}