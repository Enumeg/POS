using System;
using System.Text;
using System.Web;
using System.Web.Security;

namespace POS.Portal.Helpers
{
    public static class CookieHelper
    {
        private static string Protect(string text, string purpose)
        {
            return string.IsNullOrEmpty(text) ? null : HttpServerUtility.UrlTokenEncode(MachineKey.Protect(Encoding.UTF8.GetBytes(text), purpose));
        }
        private static string Unprotect(string text, string purpose)
        {
            return string.IsNullOrEmpty(text) ? null : Encoding.UTF8.GetString(MachineKey.Unprotect(HttpServerUtility.UrlTokenDecode(text), purpose));
        }
        public static int TenantId
        {
            get { return HttpContext.Current.Request.Cookies["TenantId"] != null ? int.Parse(Unprotect(HttpContext.Current.Request.Cookies["TenantId"].Value, "TenantId")) : 0; }
            set
            {
                HttpContext.Current.Response.Cookies.Remove("TenantId");
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("TenantId", Protect(value.ToString(), "TenantId")) { Expires = DateTime.Now.AddDays(360) });
            }
        }

    }
}