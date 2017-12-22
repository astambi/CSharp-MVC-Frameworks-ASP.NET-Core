namespace Prestissimo.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RecordingService : IRecordingService
    {
        private readonly PrestissimoDbContext db;

        public RecordingService(PrestissimoDbContext db)
        {
            this.db = db;
        }

        public async Task<RecordingDetailsServiceModel> Details(int id)
        {
            if (!await this.Exists(id))
            {
                return null;
            }

            return await this.db
                .Recordings
                .Where(r => r.Id == id)
                .ProjectTo<RecordingDetailsServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Exists(int id)
            => await this.db.Recordings.AnyAsync(r => r.Id == id);

        public async Task<IEnumerable<CartItemWithDetailsServiceModel>> GetRecordingsAsync()
            => await this.db
                .RecordingFormats
                .ProjectTo<CartItemWithDetailsServiceModel>()
                .OrderByDescending(rf => rf.ReleaseDate)
                .ThenBy(rf => rf.RecordingTitle)
                .ToListAsync();
    }
}
