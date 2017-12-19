namespace Prestissimo.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Prestissimo.Services.Admin.Models.Formats;
    using Prestissimo.Services.Admin.Models.Recordings;
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

        public async Task<bool> AddArtistAsync(int id, int artistId)
        {
            var recording = this.db.Recordings.Find(id);
            var artist = this.db.Artists.Find(artistId);
            var recordingArtist = this.db.RecordingArtists.Find(id, artistId);

            if (recording == null
                || artist == null
                || recordingArtist != null)
            {
                return false;
            }

            recording.Artists.Add(new RecordingArtist
            {
                ArtistId = artistId
            });

            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddFormatAsync(int id, int formatId, decimal price, double discount, int quantity)
        {
            var recording = this.db.Recordings.Find(id);
            var format = this.db.Formats.Find(formatId);
            var recordingFormat = this.db.RecordingFormats.Find(id, formatId);

            if (recording == null
                || format == null
                || recordingFormat != null)
            {
                return false;
            }

            recording.Formats.Add(new RecordingFormat
            {
                FormatId = formatId,
                Price = price,
                Discount = discount,
                Quantity = quantity
            });

            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class
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

        // TODO NB! Check for potential errors in previously error-free queries! 
        // Used to select Artists & Formats along with Recording Data
        public async Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class
            => await this.db
                .Recordings
                .Where(r => r.Id == id)
                .ProjectTo<TModel>(new { id }) // TODO test for errors in working code
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<AdminFormatPriceQuantityServiceModel>> GetFormatsAsync(int id)
        {
            if (!await this.ExistsAsync(id))
            {
                return null;
            }

            return await this.db
                .Recordings
                .Where(r => r.Id == id)
                .Select(r => r
                    .Formats
                    .Select(rf => new AdminFormatPriceQuantityServiceModel
                    {
                        Id = rf.FormatId,
                        Name = rf.Format.Name,
                        Price = rf.Price,
                        Discount = rf.Discount,
                        Quantity = rf.Quantity
                    })
                    .ToList())
                .FirstOrDefaultAsync();
        }

        public async Task<AdminFormatPriceQuantityServiceModel> GetPricingAsync(int id, int formatId)
        {
            var recordingFormatExists = this.db
                .RecordingFormats
                .Any(rf => rf.RecordingId == id
                        && rf.FormatId == formatId);

            if (!recordingFormatExists)
            {
                return null;
            }

            return await this.db
                .RecordingFormats
                .Where(rf => rf.RecordingId == id
                          && rf.FormatId == formatId)
                .ProjectTo<AdminFormatPriceQuantityServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task<AdminRecordingFormatNamesServiceModel> GetRecordingFormatNames(int id, int formatId)
        {
            var recordingFormat = this.db.RecordingFormats.Find(id, formatId);
            if (recordingFormat == null)
            {
                return null;
            }

            return await this.db
                .RecordingFormats
                .Where(rf => rf.RecordingId == id
                          && rf.FormatId == formatId)
                .ProjectTo<AdminRecordingFormatNamesServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task RemoveArtistAsync(int id, int artistId)
        {
            var recordingArtist = this.db.RecordingArtists.Find(id, artistId);
            if (recordingArtist == null)
            {
                return;
            }

            this.db.Remove(recordingArtist);
            await this.db.SaveChangesAsync();
        }

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

        public async Task RemoveFormatAsync(int id, int formatId)
        {
            var recordingFormat = this.db.RecordingFormats.Find(id, formatId);
            if (recordingFormat == null)
            {
                return;
            }

            this.db.Remove(recordingFormat);
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

        public async Task UpdateFormatPricing(int id, int formatId, decimal price, double discount, int quantity)
        {
            var recordingFormat = this.db.RecordingFormats.Find(id, formatId);
            if (recordingFormat == null)
            {
                return;
            }

            var isChanged = false;
            if (recordingFormat.Price != price)
            {
                recordingFormat.Price = price;
                isChanged = true;
            }

            if (recordingFormat.Discount != discount)
            {
                recordingFormat.Discount = discount;
                isChanged = true;
            }

            if (recordingFormat.Quantity != quantity)
            {
                recordingFormat.Quantity = quantity;
                isChanged = true;
            }

            if (isChanged)
            {
                this.db.RecordingFormats.Update(recordingFormat);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
