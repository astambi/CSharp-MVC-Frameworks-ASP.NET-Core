namespace BookShop.Services
{
    using Models.Categories;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryServiceModel>> All();

        Task<CategoryServiceModel> Details(int id);
    }
}
