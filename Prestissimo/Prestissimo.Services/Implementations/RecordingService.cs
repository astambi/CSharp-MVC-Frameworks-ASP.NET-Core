namespace Prestissimo.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RecordingService : IRecordingService
    {
        private readonly PrestissimoDbContext db;

        public RecordingService(PrestissimoDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CartItemWithDetailsServiceModel>> GetRecordingsAsync()
            => await this.db
                .RecordingFormats
                .ProjectTo<CartItemWithDetailsServiceModel>()
                .ToListAsync();
    }
}
