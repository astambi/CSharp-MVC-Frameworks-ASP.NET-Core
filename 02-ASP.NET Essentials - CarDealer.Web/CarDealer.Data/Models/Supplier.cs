namespace CarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
