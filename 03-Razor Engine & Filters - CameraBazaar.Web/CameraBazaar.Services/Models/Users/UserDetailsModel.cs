namespace CameraBazaar.Services.Models.Users
{
    using Models.Cameras;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserDetailsModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsOwner { get; set; } = false;

        public DateTime? LastLoginTime { get; set; }

        public IEnumerable<CameraListingModel> Cameras { get; set; } = new List<CameraListingModel>();

        public int CamerasInStock => this.Cameras.Where(c => c.InStock).Count();

        public int CamerasOutOfStock => this.Cameras.Where(c => !c.InStock).Count();
    }
}
