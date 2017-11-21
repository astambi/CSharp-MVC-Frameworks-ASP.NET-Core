namespace CameraBazaar.Services
{
    using Models.Users;
    using System;

    public interface IUserService
    {
        UserDetailsModel GetUserDetailsWithCameras(string username);

        void UpdateLoginTime(string userName, DateTime currentDateTime);
    }
}
