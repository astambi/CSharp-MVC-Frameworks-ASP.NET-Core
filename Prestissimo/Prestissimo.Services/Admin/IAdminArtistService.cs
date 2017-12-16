namespace Prestissimo.Services.Admin
{
    using Data.Models;
    using Services.Admin.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminArtistService
    {
        Task<IEnumerable<AdminArtistListingServiceModel>> AllAsync();

        Task CreateAsync(string name, string description, ArtistType artistType);

        Task<bool> Exists(int id);

        Task<Artist> GetByIdAsync(int id);

        Task UpdateAsync(int id, string name, string description, ArtistType artistType);

        Task RemoveAsync(int id);
    }
}
