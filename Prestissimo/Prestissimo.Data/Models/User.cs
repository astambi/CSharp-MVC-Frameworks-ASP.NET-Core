namespace Prestissimo.Data.Models
{
    using Data;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(DataConstants.UserNameMinLength)]
        [MaxLength(DataConstants.UserNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
