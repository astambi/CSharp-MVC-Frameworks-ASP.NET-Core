namespace FDMC.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CatsDbContext : DbContext
    {
        public CatsDbContext(DbContextOptions<CatsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }
    }
}
