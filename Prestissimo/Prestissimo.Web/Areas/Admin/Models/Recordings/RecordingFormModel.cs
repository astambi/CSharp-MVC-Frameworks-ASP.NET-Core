namespace Prestissimo.Web.Areas.Admin.Models.Recordings
{
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RecordingFormModel
    {
        [Required]
        [MaxLength(DataConstants.RecordingTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.RecordingDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Length (in minutes)")]
        public int Length { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Label")]
        public int LabelId { get; set; }

        public IEnumerable<SelectListItem> Labels { get; set; }
    }
}
