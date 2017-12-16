namespace Prestissimo.Services.Admin
{
    using Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminArtistService
    {
        Task<IEnumerable<TModel>> AllAsync<TModel>();

        Task CreateAsync(string name, string description, ArtistType artistType);

        Task<bool> ExistsAsync(int id);

        Task<TModel> GetByIdAsync<TModel>(int id);

        Task RemoveAsync(int id);

        Task UpdateAsync(int id, string name, string description, ArtistType artistType);
    }
}
