namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Courses;
    using Services.Admin;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Controllers;
    using Web.Infrastructure.Extensions;

    public class CoursesController : BaseAdminController
    {
        private readonly UserManager<User> userManager;
        private readonly IAdminCourseService adminCourseService;

        public CoursesController(
            UserManager<User> userManager,
            IAdminCourseService adminCourseService)
        {
            this.userManager = userManager;
            this.adminCourseService = adminCourseService;
        }

        public async Task<IActionResult> Create()
        {
            return View(new AddCourseFormModel
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(20),
                Trainers = await GetTrainers()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCourseFormModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.TrainerId);
            if (user == null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid user.");
            }

            if (!this.ModelState.IsValid)
            {
                model.Trainers = await GetTrainers();
                return View(model);
            }

            await this.adminCourseService.Create(
                model.Name,
                model.Description,
                model.StartDate,
                model.EndDate,
                model.TrainerId);

            this.TempData.AddSuccessMessage("Course created successfully.");

            return this.RedirectToAction(
                nameof(HomeController.Index),
                "Home",
                routeValues: new { area = string.Empty });
        }

        // todo edit

        private async Task<IEnumerable<SelectListItem>> GetTrainers()
        {
            var trainers = await this.userManager.GetUsersInRoleAsync(WebConstants.TrainerRole);
            return trainers
                 .Select(t => new SelectListItem
                 {
                     Text = $"{t.Name} ({t.UserName})",
                     Value = t.Id
                 })
                 .OrderBy(t => t.Text)
                 .ToList();
        }
    }
}
