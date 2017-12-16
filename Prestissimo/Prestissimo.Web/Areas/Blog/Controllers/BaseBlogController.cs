namespace Prestissimo.Web.Areas.Blog.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(WebConstants.BlogArea)]
    [Authorize(Roles = WebConstants.BlogAuthorRole)]
    public class BaseBlogController
    {
    }
}
