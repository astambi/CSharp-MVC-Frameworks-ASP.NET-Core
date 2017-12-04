namespace BookShop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using BookShop.Models;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Categories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly BookShopDbContext db;

        public CategoryService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CategoryServiceModel>> All()
            => await this.db
            .Categories
            .ProjectTo<CategoryServiceModel>()
            .ToListAsync();

        public async Task<int> Create(string name)
        {
            var category = new Category { Name = name };

            await this.db.Categories.AddAsync(category);
            await this.db.SaveChangesAsync();

            return category.Id;
        }

        public async Task Delete(int id)
        {
            var category = await this.db.Categories.FindAsync(id);
            if (category == null)
            {
                return;
            }

            this.db.Remove(category);
            await this.db.SaveChangesAsync();
        }

        public async Task<CategoryServiceModel> Details(int id)
            => await this.db
            .Categories
            .Where(c => c.Id == id)
            .ProjectTo<CategoryServiceModel>()
            .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await this.db
            .Categories
            .AnyAsync(c => c.Id == id);

        public async Task<bool> Exists(int id, string name)
            => await this.db
            .Categories
            .Where(c => c.Id != id)
            .AnyAsync(c => c.Name.ToLower() == name.ToLower());

        public async Task<bool> Exists(string name)
            => await this.db
            .Categories
            .AnyAsync(c => c.Name.ToLower() == name.ToLower());

        public async Task Update(int id, string name)
        {
            var category = await this.db.Categories.FindAsync(id);
            if (category == null)
            {
                return;
            }

            if (category.Name != name)
            {
                category.Name = name;

                await this.db.SaveChangesAsync();
            }
        }
    }
}
