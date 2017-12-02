namespace BookShop.Services
{
    using Models.Authors;
    using Models.Books;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAuthorService
    {
        Task<int> Create(string firstName, string lastName);

        Task<AuthorDetailsServiceModel> Details(int id);

        Task<IEnumerable<BookWithCategoriesServiceModel>> BooksByAuthor(int authorId);

        Task<bool> Exists(int id);
    }
}
