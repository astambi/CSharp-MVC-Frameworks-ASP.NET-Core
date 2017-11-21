namespace CameraBazaar.Web.Models.Cameras
{
    using Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CameraViewModel
    {
        public CameraMake Make { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression("^[A-Z0-9-]+$",
            ErrorMessage = "The {0} can contain only uppercase letters, digits and a dash (“-“).")]
        public string Model { get; set; }

        [Range(0, double.MaxValue,
            ErrorMessage = "The {0} cannot be negative.")]
        public decimal Price { get; set; }

        [Range(0, 100,
            ErrorMessage = "The {0} must be in the range {1} – {2}.")]
        public int Quantity { get; set; }

        [Range(0, 100,
            ErrorMessage = "The {0} must be in the range {1} – {2}.")]
        [Display(Name = "Min Shutter Speed")]
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000,
            ErrorMessage = "The {0} must be in the range {1} – {2}.")]
        [Display(Name = "Max Shutter Speed")]
        public int MaxShutterSpeed { get; set; }

        [Display(Name = "Min ISO")]
        public MinIso MinISO { get; set; }

        [Display(Name = "Max ISO")]
        [Range(200, 409600,
            ErrorMessage = "The {0} must be in the range {1} – {2}.")]
        [RegularExpression("^[1-9][0-9]*00$",
            ErrorMessage = "The {0} must be divisible by 100.")]
        public int MaxISO { get; set; }

        [Display(Name = "Full Frame")]
        public bool IsFullFrame { get; set; }

        [Required]
        [StringLength(15,
            ErrorMessage = "The {0} must be no longer than {1} symbols.")]
        [Display(Name = "Video Resolution")]
        public string VideoResolution { get; set; }

        [Required]
        [Display(Name = "Light Metering")]
        public IEnumerable<LightMetering> LightMeterings { get; set; }

        [Required]
        [StringLength(6000,
            ErrorMessage = "The {0} must be no longer than {1} symbols.")]
        public string Description { get; set; }

        [Required]
        [Url]
        [StringLength(2000,
            ErrorMessage = "The {0} must contain between {2} and {1} symbols.", MinimumLength = 10)]
        [RegularExpression(@"^(https?:\/\/).+$",
            ErrorMessage = "The {0} must start with http:// or https://")]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}
