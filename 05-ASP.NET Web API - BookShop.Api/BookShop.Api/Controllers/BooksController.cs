namespace BookShop.Api.Controllers
{
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models.Books;
    using Services;
    using System.Threading.Tasks;
    using static WebConstants;

    public class BooksController : BaseApiController
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;

        public BooksController(
            IBookService bookService,
            IAuthorService authorService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.bookService.Details(id));

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string search = "")
            => this.OkOrNotFound(await this.bookService.All(search));

        [HttpDelete(WithId)]
        public async Task<IActionResult> Delete(int id)
        {
            var bookExists = await this.bookService.Exists(id);
            if (!bookExists)
            {
                return NotFound("Book does not exist.");
            }

            await this.bookService.Delete(id);

            return this.Ok();
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]BookWithCategoriesRequestModel model)
        {
            var authorExists = await this.authorService.Exists(model.AuthorId);
            if (!authorExists)
            {
                return BadRequest("Author does not exist.");
            }

            var id = await this.bookService.Create(
                model.Title.Trim(),
                model.Description.Trim(),
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId,
                model.Categories);

            return Ok(id);
        }

        [HttpPut(WithId)]
        [ValidateModelState]
        public async Task<IActionResult> Put(int id, [FromBody]BookRequestModel model)
        {
            var bookExists = await this.bookService.Exists(id);
            if (!bookExists)
            {
                return NotFound("Book does not exist.");
            }

            var authorExists = await this.authorService.Exists(model.AuthorId);
            if (!authorExists)
            {
                return BadRequest("Author does not exist.");
            }

            await this.bookService.Update(
                id,
                model.Title.Trim(),
                model.Description.Trim(),
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId);

            return Ok();
        }
    }
}
