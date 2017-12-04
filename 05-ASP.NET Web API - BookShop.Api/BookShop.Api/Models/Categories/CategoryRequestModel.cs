namespace BookShop.Api.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using static BookShop.Models.ModelConstants;

    public class CategoryRequestModel
    {
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
