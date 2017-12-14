namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Home;
    using Services;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;

        public HomeController(
            ICourseService courseService,
            IUserService userService)
        {
            this.courseService = courseService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
            => View(new HomeIndexViewModel
            {
                Courses = await this.courseService.AllActiveAsync()
            });

        public async Task<IActionResult> Search(SearchFormModel model)
        {
            var viewModel = new SearchViewModel
            {
                SearchText = model.SearchText
            };

            if (model.SearchInCourses)
            {
                viewModel.Courses = await this.courseService.FindAsync(model.SearchText);
            }

            if (model.SearchInUsers)
            {
                viewModel.Users = await this.userService.FindAsync(model.SearchText);
            }

            return View(viewModel);
        }

        public IActionResult Error
            => View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            });
    }
}
