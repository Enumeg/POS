using System.Threading;

namespace POS.Domain.Helpers
{
    public static class CommonHelper
    {
        public static bool IsArabic => Thread.CurrentThread.CurrentUICulture.Name.StartsWith("ar");

        public static string DateFormat => Thread.CurrentThread.CurrentUICulture.Name.StartsWith("ar") ? "yyyy/MM/dd" : Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;

    }
}
