namespace Prestissimo.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Label
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.LabelNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.LabelDescriptionMaxLength)]
        public string Description { get; set; }

        public ICollection<Recording> Recordings { get; set; } = new List<Recording>();
    }
}
