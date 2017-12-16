namespace Prestissimo.Web.Areas.Admin.Models.Formats
{
    using Data;
    using System.ComponentModel.DataAnnotations;

    public class FormatFormModel
    {
        [Required]
        [MaxLength(DataConstants.FormatNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.FormatDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
