using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Portal.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions
        public ActionResult Purchases()
        {
            return View();
        }
        public ActionResult Sales()
        {
            return View();
        }
    }
}