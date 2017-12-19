namespace Prestissimo.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminLabelService : IAdminLabelService
    {
        private readonly PrestissimoDbContext db;

        public AdminLabelService(PrestissimoDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<TModel>> AllAsync<TModel>() where TModel : class
            => await this.db
                .Labels
                .OrderBy(l => l.Name)
                .ProjectTo<TModel>()
                .ToListAsync();

        public async Task CreateAsync(string name, string description)
        {
            var label = new Label
            {
                Name = name,
                Description = description
            };

            await this.db.Labels.AddAsync(label);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await this.db.Labels.AnyAsync(l => l.Id == id);

        public async Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class
             => await this.db
                .Labels
                .Where(l => l.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefaultAsync();

        public async Task RemoveAsync(int id)
        {
            var label = this.db.Labels.Find(id);
            if (label == null)
            {
                return;
            }

            this.db.Remove(label);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, string name, string description)
        {
            var label = this.db.Labels.Find(id);
            if (label == null)
            {
                return;
            }

            var isChanged = false;
            if (label.Name != name)
            {
                label.Name = name;
                isChanged = true;
            }

            if (label.Description != description)
            {
                label.Description = description;
                isChanged = true;
            }

            if (isChanged)
            {
                this.db.Labels.Update(label);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
