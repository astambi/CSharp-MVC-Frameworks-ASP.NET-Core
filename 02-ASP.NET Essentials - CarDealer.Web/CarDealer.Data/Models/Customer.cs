namespace CarDealer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
