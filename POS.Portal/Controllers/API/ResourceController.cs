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
                Resources.Common.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, false, false),
                Resources.Person.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, false, false),
                Resources.Product.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, false, false),
                Resources.Pages.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, false, false),
                Resources.Transaction.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, false, false),
            };
            foreach (var item in sets.SelectMany(resourceSet => resourceSet.Cast<DictionaryEntry>().Where(item => !dic.ContainsKey(item.Key.ToString()))))
            {
                dic.Add(item.Key.ToString(), item.Value.ToString());
            }
            return dic;
        }
    }
}
