using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using POS.Resources;

namespace POS.Portal.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(ResourceType = typeof(Identity), Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(ResourceType = typeof(Identity), Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Identity), Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Identity), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Identity), Name = "Rememberme")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Identity), Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Identity), ErrorMessageResourceName = "PasswordLength", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Identity), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Identity), Name = "Confirmpassword")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(Identity), ErrorMessageResourceName = "PasswordConfirmation")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof(Identity), Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Identity), ErrorMessageResourceName = "PasswordLength", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Identity), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Identity), Name = "Confirmpassword")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(Identity), ErrorMessageResourceName = "PasswordConfirmation")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof(Identity), Name = "Email")]
        public string Email { get; set; }
    }
    public class CreateRoleBindingModel
    {
        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }

    }

    public class UsersInRoleModel
    {

        public string Id { get; set; }
        public List<string> EnrolledUsers { get; set; }
        public List<string> RemovedUsers { get; set; }
    }
}
