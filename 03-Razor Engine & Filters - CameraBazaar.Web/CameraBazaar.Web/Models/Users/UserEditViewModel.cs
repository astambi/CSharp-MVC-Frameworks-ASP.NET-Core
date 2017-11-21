namespace CameraBazaar.Web.Models.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserEditViewModel
    {
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        [RegularExpression(@"^\+[0-9]{10,12}$",
            ErrorMessage = "The {0} must start with a '+' sign and contain 10 to 12 digits.")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression("^[a-z0-9]{3,}$", ErrorMessage = "The {0} must be at least 3 symbols & contain only lowercase letters and digits.")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public DateTime? LastLoginTime { get; set; }
    }
}
