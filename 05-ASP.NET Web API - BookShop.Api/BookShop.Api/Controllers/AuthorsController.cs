namespace BookShop.Api.Controllers
{
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models.Authors;
    using Services;
    using System.Threading.Tasks;

    using static WebConstants;

    public class AuthorsController : BaseApiController
    {
        private readonly IAuthorService authorService;
        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.authorService.Details(id));

        [HttpGet(WithId + "/books")]
        public async Task<IActionResult> GetBooks(int id)
            => this.OkOrNotFound(await this.authorService.All(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]AuthorRequestModel model)
        {
            var id = await this.authorService.Create(
                model.FirstName.Trim(),
                model.LastName.Trim());

            return Ok(id);
        }
    }
}
