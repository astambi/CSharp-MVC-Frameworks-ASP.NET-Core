namespace BookShop.Services
{
    using Models.Books;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookService
    {
        Task<IEnumerable<BookListingServiceModel>> All(string searchTerm);

        Task<BookDetailsServiceModel> Details(int id);

        Task<int> Create(
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories);

        Task<bool> Exists(int id);

        Task Delete(int id);

        Task Update(
            int id,
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId);
    }
}
