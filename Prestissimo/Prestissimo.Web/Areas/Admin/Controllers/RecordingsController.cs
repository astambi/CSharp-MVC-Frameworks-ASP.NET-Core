namespace Prestissimo.Web.Areas.Admin.Controllers
{
    using Admin.Models.Recordings;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin;
    using Services.Admin.Models.Labels;
    using Services.Admin.Models.Recordings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Infrastructure.Extensions;

    public class RecordingsController : BaseAdminController
    {
        private readonly IAdminRecordingService recordingService;
        private readonly IAdminLabelService labelService;

        public RecordingsController(
            IAdminRecordingService recordingService,
            IAdminLabelService labelService)
        {
            this.recordingService = recordingService;
            this.labelService = labelService;
        }

        public async Task<IActionResult> Index()
        {
            var recordingsData = await this.recordingService
                .AllAsync<AdminRecordingListingServiceModel>();

            return this.View(recordingsData);
        }

        public async Task<IActionResult> Create()
            => this.View(new RecordingFormModel
            {
                Labels = await this.GetLabelsSelectListAsync()
            });

        [HttpPost]
        public async Task<IActionResult> Create(RecordingFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Labels = await this.GetLabelsSelectListAsync();
                return this.View(model);
            }

            if (!await this.LabelExists(model.LabelId))
            {
                return this.BadRequest(WebAdminConstants.LabelNotFoundMsg);
            }

            await this.recordingService.CreateAsync(
                model.Title,
                model.Description,
                (DateTime)model.ReleaseDate,
                model.Length,
                model.ImageUrl,
                model.LabelId);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.RecordingCreatedMsg,
                model.Title.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var recordingData = await this.recordingService
                .GetByIdAsync<AdminRecordingDetailsToModifyServiceModel>(id);

            if (recordingData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.RecordingNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(new RecordingFormModel
            {
                Title = recordingData.Title,
                Description = recordingData.Description,
                ReleaseDate = recordingData.ReleaseDate,
                ImageUrl = recordingData.ImageUrl,
                Length = recordingData.Length,
                LabelId = recordingData.LabelId,
                Labels = await this.GetLabelsSelectListAsync()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RecordingFormModel model)
        {
            var recordingExists = await this.recordingService.ExistsAsync(id);
            if (!recordingExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.RecordingNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            if (!await this.LabelExists(model.LabelId))
            {
                return this.BadRequest(WebAdminConstants.LabelNotFoundMsg);
            }

            if (!this.ModelState.IsValid)
            {
                model.Labels = await this.GetLabelsSelectListAsync();
                return this.View(model);
            }

            await this.recordingService.UpdateAsync(
                id,
                model.Title,
                model.Description,
                (DateTime)model.ReleaseDate,
                model.Length,
                model.ImageUrl,
                model.LabelId);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.RecordingUpdatedMsg,
                model.Title.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var recordingData = await this.recordingService
                .GetByIdAsync<AdminRecordingListingServiceModel>(id);

            if (recordingData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.RecordingNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(recordingData);
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var recordingData = await this.recordingService
                .GetByIdAsync<AdminRecordingBasicServiceModel>(id);

            if (recordingData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.RecordingNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            await this.recordingService.RemoveAsync(id);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.RecordingDeletedMsg,
                recordingData.Title.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetLabelsSelectListAsync()
            => (await this.labelService.AllAsync<AdminLabelBasicServiceModel>())
                .Select(l => new SelectListItem
                {
                    Text = l.Name,
                    Value = l.Id.ToString()
                })
                .ToList();

        private async Task<bool> LabelExists(int labelId)
            => await this.labelService.ExistsAsync(labelId);
    }
}
