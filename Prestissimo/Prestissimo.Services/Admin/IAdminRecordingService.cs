namespace Prestissimo.Services.Admin
{
    using Services.Admin.Models.Formats;
    using Services.Admin.Models.Recordings;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminRecordingService
    {
        Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class;

        Task CreateAsync(
            string title, string description, DateTime releaseDate, 
            int length, string imageUrl, int labelId);

        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<AdminFormatPriceQuantityServiceModel>> GetFormatsAsync(int id);

        Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class;

        Task<AdminFormatPriceQuantityServiceModel> GetPricingAsync(int id, int formatId); // todo

        Task<AdminRecordingFormatNamesServiceModel> GetRecordingFormatNames(int id, int formatId);

        Task RemoveAsync(int id);

        Task<bool> AddArtistAsync(int id, int artistId);

        Task<bool> AddFormatAsync(int id, int formatId, decimal price, double discount, int quantity);

        Task RemoveArtistAsync(int id, int artistId);

        Task RemoveFormatAsync(int id, int formatId);

        Task UpdateAsync(int id, 
            string title, string description, DateTime releaseDate,
            int length, string imageUrl, int labelId);

        Task UpdateFormatPricing(int id, int formatId, decimal price, double discount, int quantity);
    }
}
