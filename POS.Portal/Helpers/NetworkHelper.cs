using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace POS.Portal.Helpers
{
    public static class NetworkHelper
    {
        public static string GetMachineName(string ip)
        {
            return Dns.GetHostEntry(IPAddress.Parse(ip)).HostName.ToString().Split('.').ToList().FirstOrDefault();
        }
    }
}