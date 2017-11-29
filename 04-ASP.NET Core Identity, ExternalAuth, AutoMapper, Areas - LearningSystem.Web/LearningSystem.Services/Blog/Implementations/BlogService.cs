namespace LearningSystem.Services.Blog.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Blog.Models;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BlogService : IBlogService
    {
        private readonly LearningSystemDbContext db;

        public BlogService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<ArticleDetailsServiceModel> ByIdAsync(int id)
            => await this.db
            .Articles
            .Where(a => a.Id == id)
            .ProjectTo<ArticleDetailsServiceModel>()
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<ArticleListingServiceModel>> AllAsync(int page = 1)
            => await this.db
            .Articles
            .OrderByDescending(a => a.PublishDate)
            .Skip((page - 1) * ServiceConstants.ArticlesListingPageSize)
            .Take(ServiceConstants.ArticlesListingPageSize)
            .ProjectTo<ArticleListingServiceModel>()
            .ToListAsync();

        public async Task CreateAsync(string title, string content, string authorId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                AuthorId = authorId,
                PublishDate = DateTime.UtcNow
            };

            await this.db.Articles.AddAsync(article);
            await this.db.SaveChangesAsync();
        }

        public async Task<int> TotalAsync()
            => await this.db.Articles.CountAsync();
    }
}
