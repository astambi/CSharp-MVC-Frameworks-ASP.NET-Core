namespace CameraBazaar.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Camera
    {
        public int Id { get; set; }

        public CameraMake Make { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        // Seconds
        [Range(0, 100)]
        public int MinShutterSpeed { get; set; }

        // Fraction of a second
        [Range(2000, 8000)]
        public int MaxShutterSpeed { get; set; }

        public MinIso MinIso { get; set; }

        [Range(200, 409600)]
        public int MaxIso { get; set; }

        public bool IsFullFrame { get; set; }

        [Required]
        [MaxLength(15)]
        public string VideoResolution { get; set; }

        public LightMetering LightMetering { get; set; }

        [Required]
        [MaxLength(6000)]
        public string Description { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(2000)]
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
