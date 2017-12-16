namespace Prestissimo.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models.Users;
    using Services.Admin;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Infrastructure.Extensions;

    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService adminUserService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(
            IAdminUserService adminUserService,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.adminUserService = adminUserService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.adminUserService.AllAsync();

            var roles = await this.roleManager
                .Roles
                .OrderBy(r => r.Name)
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name // roleName => RoleExistsAsync
                })
                .ToListAsync();

            return View(new UserListingViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddRemoveUserRoleFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                this.ModelState.AddModelError(string.Empty, WebAdminConstants.UserIvalidIdentityDetailsMsg);
            }

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.UserAddedToRoleMsg,
                user.Name,
                user.UserName,
                model.Role));
                
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromRole(AddRemoveUserRoleFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                this.ModelState.AddModelError(string.Empty, WebAdminConstants.UserIvalidIdentityDetailsMsg);
            }

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.RemoveFromRoleAsync(user, model.Role);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.UserRemovedFromRoleMsg,
                user.Name,
                user.UserName,
                model.Role));

            return RedirectToAction(nameof(Index));
        }
    }
}
