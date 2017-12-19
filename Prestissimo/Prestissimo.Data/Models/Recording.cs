namespace Prestissimo.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recording
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.RecordingTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.RecordingDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Range(DataConstants.RecordingMinLength, DataConstants.RecordingMaxLength)]
        public int Length { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public int LabelId { get; set; }

        public Label Label { get; set; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();

        public ICollection<RecordingArtist> Artists { get; set; } = new List<RecordingArtist>();

        public ICollection<RecordingFormat> Formats { get; set; } = new List<RecordingFormat>();
    }
}
