namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Cars;
    using Services;
    using System.Collections.Generic;
    using System.Linq;

    [Route("cars")]
    public class CarsController : Controller
    {
        private const string All = "all";

        private readonly ICarService carService;
        private readonly IPartService partService;

        public CarsController(
            ICarService carService,
            IPartService partService)
        {
            this.carService = carService;
            this.partService = partService;
        }

        // cars/All
        // cars/BMW
        [Route("{make}", Order = 2)]
        public IActionResult ByMake(string make)
        {
            var makeFilter = make.ToLower() == All ? null : make;

            var cars = this.carService.AllByMake(makeFilter);

            var model = new CarsByMakeModel
            {
                Cars = cars,
                Make = make
            };

            return this.View(model);
        }

        [Route(nameof(Parts), Order = 1)]
        public IActionResult Parts()
        {
            var carsWithParts = this.carService.AllWithParts();

            return this.View(carsWithParts);
        }

        [Authorize]
        [Route(nameof(Create), Order = 1)]
        public IActionResult Create()
        {
            var model = new CarFormModel
            {
                PartsDropdown = this.GetPartsSelectListItem()
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Create), Order = 1)]
        public IActionResult Create(CarFormModel carModel) // NB!!! model property & CarFormModel binding mismatch!
        {
            if (!this.ModelState.IsValid)
            {
                carModel.PartsDropdown = this.GetPartsSelectListItem();
                return this.View(carModel);
            }

            this.carService.Create(
                carModel.Make,
                carModel.Model,
                carModel.TravelledDistance,
                carModel.SelectedParts);

            return this.RedirectToAction(
                nameof(ByMake),
                new { make = carModel.Make });
        }

        public IEnumerable<SelectListItem> GetPartsSelectListItem()
        {
            return this.partService
                    .AllBasic()
                    .Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString()
                    });
        }
    }
}
