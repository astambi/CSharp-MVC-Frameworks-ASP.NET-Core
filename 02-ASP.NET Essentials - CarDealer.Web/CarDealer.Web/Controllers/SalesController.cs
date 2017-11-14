namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Sales;
    using Services;
    using Services.Models.Sales;
    using System.Collections.Generic;
    using System.Linq;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService saleService;
        private readonly ICustomerService customerService;
        private readonly ICarService carService;

        public SalesController(
            ISaleService saleService,
            ICustomerService customerService,
            ICarService carService)
        {
            this.saleService = saleService;
            this.customerService = customerService;
            this.carService = carService;
        }

        [Route("")]
        public IActionResult All()
        {
            var sales = this.saleService.All();

            return this.View(sales);
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            //return this.ViewOrNotFound(this.saleService.ById(id));

            var sale = this.saleService.ById(id);

            return this.View(sale);
        }

        [Route("discounted")]
        public IActionResult Discounted()
        {
            var sales = this.saleService.Discounted(null);

            return this.View(sales);
        }

        [Route("discounted/{percentage}")]
        public IActionResult Discounted(int percentage)
        {
            var sales = this.saleService.Discounted(percentage);

            return this.View(sales);
        }

        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create()
        {
            var model = new SaleFormModel
            {
                CarsDropdown = this.GetCarsSelectListItem(),
                CustomersDropdown = this.GetCustomersSelectListItem()
            };

            return this.View(model);
        }

        [Authorize]
        [Route(nameof(ReviewCreate))]
        public IActionResult ReviewCreate(SaleFormModel saleModel)
        {
            if (!this.ModelState.IsValid)
            {
                saleModel.CarsDropdown = this.GetCarsSelectListItem();
                saleModel.CustomersDropdown = this.GetCustomersSelectListItem();
                return this.View(saleModel);
            }

            var saleReviewModel = this.saleService.SaleReview(
                saleModel.CarId,
                saleModel.CustomerId,
                saleModel.Discount);

            return this.View(saleReviewModel);
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(FinalizeCreate))]
        public IActionResult FinalizeCreate(SaleReviewModel saleModel)
        {
            if (!this.ModelState.IsValid)
            {
                var createModel = new SaleFormModel
                {
                    CarsDropdown = this.GetCarsSelectListItem(),
                    CustomersDropdown = this.GetCustomersSelectListItem()
                };
                return this.RedirectToAction(nameof(Create), createModel);
            }

            this.saleService.Create(
                saleModel.CustomerId,
                saleModel.CarId,
                saleModel.Discount);

            return this.RedirectToAction(nameof(All));
        }

        private IEnumerable<SelectListItem> GetCarsSelectListItem()
        {
            return this.carService
                .AllBasic()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Make} {c.Model}"
                });
        }

        private IEnumerable<SelectListItem> GetCustomersSelectListItem()
        {
            return this.customerService
                .AllBasic()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
        }
    }
}
