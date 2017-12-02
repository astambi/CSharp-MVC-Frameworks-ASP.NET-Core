namespace BookShop.Api.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;
    using static WebConstants;

    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => this.OkOrNotFound(await this.categoryService.All());

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
           => this.OkOrNotFound(await this.categoryService.Details(id));



    }
}
