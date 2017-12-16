namespace Prestissimo.Web.Areas.Admin.Controllers
{
    using Admin.Models.Formats;
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin;
    using Services.Admin.Models.Formats;
    using System.Threading.Tasks;
    using Web.Infrastructure.Extensions;

    public class FormatsController : BaseAdminController
    {
        private readonly IAdminFormatService formatService;

        public FormatsController(IAdminFormatService formatService)
        {
            this.formatService = formatService;
        }

        public async Task<IActionResult> Index()
        {
            var formatsData = await this.formatService
                .AllAsync<AdminFormatListingServiceModel>();

            return this.View(formatsData);
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(FormatFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var success = await this.formatService.CreateAsync(
                model.Name,
                model.Description);

            if (!success)
            {
                this.TempData.AddErrorMessage(string.Format(
                    WebAdminConstants.FormatExistsMsg,
                    model.Name.ToStrongHtml()));

                return this.View(model);
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.FormatCreatedMsg,
                model.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var formatData = await this.formatService
                .GetByIdAsync<AdminFormatDetailsToModifyServiceModel>(id);

            if (formatData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(new FormatFormModel
            {
                Name = formatData.Name,
                Description = formatData.Description
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FormatFormModel model)
        {
            var formatExists = await this.formatService.ExistsAsync(id);
            if (!formatExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var success = await this.formatService.UpdateAsync(
                id,
                model.Name,
                model.Description);

            if (!success)
            {
                this.TempData.AddErrorMessage(string.Format(
                    WebAdminConstants.FormatExistsMsg,
                    model.Name.ToStrongHtml()));

                return this.View(model);
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.FormatUpdatedMsg,
                model.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var formatData = await this.formatService
                .GetByIdAsync<AdminFormatBasicServiceModel>(id);

            if (formatData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(formatData);
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var formatData = await this.formatService
                .GetByIdAsync<AdminFormatBasicServiceModel>(id);

            if (formatData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.FormatDeletedMsg,
                formatData.Name.ToStrongHtml()));

            await this.formatService.RemoveAsync(id);

            return this.RedirectToAction(nameof(Index));
        }
    }
}
