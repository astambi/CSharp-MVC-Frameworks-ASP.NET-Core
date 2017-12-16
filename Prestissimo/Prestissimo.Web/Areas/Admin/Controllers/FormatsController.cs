namespace Prestissimo.Web.Areas.Admin.Controllers
{
    using Admin.Models.Formats;
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin;
    using System.Threading.Tasks;
    using Web.Infrastructure.Extensions;

    public class FormatsController : BaseAdminController
    {
        private readonly IAdminFormatService adminFormatService;

        public FormatsController(IAdminFormatService adminFormatService)
        {
            this.adminFormatService = adminFormatService;
        }

        public async Task<IActionResult> Index()
        {
            var formatsData = await this.adminFormatService.AllAsync();

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

            var success = await this.adminFormatService.CreateAsync(
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
            var formatData = await this.adminFormatService.GetByIdAsync(id);
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
            var formatData = await this.adminFormatService.GetByIdAsync(id);
            if (formatData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var success = await this.adminFormatService.UpdateAsync(
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
            var formatData = await this.adminFormatService.GetByIdAsync(id);
            if (formatData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(new FormatDeleteModel
            {
                Id = id,
                Name = formatData.Name
            });
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var formatData = await this.adminFormatService.GetByIdAsync(id);
            if (formatData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.FormatNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.FormatDeletedMsg,
                formatData.Name.ToStrongHtml()));

            await this.adminFormatService.RemoveAsync(id);

            return this.RedirectToAction(nameof(Index));
        }
    }
}
