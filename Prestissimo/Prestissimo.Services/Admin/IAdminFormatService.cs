namespace Prestissimo.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminFormatService
    {
        Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class;

        Task<bool> CreateAsync(string name, string description);

        Task<bool> ExistsAsync(int id);

        Task<bool> ExistsAsync(string name);

        Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class;

        Task RemoveAsync(int id);

        Task<bool> UpdateAsync(int id, string name, string description);
    }
}
