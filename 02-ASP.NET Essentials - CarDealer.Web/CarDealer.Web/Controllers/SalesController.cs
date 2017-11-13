namespace CarDealer.Web.Controllers
{
    using Infrastructure.Extensions;
    using Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService saleService;

        public SalesController(ISaleService saleService)
        {
            this.saleService = saleService;
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
    }
}
