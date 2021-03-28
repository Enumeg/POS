using System;
using System.Configuration;
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
            var value = HttpServerUtility.UrlTokenDecode(text);
            return !string.IsNullOrEmpty(text) && value != null ? Encoding.UTF8.GetString(MachineKey.Unprotect( value, purpose) ?? Array.Empty<byte>()) : null;
        }
        private static object Get(string property, bool protect = true)
        {
            var value = HttpContext.Current.Request.Cookies[property]?.Value;
            return HttpContext.Current.Request.Cookies[property] != null ? protect?  Unprotect(value, property) : value : null;
        }
        private static void Set(string property, object value, bool protect = true)
        {
            if (protect)
                value = Protect(value.ToString(), property);
            HttpContext.Current.Response.Cookies.Remove(property);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(property, value.ToString())
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
            set => Set("TenantId", value);
        }
        public static int ShiftId
        {
            get
            {
                var value = Get("ShiftId");
                return value != null ? int.Parse(value.ToString()) : 0;
            }
            set => Set("ShiftId", value);
        }
        public static int MachineId
        {
            get
            {
                var value = Get("MachineId");
                return value != null ? int.Parse(value.ToString()) : 0;
            }
            set => Set("MachineId", value);
        }
        public static int SafeId
        {
            get
            {
                var value = Get("SafeId");
                return value != null ? int.Parse(value.ToString()) : 0;
            }
            set => Set("SafeId", value);
        }
        public static int Safe
        {
            get
            {
                var value = Get("Safe", false);
                return value != null ? int.Parse(value.ToString()) : 0;
            }
            set => Set("Safe", value, false);
        }

        public static void ClearAll()
        {
            Remove("TenantId");
            Remove("ShiftId");
            Remove("MachineId");
            Remove("SafeId");
            Remove("Safe");
        }
        private static void Remove(string name)
        {
            if (HttpContext.Current.Response.Cookies[name] != null)
            {
                HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}