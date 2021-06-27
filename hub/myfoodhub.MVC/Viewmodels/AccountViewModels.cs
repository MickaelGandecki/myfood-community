using System.ComponentModel.DataAnnotations;

namespace myfoodapp.Hub.Models
{
    public class LoginViewModel
    {    
        [Required]
        [Display(Name = "[[[Email]]]")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "[[[Password]]]")]
        public string Password { get; set; }

		[Display(Name = "[[[Remember me?]]]")]
		public bool RememberMe { get; set; } = true;
    }

    public class UserViewModel
    {
        [Display(Name = "[[[Name]]]")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "[[[Surname]]]")]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "[[[Login]]]")]
        [EmailAddress]
        public string Login { get; set; }

        [Display(Name = "[[[Notification Email]]]")]
        [EmailAddress]
        public string NotificationEmail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "[[[Old Password]]]")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "[[[New Password]]]")]
        public string NewPassword { get; set; }

        [Display(Name = "[[[Enable Email Notification]]]")]
        public bool? IsMailNotificationActivated { get; set; }

        public int Language { get; set; }

        [Display(Name = "[[[Display the temperature into Farenheit]]]")]
        public bool? HasFahrenheitSetting { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
        public string UserId { get; set; }
    }
}