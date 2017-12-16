namespace Prestissimo.Web.Areas.Admin.Models.Artists
{
    using Data;
    using Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class ArtistFormModel
    {
        [Required]
        [MaxLength(DataConstants.ArtistNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.ArtistDescriptionMaxLength)]
        public string Description { get; set; }

        [Display(Name = "Artist Category")]
        public ArtistType ArtistType { get; set; }
    }
}
