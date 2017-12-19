namespace Prestissimo.Web.Areas.Admin.Controllers
{
    using Admin.Models.Recordings;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin;
    using Services.Admin.Models.Artists;
    using Services.Admin.Models.Formats;
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
        private readonly IAdminArtistService artistService;
        private readonly IAdminFormatService formatService;

        public RecordingsController(
            IAdminRecordingService recordingService,
            IAdminLabelService labelService,
            IAdminArtistService artistService,
            IAdminFormatService formatService)
        {
            this.recordingService = recordingService;
            this.labelService = labelService;
            this.artistService = artistService;
            this.formatService = formatService;
        }

        public async Task<IActionResult> Index()
        {
            var recordingsData = await this.recordingService
                .AllAsync<AdminRecordingListingServiceModel>();

            return this.View(recordingsData);
        }

        public async Task<IActionResult> Artists(int id)
        {
            var recordingWithArtistsData = await this.recordingService
                .GetByIdAsync<AdminRecordingListingWithArtistsServiceModel>(id);

            var artistsSelectList =
                (await this.artistService.AllAsync<AdminArtistBasicServiceModel>())
                .Select(a => new SelectListItem
                {
                    Text = $"{a.Name} ({a.ArtistCategory})",
                    Value = a.Id.ToString()
                })
                .OrderBy(a => a.Text)
                .ToList();

            return this.View(new RecordingArtistsListingModel
            {
                Recording = recordingWithArtistsData,
                Artists = artistsSelectList
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveArtistFromRecording(int id, int artistId)
        {
            var recordingExists = await this.recordingService.ExistsAsync(id);
            if (!recordingExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.RecordingNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var artistExists = await this.artistService.ExistsAsync(artistId);
            if (!artistExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ArtistNotFoundMsg);
                return this.RedirectToAction(nameof(Artists), new { id });
            }

            var artistData = await this.artistService
                            .GetByIdAsync<AdminArtistBasicServiceModel>(artistId);

            await this.recordingService.RemoveArtistAsync(id, artistId);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.ArtistRemovedFromRecordingMsg,
                artistData.ArtistCategory.ToStrongHtml(),
                artistData.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Artists), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> AddArtistToRecording(AddArtistToRecordingModel model)
        {
            var recordingExists = await this.recordingService.ExistsAsync(model.RecordingId);
            if (!recordingExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.RecordingNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var artistExists = await this.artistService.ExistsAsync(model.ArtistId);
            if (!artistExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ArtistNotFoundMsg);
                return this.RedirectToAction(nameof(Artists), new { id = model.RecordingId });
            }

            if (!this.ModelState.IsValid)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ArtistInvalidDataMsg);
                return this.RedirectToAction(nameof(Artists), new { id = model.RecordingId });
            }

            var success = await this.recordingService.AddArtistAsync(
                model.RecordingId,
                model.ArtistId);

            var artistData = await this.artistService
                            .GetByIdAsync<AdminArtistBasicServiceModel>(model.ArtistId);

            if (!success)
            {
                this.TempData.AddErrorMessage(string.Format(
                    WebAdminConstants.ArtistAlreadyContributingToRecordingMsg,
                    artistData.ArtistCategory.ToStrongHtml(),
                    artistData.Name.ToStrongHtml()));

                return this.RedirectToAction(nameof(Artists), new { id = model.RecordingId });
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.ArtistAddedToRecordingMsg,
                artistData.ArtistCategory.ToStrongHtml(),
                artistData.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Artists), new { id = model.RecordingId });
        }

        public async Task<IActionResult> Formats(int id)
        {
            var recordingWithFormatsData = await this.recordingService
                .GetByIdAsync<AdminRecordingListingWithFormatsServiceModel>(id);

            recordingWithFormatsData.AvailableFormats = await this.recordingService
                .GetFormatsAsync(id);

            var formatsSelectList =
                (await this.formatService.AllAsync<AdminFormatBasicServiceModel>())
                .Select(f => new SelectListItem
                {
                    Text = f.Name,
                    Value = f.Id.ToString()
                })
                .OrderBy(a => a.Text)
                .ToList();

            return this.View(new RecordingFormatsListingModel
            {
                Recording = recordingWithFormatsData,
                Formats = formatsSelectList
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFormatFromRecording(int id, int formatId)
        {
            var recordingExists = await this.recordingService.ExistsAsync(id);
            if (!recordingExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.RecordingNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var formatExists = await this.formatService.ExistsAsync(formatId);
            if (!formatExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatNotFoundMsg);
                return this.RedirectToAction(nameof(Formats), new { id });
            }

            var formatData = await this.formatService
                            .GetByIdAsync<AdminFormatBasicServiceModel>(formatId);

            await this.recordingService.RemoveFormatAsync(id, formatId);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.FormatRemovedFromRecordingMsg,
                formatData.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Formats), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> AddFormatToRecording(AddFormatRecordingModel model)
        {
            var recordingExists = await this.recordingService.ExistsAsync(model.RecordingId);
            if (!recordingExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.RecordingNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var formatExists = await this.formatService.ExistsAsync(model.FormatId);
            if (!formatExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatNotFoundMsg);
                return this.RedirectToAction(nameof(Formats), new { id = model.RecordingId });
            }

            if (!this.ModelState.IsValid)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatInvalidDataMsg);
                return this.RedirectToAction(nameof(Formats), new { id = model.RecordingId });
            }

            var success = await this.recordingService.AddFormatAsync(
                model.RecordingId,
                model.FormatId,
                model.Price,
                model.Discount,
                model.Quantity);

            var formatData = await this.formatService
                            .GetByIdAsync<AdminFormatBasicServiceModel>(model.FormatId);

            if (!success)
            {
                this.TempData.AddErrorMessage(string.Format(
                    WebAdminConstants.FormatAlreadyAddedToRecordingMsg,
                    formatData.Name.ToStrongHtml()));

                return this.RedirectToAction(nameof(Formats), new { id = model.RecordingId });
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.FormatAddedToRecordingMsg,
                formatData.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Formats), new { id = model.RecordingId });
        }

        public async Task<IActionResult> Pricing(int id, int formatId)
        {
            var recordingExists = await this.recordingService.ExistsAsync(id);
            if (!recordingExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.RecordingNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var formatExists = await this.formatService.ExistsAsync(formatId);
            if (!formatExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatNotFoundMsg);
                return this.RedirectToAction(nameof(Formats), new { id });
            }

            var recordingFormatPricingData = await this.recordingService
                .GetPricingAsync(id, formatId);

            return this.View(new RecordingFormatPricingFormModel
            {
                Id = id,
                FormatId = formatId,
                Price = recordingFormatPricingData.Price,
                Discount = recordingFormatPricingData.Discount,
                Quantity = recordingFormatPricingData.Quantity
            });
        }

        [HttpPost]
        public async Task<IActionResult> Pricing(int id, RecordingFormatPricingFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var recordingExists = await this.recordingService.ExistsAsync(id);
            if (!recordingExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.RecordingNotFoundMsg);
                return this.RedirectToAction(nameof(Index));
            }

            var formatExists = await this.formatService.ExistsAsync(model.FormatId);
            if (!formatExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatNotFoundMsg);
                return this.RedirectToAction(nameof(Formats), new { id });
            }

            await this.recordingService.UpdateFormatPricing(
                id,
                model.FormatId,
                model.Price,
                model.Discount,
                model.Quantity);

            var recordingFormatData = await this.recordingService.GetRecordingFormatNames(id, model.FormatId);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.PricingUpdatedMsg,
                recordingFormatData.RecordingTitle.ToStrongHtml(),
                recordingFormatData.FormatName.ToStrongHtml()));

            return this.RedirectToAction(nameof(Formats), new { id });
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
