namespace FDMC.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Cat
    {
        private const int StringMaxLength = 50;
        private const int UrlMaxLength = 2000;

        public int Id { get; set; }

        [Required]
        [MaxLength(StringMaxLength)]
        public string Name { get; set; }

        [Range(0, 30)]
        public int Age { get; set; }

        [Required]
        [MaxLength(StringMaxLength)]
        public string Breed { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        public string ImageUrl { get; set; }
    }
}
