namespace CarDealer.Web.Controllers
{
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
    }
}
