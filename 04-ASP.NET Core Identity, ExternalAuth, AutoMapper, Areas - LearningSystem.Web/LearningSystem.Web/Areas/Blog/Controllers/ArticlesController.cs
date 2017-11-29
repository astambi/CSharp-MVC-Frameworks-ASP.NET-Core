namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Articles;
    using Services.Blog;
    using Services.Html;
    using System.Threading.Tasks;

    [Area(WebConstants.BlogArea)]
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IHtmlService htmlService;
        private readonly IBlogService blogService;

        public ArticlesController(
            UserManager<User> userManager,
            IHtmlService htmlService,
            IBlogService blogService)
        {
            this.userManager = userManager;
            this.htmlService = htmlService;
            this.blogService = blogService;
        }

        public async Task<IActionResult> Index(int page = 1)
            => this.View(new ArticleListingViewModel
            {
                Articles = await this.blogService.AllAsync(page),
                TotalArticles = await this.blogService.TotalAsync(),
                CurrentPage = page
            });

        public async Task<IActionResult> Details(int id)
            => this.ViewOrNotFound(await this.blogService.ByIdAsync(id));

        [Authorize(Roles = WebConstants.BlogAuthorRole)]
        public IActionResult Create()
            => this.View();

        [Authorize(Roles = WebConstants.BlogAuthorRole)]
        [HttpPost]
        [ValidateModelState] // validates model state filter
        public async Task<IActionResult> Create(PublishArticleFormModel model)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    return this.View(model);
            //}

            var userId = this.userManager.GetUserId(this.User);
            model.Content = this.htmlService.Sanitize(model.Content);

            await this.blogService.CreateAsync(model.Title, model.Content, userId);

            return this.RedirectToAction(nameof(Index));
        }
    }
}
