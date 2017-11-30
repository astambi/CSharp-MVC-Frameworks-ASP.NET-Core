namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ICourseService courseService;

        public HomeController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
            => View(await this.courseService.AllActiveAsync());

        public IActionResult Error
            => View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            });
    }
}
