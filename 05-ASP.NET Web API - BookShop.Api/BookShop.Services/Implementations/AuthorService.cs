namespace BookShop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using BookShop.Models;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Authors;
    using Models.Books;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AuthorService : IAuthorService
    {
        private readonly BookShopDbContext db;

        public AuthorService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BookWithCategoriesServiceModel>> All(int authorId)
            => await this.db
            .Books
            .Where(b => b.AuthorId == authorId)
            .ProjectTo<BookWithCategoriesServiceModel>()
            .ToListAsync();

        public async Task<int> Create(string firstName, string lastName)
        {
            var author = new Author
            {
                FirstName = firstName,
                LastName = lastName
            };

            await this.db.Authors.AddAsync(author);
            await this.db.SaveChangesAsync();

            return author.Id;
        }

        public async Task<AuthorDetailsServiceModel> Details(int id)
            => await this.db
            .Authors
            .Where(a => a.Id == id)
            .ProjectTo<AuthorDetailsServiceModel>()
            .FirstOrDefaultAsync();

        public async Task<AuthorDetailsServiceModel> DetailsWithBooks(int id)
            => await this.db
            .Authors
            .Where(a => a.Id == id)
            .ProjectTo<AuthorDetailsServiceModel>()
            .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await this.db.Authors.AnyAsync(a => a.Id == id);
    }
}
