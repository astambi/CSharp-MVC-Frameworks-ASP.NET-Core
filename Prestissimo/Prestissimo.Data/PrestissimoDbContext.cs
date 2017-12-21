namespace Prestissimo.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PrestissimoDbContext : IdentityDbContext<User>
    {
        public PrestissimoDbContext(DbContextOptions<PrestissimoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Format> Formats { get; set; }

        public DbSet<Label> Labels { get; set; }

        public DbSet<Recording> Recordings { get; set; }

        public DbSet<RecordingArtist> RecordingArtists { get; set; }

        public DbSet<RecordingFormat> RecordingFormats { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Add your customizations after calling base.OnModelCreating(builder);

            builder
                .Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(a => a.Articles)
                .HasForeignKey(a => a.AuthorId);

            builder
                .Entity<Article>()
                .HasOne(a => a.Recording)
                .WithMany(r => r.Articles)
                .HasForeignKey(a => a.RecordingId);

            builder
                .Entity<Recording>()
                .HasOne(r => r.Label)
                .WithMany(l => l.Recordings)
                .HasForeignKey(r => r.LabelId);

            builder
                .Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            builder
                .Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            // Many-to-Many
            builder
                .Entity<RecordingArtist>()
                .HasKey(ra => new { ra.RecordingId, ra.ArtistId });

            builder
                .Entity<RecordingArtist>()
                .HasOne(ra => ra.Artist)
                .WithMany(a => a.Recordings)
                .HasForeignKey(ra => ra.ArtistId);

            builder
                .Entity<RecordingArtist>()
                .HasOne(ra => ra.Recording)
                .WithMany(r => r.Artists)
                .HasForeignKey(ra => ra.RecordingId);

            builder
               .Entity<RecordingFormat>()
               .HasKey(rf => new { rf.RecordingId, rf.FormatId });

            builder
              .Entity<RecordingFormat>()
              .HasOne(rf => rf.Recording)
              .WithMany(r => r.Formats)
              .HasForeignKey(rf => rf.RecordingId);

            builder
              .Entity<RecordingFormat>()
              .HasOne(rf => rf.Format)
              .WithMany(f => f.Recordings)
              .HasForeignKey(rf => rf.FormatId);
        }
    }
}
