using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using POS.Portal.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace POS.Portal.Controllers.API
{

    public class RolesController : ApiController
    {
        private readonly ApplicationRoleManager _appRoleManager = null;
        private readonly ApplicationUserManager _appUserManager = null;

        protected ApplicationRoleManager AppRoleManager => _appRoleManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        protected ApplicationUserManager AppUserManager => _appUserManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
     

        public IHttpActionResult GetAllRoles()
        {
            var roles = AppRoleManager.Roles;
            return Ok(roles);
        }
        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateRoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new IdentityRole { Name = model.Name };
            var result = await AppRoleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return Ok(result.Errors.ToString());
            }

        
            return Created("", role.Id);

        }

        public async Task<IHttpActionResult> DeleteRole(string Id)
        {

            var role = await AppRoleManager.FindByIdAsync(Id);

            if (role != null)
            {
                var result = await AppRoleManager.DeleteAsync(role);

                

                return Ok();
            }

            return NotFound();

        }

        [Route("ManageUsersInRole")]
        public async Task<IHttpActionResult> ManageUsersInRole(UsersInRoleModel model)
        {
            var role = await AppRoleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ModelState.AddModelError("", "Role does not exist");
                return BadRequest(ModelState);
            }

            foreach (var user in model.EnrolledUsers)
            {
                var appUser = await AppUserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    ModelState.AddModelError("", String.Format("User: {0} does not exists", user));
                    continue;
                }

                if (!AppUserManager.IsInRole(user, role.Name))
                {
                    var result = await AppUserManager.AddToRoleAsync(user, role.Name);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", String.Format("User: {0} could not be added to role", user));
                    }

                }
            }

            foreach (var user in model.RemovedUsers)
            {
                var appUser = await AppUserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    ModelState.AddModelError("", String.Format("User: {0} does not exists", user));
                    continue;
                }

                var result = await AppUserManager.RemoveFromRoleAsync(user, role.Name);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", String.Format("User: {0} could not be removed from role", user));
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

    }
}
