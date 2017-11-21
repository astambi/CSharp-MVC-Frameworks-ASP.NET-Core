namespace CameraBazaar.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public DateTime? LastLogin { get; set; }

        public ICollection<Camera> Cameras => new List<Camera>();
    }
}
