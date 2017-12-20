namespace Prestissimo.Services
{
    using Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecordingService
    {
        Task<IEnumerable<CartItemWithDetailsServiceModel>> GetRecordingsAsync();
    }
}
