namespace CameraBazaar.Services
{
    using Data.Models;
    using Models.Cameras;
    using System.Collections.Generic;

    public interface ICameraService
    {
        IEnumerable<CameraListingModel> All();

        void Create(
            CameraMake make, string model, decimal price,
            int quantity, int minShutterSpeed, int maxShutterSpeed,
            MinIso minIso, int maxIso, bool isFullFrame,
            string videoResolution,
            IEnumerable<LightMetering> lightMeterings,
            string description, string imageUrl, string userId);

        bool Exists(int id);

        CameraDetailsModel GetById(int id);

        CameraWithSellerModel GetByIdWithSeller(int id);

        void Delete(int id);

        void Update(int id,
            CameraMake make, string model, decimal price,
            int quantity, int minShutterSpeed, int maxShutterSpeed,
            MinIso minIso, int maxIso, bool isFullFrame,
            string videoResolution,
            IEnumerable<LightMetering> lightMeterings,
            string description, string imageUrl);
    }
}
