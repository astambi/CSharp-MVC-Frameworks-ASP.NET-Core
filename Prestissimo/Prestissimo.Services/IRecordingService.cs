namespace Prestissimo.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecordingService
    {
        Task<IEnumerable<CartItemWithDetailsServiceModel>> GetRecordingsAsync();

        Task<RecordingDetailsServiceModel> Details(int id);

        Task<bool> Exists(int id);
    }
}
