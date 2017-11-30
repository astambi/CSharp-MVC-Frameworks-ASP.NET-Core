﻿namespace LearningSystem.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Trainers;
    using Services;
    using Services.Models;
    using System.Threading.Tasks;

    [Authorize(Roles = WebConstants.TrainerRole)]
    public class TrainersController : Controller
    {
        private readonly ITrainerService trainerService;
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;

        public TrainersController(
            ITrainerService trainerService,
            ICourseService courseService,
            UserManager<User> userManager)
        {
            this.trainerService = trainerService;
            this.courseService = courseService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Courses()
        {
            var userId = this.userManager.GetUserId(this.User);
            var courses = await this.trainerService.CoursesAsync(userId);

            return this.View(courses);
        }

        public async Task<IActionResult> Students(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!await this.trainerService.IsTrainer(id, userId))
            {
                return NotFound();
            }

            return this.View(new StudentsInCourseViewModel
            {
                Students = await this.trainerService.StudentsInCourseAsync(id),
                Course = await this.courseService.ByIdAsync<CourseListingServiceModel>(id)
            });
        }

        [HttpPost]
        public async Task<IActionResult> GradeStudent(int id, string studentId, Grade grade)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                return BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);

            if (!await this.trainerService.IsTrainer(id, userId))
            {
                return BadRequest();
            }

            var success = await this.trainerService.AddGradeAsync(id, studentId, grade);

            if (!success)
            {
                return RedirectToAction(nameof(Students), new { id });
            }

            return this.RedirectToAction(nameof(Students), new { id });
        }
    }
}
