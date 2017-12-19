namespace Prestissimo.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminArtistService : IAdminArtistService
    {
        private readonly PrestissimoDbContext db;

        public AdminArtistService(PrestissimoDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class
            => await this.db
                .Artists
                .OrderBy(a => a.ArtistType)
                .ThenBy(a => a.Name)
                .ProjectTo<TModel>()
                .ToListAsync();

        public async Task CreateAsync(
            string name, string description, ArtistType artistType)
        {
            var artist = new Artist
            {
                Name = name,
                ArtistType = artistType,
                Description = description
            };

            await this.db.Artists.AddAsync(artist);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await this.db.Artists.AnyAsync(a => a.Id == id);

        public async Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class
            => await this.db
                .Artists
                .Where(a => a.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefaultAsync();

        public async Task RemoveAsync(int id)
        {
            var artist = this.db.Artists.Find(id);
            if (artist == null)
            {
                return;
            }

            this.db.Remove(artist);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, string name, string description, ArtistType artistType)
        {
            var artist = this.db.Artists.Find(id);
            if (artist == null)
            {
                return;
            }

            var isChanged = false;
            if (artist.Name != name)
            {
                artist.Name = name;
                isChanged = true;
            }

            if (artist.Description != description)
            {
                artist.Description = description;
                isChanged = true;
            }

            if (artist.ArtistType != artistType)
            {
                artist.ArtistType = artistType;
                isChanged = true;
            }

            if (isChanged)
            {
                this.db.Artists.Update(artist);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
