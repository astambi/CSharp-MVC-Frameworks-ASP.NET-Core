namespace Prestissimo.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Prestissimo.Web.Models.Home;
    using Services;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IRecordingService recordingService;

        public HomeController(IRecordingService recordingService)
        {
            this.recordingService = recordingService;
        }

        public async Task<IActionResult> Index()
        {
            var recordingsData = await this.recordingService.GetRecordingsAsync();
            return View(recordingsData);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
