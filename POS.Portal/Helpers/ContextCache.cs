using Ninject.Activation;
using POS.Domain.Infrastructure;
using System.Web;

namespace POS.Portal.Helpers
{
    public static class ContextCache
    {
        public static PosContext GetPosContext()
        {
            var context = HttpContext.Current.GetOwinContext().Get<PosContext>("PosContext");
            if (context != null && context.TenantId != 0) return context;

            context = PosContext.CreateContext(CookieHelper.TenantId);
            HttpContext.Current.GetOwinContext().Set("PosContext", context);

            return context;
        }
    }
    public class PosContextWebProvider : Provider<PosContext>
    {
        protected override PosContext CreateInstance(IContext contexts)
        {
            var context = HttpContext.Current.GetOwinContext().Get<PosContext>("PosContext");
            if (context != null && context.TenantId != 0) return context;

            context = PosContext.CreateContext(CookieHelper.TenantId);
            HttpContext.Current.GetOwinContext().Set("PosContext", context);

            return context;
        }
    }
}