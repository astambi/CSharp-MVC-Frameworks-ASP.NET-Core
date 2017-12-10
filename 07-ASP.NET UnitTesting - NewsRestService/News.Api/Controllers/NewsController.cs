namespace News.Api.Controllers
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models.News;
    using System.Linq;

    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private readonly NewsDbContext db;

        public NewsController(NewsDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetAllNews()
        {
            return this.Ok(this.db.News.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetSingleNews([FromRoute]int id)
        {
            var news = this.db.News.Find(id);
            if (news == null)
            {
                return this.NotFound();
            }

            return this.Ok(news);
        }

        [HttpPost]
        public IActionResult PostNews([FromBody]NewsModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var news = new News
            {
                Title = model.Title,
                Content = model.Content,
                PublishDate = model.PublishDate
            };

            this.db.News.Add(news);
            this.db.SaveChanges();

            return this.CreatedAtAction(nameof(GetSingleNews), new { id = news.Id }, news);
        }

        [HttpPut("{id}")]
        public IActionResult PutNews([FromRoute]int id, [FromBody]NewsModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newsToUpdate = this.db.News.Find(id);
            if (newsToUpdate == null)
            {
                return this.BadRequest();
            }

            var isChanged = false;

            if (newsToUpdate.Title != model.Title)
            {
                newsToUpdate.Title = model.Title;
                isChanged = true;
            }

            if (newsToUpdate.Content != model.Content)
            {
                newsToUpdate.Content = model.Content;
                isChanged = true;
            }

            if (newsToUpdate.PublishDate != model.PublishDate)
            {
                newsToUpdate.PublishDate = model.PublishDate;
                isChanged = true;
            }

            if (isChanged)
            {
                this.db.News.Update(newsToUpdate);
                this.db.SaveChanges();
            }

            return this.Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNews([FromRoute]int id)
        {
            var newsToDelete = this.db.News.Find(id);
            if (newsToDelete == null)
            {
                return this.BadRequest();
            }

            this.db.News.Remove(newsToDelete);
            this.db.SaveChanges();

            return this.Ok();
        }
    }
}
