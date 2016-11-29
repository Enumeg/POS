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
        private static object Get(string property)
        {
            return HttpContext.Current.Request.Cookies[property] != null ? Unprotect(HttpContext.Current.Request.Cookies[property].Value, property) : null;
        }
        private static void Set(string property, object value)
        {
            HttpContext.Current.Response.Cookies.Remove(property);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(property, Protect(value.ToString(), property))
            {
                Expires = DateTime.Now.AddDays(360)
            });
        }
        public static int TenantId
        {
            get
            {
                var value = Get("TenantId");
                return value != null ? int.Parse(value.ToString()) : 0;
            }
            set
            {
                Set("TenantId", value);
            }
        }
        public static int ShiftId
        {
            get
            {
                var value = Get("ShiftId");
                return value != null ? int.Parse(value.ToString()) : 0;
            }
            set
            {
                Set("ShiftId", value);
            }
        }
        public static int MachineId
        {
            get
            {
                var value = Get("MachineId");
                return value != null ? int.Parse(value.ToString()) : 0;
            }
            set
            {
                Set("MachineId", value);
            }
        }
    }
}