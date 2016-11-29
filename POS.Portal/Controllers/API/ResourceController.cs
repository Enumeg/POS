using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            var sets = new List<ResourceSet>
            {
                Resources.Common.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, false, false),
                Resources.Pages.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, false, false)
            };
            return sets.SelectMany(set => set.Cast<DictionaryEntry>()).ToDictionary(item => item.Key.ToString(), item => item.Value.ToString());
        }
    }
}
