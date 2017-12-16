namespace Prestissimo.Web.Areas.Admin.Models.Labels
{
    using Data;
    using System.ComponentModel.DataAnnotations;

    public class LabelFormModel
    {
        [Required]
        [MaxLength(DataConstants.LabelNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.LabelDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
