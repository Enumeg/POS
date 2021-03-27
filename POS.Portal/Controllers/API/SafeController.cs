using POS.Domain.Helpers;
using POS.Domain.Services;
using POS.Portal.Helpers;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace POS.Portal.Controllers.API
{
    public class SafeController : ApiController
    {
        private readonly ISafeService _safeService;

        public SafeController(ISafeService safeService)
        {
            _safeService = safeService;
        }


        // api/get
        [HttpGet]
        public async Task<IHttpActionResult> GetSafes()
        {
            var safes = await _safeService.GetSafes();
            safes = User.IsInRole(Roles.CanChangeSafe) ? safes : safes.Where(s => s.Id == CookieHelper.SafeId).ToList();
            return Ok(safes.Select(s => new { s.Id, s.Name }));
        }
    }
}
