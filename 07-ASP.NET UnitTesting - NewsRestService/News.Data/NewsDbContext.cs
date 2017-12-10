namespace News.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options)
            : base(options)
        {
        }

        public DbSet<News> News { get; set; }
    }
}
