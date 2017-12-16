namespace Prestissimo.Web.Models.Account
{
    using Prestissimo.Data;
    using System.ComponentModel.DataAnnotations;

    public class ExternalLoginViewModel
    {
        [Required]
        [MinLength(DataConstants.UserNameMinLength)]
        [MaxLength(DataConstants.UserNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.UserUsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
