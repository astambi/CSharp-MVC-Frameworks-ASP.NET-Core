namespace LearningSystem.Web.Models.AccountViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "BirthDate")]
        public DateTime Birthdate { get; set; }
    }
}
