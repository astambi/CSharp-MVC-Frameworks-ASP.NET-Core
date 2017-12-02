namespace BookShop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
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

        public async Task<CategoryServiceModel> Details(int id)
            => await this.db
            .Categories
            .Where(c => c.Id == id)
            .ProjectTo<CategoryServiceModel>()
            .FirstOrDefaultAsync();
    }
}
