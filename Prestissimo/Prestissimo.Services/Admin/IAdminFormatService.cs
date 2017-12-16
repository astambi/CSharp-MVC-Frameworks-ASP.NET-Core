namespace Prestissimo.Services.Admin
{
    using Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminFormatService
    {
        Task<IEnumerable<AdminFormatListingServiceModel>> AllAsync();

        Task<bool> CreateAsync(string name, string description);

        Task<bool> Exists(int id);

        Task<bool> Exists(string name);

        Task<Format> GetByIdAsync(int id);

        Task RemoveAsync(int id);

        Task<bool> UpdateAsync(int id, string name, string description);
    }
}
