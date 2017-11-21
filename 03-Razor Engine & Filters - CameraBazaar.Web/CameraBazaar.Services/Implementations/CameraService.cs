namespace CameraBazaar.Services.Implementations
{
    using Data;
    using Data.Models;
    using Models.Cameras;
    using System.Collections.Generic;
    using System.Linq;

    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext db;

        public CameraService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CameraListingModel> All()
        {
            return this.db
                .Cameras
                .OrderBy(c => c.Make.ToString())
                .ThenBy(c => c.Model)
                .ThenByDescending(c => c.Price)
                .Select(c => new CameraListingModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    InStock = c.Quantity > 0,
                    ImageUrl = c.ImageUrl
                })
                .ToList();
        }

        public void Create(
            CameraMake make, string model, decimal price,
            int quantity, int minShutterSpeed, int maxShutterSpeed,
            MinIso minIso, int maxIso, bool isFullFrame,
            string videoResolution,
            IEnumerable<LightMetering> lightMeterings,
            string description, string imageUrl, string userId)
        {
            var camera = new Camera
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinIso = minIso,
                MaxIso = maxIso,
                IsFullFrame = isFullFrame,
                VideoResolution = videoResolution,
                LightMetering = (LightMetering)lightMeterings.Cast<int>().Sum(), // NB! multiple enum selection
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.db.Cameras.Add(camera);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var camera = this.db.Cameras.Find(id);

            this.db.Cameras.Remove(camera);
            this.db.SaveChanges();
        }

        public bool Exists(int id)
            => this.db.Cameras.Any(c => c.Id == id);

        public CameraDetailsModel GetById(int id)
        {
            if (!this.Exists(id))
            {
                return null;
            }

            return this.db.Cameras
                .Where(c => c.Id == id)
                .Select(c => new CameraDetailsModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Description = c.Description,
                    Quantity = c.Quantity,
                    InStock = c.Quantity > 0,
                    ImageUrl = c.ImageUrl,
                    IsFullFrame = c.IsFullFrame,
                    LightMeterings = c.LightMetering,
                    MinISO = c.MinIso,
                    MaxISO = c.MaxIso,
                    MinShutterSpeed = c.MinShutterSpeed,
                    MaxShutterSpeed = c.MaxShutterSpeed,
                    VideoResolution = c.VideoResolution,
                    UserId = c.UserId,
                    UserName = c.User.UserName,
                })
                .FirstOrDefault();
        }

        public CameraWithSellerModel GetByIdWithSeller(int id)
        {
            if (!this.Exists(id))
            {
                return null;
            }

            return this.db.Cameras
                .Where(c => c.Id == id)
                .Select(c => new CameraWithSellerModel
                {
                    Id = id,
                    Make = c.Make.ToString(),
                    Model = c.Model,
                    SellerUsername = c.User.UserName
                })
                .FirstOrDefault();
        }

        public void Update(int id,
            CameraMake make, string model, decimal price, int quantity,
            int minShutterSpeed, int maxShutterSpeed, MinIso minIso, int maxIso, bool isFullFrame,
            string videoResolution, IEnumerable<LightMetering> lightMeterings,
            string description, string imageUrl)
        {
            var camera = this.db.Cameras.Find(id);

            if (camera == null)
            {
                return;
            }

            camera.Make = make;
            camera.Model = model;
            camera.Price = price;
            camera.Quantity = quantity;
            camera.MinShutterSpeed = minShutterSpeed;
            camera.MaxShutterSpeed = maxShutterSpeed;
            camera.MinIso = minIso;
            camera.MaxIso = maxIso;
            camera.IsFullFrame = isFullFrame;
            camera.VideoResolution = videoResolution;
            camera.Description = description;
            camera.ImageUrl = imageUrl;
            camera.LightMetering = (LightMetering)lightMeterings.Cast<int>().Sum(); // NB! multiple enum selection

            this.db.Cameras.Update(camera);
            this.db.SaveChanges();
        }
    }
}
