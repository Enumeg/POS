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
            var context =  HttpContext.Current.GetOwinContext().Get<PosContext>();
            if (context != null && context.TenantId != 0) return context;

            context = PosContext.CreateContext(CookieHelper.TenantId);
            HttpContext.Current.GetOwinContext().Set(context);

            return context;
        }
    }
}