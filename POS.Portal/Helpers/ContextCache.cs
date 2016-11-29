using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POS.Domain.Infrastructure;
using Microsoft.AspNet.Identity.Owin;

namespace POS.Portal.Helpers
{
    public static class ContextCache
    {
        public static PosContext GetPosContext()
        {
            return HttpContext.Current.GetOwinContext().Get<PosContext>();
        }
    }
}