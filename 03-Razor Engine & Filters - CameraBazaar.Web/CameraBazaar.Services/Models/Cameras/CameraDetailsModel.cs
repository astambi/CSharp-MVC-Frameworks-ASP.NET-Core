namespace CameraBazaar.Services.Models.Cameras
{
    using Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class CameraDetailsModel : CameraListingModel
    {
        [Display(Name = "Is Full Frame")]
        public bool IsFullFrame { get; set; }

        [Display(Name = "Min Shutter Speed")]
        public int MinShutterSpeed { get; set; }

        [Display(Name = "Max Shutter Speed")]
        public int MaxShutterSpeed { get; set; }

        [Display(Name = "Min ISO")]
        public MinIso MinISO { get; set; }

        [Display(Name = "Max ISO")]
        public int MaxISO { get; set; }

        [Display(Name = "Video Resolution")]
        public string VideoResolution { get; set; }

        [Display(Name = "Light Metering")]
        public LightMetering LightMeterings { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
