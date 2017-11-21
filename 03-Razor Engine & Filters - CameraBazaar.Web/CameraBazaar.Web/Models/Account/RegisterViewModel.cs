namespace CameraBazaar.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$",
            ErrorMessage = "The {0} must contain only letters.")]
        [StringLength(20,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "The {0} must contain '@' and be a valid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [RegularExpression(@"^\+[0-9]{10,12}$",
            ErrorMessage = "The {0} must start with a '+' sign and contain 10 to 12 digits.")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-z0-9]+$",
            ErrorMessage = "The {0} must contain only lowercase letters and digits.")]
        [StringLength(100,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 3)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
