namespace Prestissimo.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminRecordingService : IAdminRecordingService
    {
        private readonly PrestissimoDbContext db;

        public AdminRecordingService(PrestissimoDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<TModel>> AllAsync<TModel>()
            => await this.db
                .Recordings
                .OrderByDescending(r => r.ReleaseDate)
                .ThenBy(r => r.Title)
                .ProjectTo<TModel>()
                .ToListAsync();

        public async Task CreateAsync(
            string title,
            string description,
            DateTime releaseDate,
            int length,
            string imageUrl,
            int labelId)
        {
            var labelExists = this.db.Labels.Any(l => l.Id == labelId);
            if (!labelExists)
            {
                return;
            }

            var recording = new Recording
            {
                Title = title,
                Description = description,
                ReleaseDate = releaseDate,
                Length = length,
                ImageUrl = imageUrl,
                LabelId = labelId
            };

            await this.db.Recordings.AddAsync(recording);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await this.db.Recordings.AnyAsync(r => r.Id == id);

        public async Task<TModel> GetByIdAsync<TModel>(int id)
            => await this.db
                .Recordings
                .Where(r => r.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefaultAsync();

        public async Task RemoveAsync(int id)
        {
            var recording = this.db.Recordings.Find(id);
            if (recording == null)
            {
                return;
            }

            this.db.Remove(recording);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateAsync(
            int id, 
            string title, string description, DateTime releaseDate, int length, string imageUrl, int labelId)
        {
            var recording = this.db.Recordings.Find(id);
            if (recording == null)
            {
                return;
            }

            var labelExists = this.db.Labels.Any(l => l.Id == labelId);
            if (!labelExists)
            {
                return;
            }

            var isChanged = false;
            if (recording.Title != title)
            {
                recording.Title = title;
                isChanged = true;
            }

            if (recording.Description != description)
            {
                recording.Description = description;
                isChanged = true;
            }

            if (recording.ReleaseDate != releaseDate)
            {
                recording.ReleaseDate = releaseDate;
                isChanged = true;
            }

            if (recording.Length != length)
            {
                recording.Length = length;
                isChanged = true;
            }

            if (recording.ImageUrl != imageUrl)
            {
                recording.ImageUrl = imageUrl;
                isChanged = true;
            }

            if (recording.LabelId != labelId)
            {
                recording.LabelId = labelId;
                isChanged = true;
            }

            if (isChanged)
            {
                this.db.Recordings.Update(recording);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
