namespace LearningSystem.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public UsersController(
            IUserService userService,
            UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Profile(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            var profile = await this.userService.ProfileAsync(user.Id);

            return this.View(profile);
        }

        [Authorize]
        public async Task<IActionResult> DownloadCertificate(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var certificateContents = await this.userService.GetPdfCertificate(id, userId);
            if (certificateContents == null)
            {
                return BadRequest();
            }

            return File(certificateContents, "application/pdf", "Certificate.pdf");
        }
    }
}
