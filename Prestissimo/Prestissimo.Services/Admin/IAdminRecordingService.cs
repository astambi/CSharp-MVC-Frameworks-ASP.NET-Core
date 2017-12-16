namespace Prestissimo.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminRecordingService
    {
        Task<IEnumerable<TModel>> AllAsync<TModel>();

        Task CreateAsync(
            string title, string description, DateTime releaseDate, 
            int length, string imageUrl, int labelId);

        Task<bool> ExistsAsync(int id);

        Task<TModel> GetByIdAsync<TModel>(int id);

        Task RemoveAsync(int id);

        Task UpdateAsync(int id, 
            string title, string description, DateTime releaseDate,
            int length, string imageUrl, int labelId);
    }
}
