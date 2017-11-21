namespace CameraBazaar.Services.Models.Cameras
{
    using Data.Models;

    public class CameraListingModel
    {
        public int Id { get; set; }

        public CameraMake Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool InStock { get; set; }

        public string ImageUrl { get; set; }
    }
}
