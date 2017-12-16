namespace Prestissimo.Services.Admin
{
    using Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminLabelService
    {
        Task<IEnumerable<AdminLabelListingServiceModel>> AllAsync();

        Task CreateAsync(string name, string description);

        Task<bool> Exists(int id);

        Task<Label> GetByIdAsync(int id);

        Task RemoveAsync(int id);

        Task UpdateAsync(int id, string name, string description);
    }
}
