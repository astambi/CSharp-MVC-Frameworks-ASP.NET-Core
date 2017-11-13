namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Cars;
    using Services;

    [Route("cars")]
    public class CarsController : Controller
    {
        private const string All = "all";

        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
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

        [Route(nameof(Create), Order = 1)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Route(nameof(Create), Order = 1)]
        public IActionResult Create(CarFormModel carModel) // NB!!! model property & CarFormModel binding mismatch!
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(carModel);
            }

            this.carService.Create(
                carModel.Make,
                carModel.Model,
                carModel.TravelledDistance);

            return this.RedirectToAction(
                nameof(ByMake),
                new { make = carModel.Make });
        }



    }
}
