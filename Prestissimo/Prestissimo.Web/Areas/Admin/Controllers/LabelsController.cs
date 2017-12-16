namespace Prestissimo.Web.Areas.Admin.Controllers
{
    using Admin.Models.Labels;
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin;
    using System.Threading.Tasks;
    using Web.Infrastructure.Extensions;

    public class LabelsController : BaseAdminController
    {
        private readonly IAdminLabelService adminLabelService;

        public LabelsController(IAdminLabelService adminLabelService)
        {
            this.adminLabelService = adminLabelService;
        }

        public async Task<IActionResult> Index()
        {
            var labelData = await this.adminLabelService.AllAsync();

            return this.View(labelData);
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(LabelFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.adminLabelService.CreateAsync(
                model.Name,
                model.Description);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.LabelCreatedMsg,
                model.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var labelData = await this.adminLabelService.GetByIdAsync(id);
            if (labelData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.LabelNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(new LabelFormModel
            {
                Name = labelData.Name,
                Description = labelData.Description
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LabelFormModel model)
        {
            var labelData = await this.adminLabelService.GetByIdAsync(id);
            if (labelData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.LabelNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.adminLabelService.UpdateAsync(
                id,
                model.Name,
                model.Description);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.LabelUpdatedMsg,
                model.Name.ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var labelData = await this.adminLabelService.GetByIdAsync(id);
            if (labelData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.LabelNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(new LabelDeleteModel
            {
                Id = id,
                Name = labelData.Name
            });
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var labelData = await this.adminLabelService.GetByIdAsync(id);
            if (labelData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ArtistNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.LabelDeletedMsg,
                labelData.Name.ToStrongHtml()));

            await this.adminLabelService.RemoveAsync(id);

            return this.RedirectToAction(nameof(Index));
        }
    }
}
