namespace Prestissimo.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Services.Admin.Models;
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

        public async Task<IEnumerable<AdminArtistListingServiceModel>> AllAsync()
        {
            return await this.db
                .Artists
                .OrderBy(a => a.ArtistType)
                .ThenBy(a => a.Name)
                .ProjectTo<AdminArtistListingServiceModel>()
                .ToListAsync();
        }

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

        public async Task<bool> Exists(int id)
            => await this.db.Artists.AnyAsync(a => a.Id == id);

        public async Task<Artist> GetByIdAsync(int id)
            => await this.db
            .Artists
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();

        public async Task RemoveAsync(int id)
        {
            var artist = await this.GetByIdAsync(id);
            if (artist == null)
            {
                return;
            }

            this.db.Remove(artist);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateAsync(
            int id,
            string name, string description, ArtistType artistType)
        {
            var artist = await this.GetByIdAsync(id);
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
