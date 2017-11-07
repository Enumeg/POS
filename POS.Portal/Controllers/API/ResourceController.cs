using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web.Http;

namespace POS.Portal.Controllers.API
{
    public class ResourceController : ApiController
    {
        [Authorize]
        public Dictionary<string, string> GetResource()
        {
            var dic = new Dictionary<string, string>();
            var sets = new List<ResourceSet>
            {
                Resources.Common.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, true, true),
                Resources.Person.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, true, true),
                Resources.Product.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, true, true),
                Resources.Pages.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, true, true),
                Resources.Transaction.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, true, true),
            };
            foreach (var item in sets.SelectMany(resourceSet => resourceSet.Cast<DictionaryEntry>().Where(item => !dic.ContainsKey(item.Key.ToString()))))
            {
                dic.Add(item.Key.ToString(), item.Value.ToString());
            }
            return dic;
        }
    }
}
