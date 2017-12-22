namespace Prestissimo.Web.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;

    public class RecordingsController : Controller
    {
        private readonly IRecordingService recordingService;

        public RecordingsController(IRecordingService recordingService)
        {
            this.recordingService = recordingService;
        }

        public async Task<IActionResult> Details(int id)
        {
            if (! await this.recordingService.Exists(id))
            {
                this.TempData.AddErrorMessage(WebConstants.RecordingNotFound);
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var recordingDetails = await this.recordingService.Details(id);

            return this.View(recordingDetails);
        }
    }
}
