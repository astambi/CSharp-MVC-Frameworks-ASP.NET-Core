namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Customers;
    using Services;
    using Services.Models;

    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [Route("all/{order=ascending}")]
        public IActionResult All(string order)
        {
            var orderDirection =
                order.ToLower() == OrderDirection.Descending.ToString().ToLower()
                ? OrderDirection.Descending
                : OrderDirection.Ascending;

            var customers = this.customerService.AllOrdered(orderDirection);

            var model = new AllCustomersModel
            {
                Customers = customers,
                OrderDirection = orderDirection
            };

            return this.View(model);
        }

        [Route("{id}")]
        public IActionResult TotalSales(int id)
        {
            var totalSales = this.customerService.TotalSalesById(id);

            return this.View(totalSales);
        }

        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CustomerFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.customerService.Create(
                model.Name,
                model.BirthDate,
                model.IsYoungDriver);

            return RedirectToAction(
                nameof(All),
                new { order = OrderDirection.Ascending.ToString() });
        }

        [Authorize]
        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var custmerExists = this.customerService.Exists(id);
            if (!custmerExists)
            {
                return RedirectToAction(nameof(All));
            }

            var customer = this.customerService.GetById(id);

            var model = new CustomerFormModel
            {
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                IsYoungDriver = customer.IsYoungDriver
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id, CustomerFormModel model)
        {
            var custmerExists = this.customerService.Exists(id);
            if (!custmerExists)
            {
                return RedirectToAction(nameof(All));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.customerService.Update(
                id,
                model.Name,
                model.BirthDate,
                model.IsYoungDriver);

            return RedirectToAction(
                nameof(All),
                new { order = OrderDirection.Ascending.ToString() });
        }
    }
}
