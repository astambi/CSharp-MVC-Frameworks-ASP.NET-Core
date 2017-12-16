namespace Prestissimo.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Artist
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.ArtistNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.ArtistDescriptionMaxLength)]
        public string Description { get; set; }

        public ArtistType ArtistType { get; set; }

        public ICollection<RecordingArtist> Recordings { get; set; } = new List<RecordingArtist>();
    }
}
