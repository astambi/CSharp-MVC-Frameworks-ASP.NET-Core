namespace Prestissimo.Web.Areas.Admin.Controllers
{
    using Admin.Models.Artists;
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin;
    using Services.Admin.Models.Artists;
    using System.Threading.Tasks;
    using Web.Infrastructure.Extensions;

    public class ArtistsController : BaseAdminController
    {
        private readonly IAdminArtistService artistService;

        public ArtistsController(IAdminArtistService artistService)
        {
            this.artistService = artistService;
        }

        public async Task<IActionResult> Index()
        {
            var artistsData = await this.artistService
                .AllAsync<AdminArtistListingServiceModel>();

            return this.View(artistsData);
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(ArtistFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.artistService.CreateAsync(
                model.Name,
                model.Description,
                model.ArtistType);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.ArtistCreatedMsg,
                model.Name.ToStrongHtml(),
                model.ArtistType.ToString().ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var artistData = await this.artistService
                .GetByIdAsync<AdminArtistDetailsToModifyServiceModel>(id);

            if (artistData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ArtistNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(new ArtistFormModel
            {
                Name = artistData.Name,
                Description = artistData.Description,
                ArtistType = artistData.ArtistType
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ArtistFormModel model)
        {
            var artistExists = await this.artistService.ExistsAsync(id);
            if (!artistExists)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ArtistNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.artistService.UpdateAsync(
                id,
                model.Name,
                model.Description,
                model.ArtistType);

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.ArtistUpdatedMsg,
                model.Name.ToStrongHtml(),
                model.ArtistType.ToString().ToStrongHtml()));

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var artistData = await this.artistService
                .GetByIdAsync<AdminArtistBasicServiceModel>(id);

            if (artistData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ArtistNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            return this.View(artistData);
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var artistData = await this.artistService
                .GetByIdAsync<AdminArtistBasicServiceModel>(id);

            if (artistData == null)
            {
                this.TempData.AddErrorMessage(WebAdminConstants.ArtistNotFoundMsg);
                return RedirectToAction(nameof(Index));
            }

            this.TempData.AddSuccessMessage(string.Format(
                WebAdminConstants.ArtistDeletedMsg,
                artistData.Name.ToStrongHtml(),
                artistData.ArtistCategory.ToStrongHtml()));

            await this.artistService.RemoveAsync(id);

            return this.RedirectToAction(nameof(Index));
        }
    }
}
