namespace CameraBazaar.Services.Implementations
{
    using Data;
    using Models.Cameras;
    using Models.Users;
    using System.Linq;
    using System;

    public class UserService : IUserService
    {
        private readonly CameraBazaarDbContext db;

        public UserService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public UserDetailsModel GetUserDetailsWithCameras(string username)
        {
            return this.db
                .Users
                .Where(u => u.UserName == username)
                .Select(u => new UserDetailsModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    Phone = u.PhoneNumber,
                    LastLoginTime = u.LastLogin,
                    Cameras = u.Cameras
                            .OrderBy(c => c.Make)
                            .ThenBy(c => c.Model)
                            .ThenByDescending(c => c.Price)
                            .Select(c => new CameraListingModel
                            {
                                Id = c.Id,
                                ImageUrl = c.ImageUrl,
                                Make = c.Make,
                                Model = c.Model,
                                Price = c.Price,
                                InStock = c.Quantity > 0
                            })
                            .ToList()
                })
                .FirstOrDefault();
        }

        public void UpdateLoginTime(string userName, DateTime currentDateTime)
        {
            var user = this.db.Users.Where(u => u.UserName == userName).FirstOrDefault();

            if (user == null)
            {
                return;
            }

            user.LastLogin = currentDateTime;
            this.db.SaveChanges();
        }
    }
}
