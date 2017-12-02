namespace BookShop.Services
{
    using Models.Authors;
    using Models.Books;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAuthorService
    {
        Task<IEnumerable<BookWithCategoriesServiceModel>> All(int authorId);

        Task<int> Create(string firstName, string lastName);

        Task<AuthorDetailsServiceModel> Details(int id);

        Task<bool> Exists(int id);
    }
}
