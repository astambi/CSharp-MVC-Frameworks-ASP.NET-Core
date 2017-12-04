namespace BookShop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using BookShop.Models;
    using Common.Extensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Books;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BookService : IBookService
    {
        private const int BooksCount = 10;

        private readonly BookShopDbContext db;

        public BookService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BookListingServiceModel>> All(string searchTerm)
            => await this.db
            .Books
            .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()))
            .OrderBy(b => b.Title)
            .Take(BooksCount)
            .ProjectTo<BookListingServiceModel>()
            .ToListAsync();

        public async Task<int> Create(
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime? releaseDate,
            int authorId,
            string categories)
        {
            // Create book
            var book = new Book
            {
                AuthorId = authorId,
                Title = title,
                Description = description,
                Price = price,
                Copies = copies,
                Edition = edition,
                ReleaseDate = releaseDate,
                AgeRestriction = ageRestriction
            };

            // Add Categories
            if (!string.IsNullOrWhiteSpace(categories))
            {
                // Get categories
                var categoryNames = categories
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToHashSet();

                var existingCategories = await this.db
                    .Categories
                    .Where(c => categoryNames
                                .Select(cn => cn.ToLower())
                                .Contains(c.Name.ToLower()))
                    .ToListAsync();

                var allCategories = new List<Category>(existingCategories);

                // Create new Categories
                foreach (var categoryName in categoryNames)
                {
                    if (!existingCategories.Any(c => c.Name.ToLower() == categoryName.ToLower()))
                    {
                        var category = new Category { Name = categoryName };
                        this.db.Categories.Add(category);
                        allCategories.Add(category);
                    }
                }

                await this.db.SaveChangesAsync();

                // Add Categories to Book
                foreach (var category in allCategories)
                {
                    book.Categories.Add(new CategoryBook { CategoryId = category.Id });
                }
            }

            await this.db.AddAsync(book);
            await this.db.SaveChangesAsync();

            return book.Id;
        }

        public async Task Delete(int id)
        {
            var book = await this.db.Books.FindAsync(id);
            if (book == null)
            {
                return;
            }

            this.db.Remove(book);
            await this.db.SaveChangesAsync();
        }

        public async Task<BookDetailsServiceModel> Details(int id)
            => await this.db
            .Books
            .Where(b => b.Id == id)
            .ProjectTo<BookDetailsServiceModel>()
            .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await this.db.Books.AnyAsync(b => b.Id == id);

        public async Task Update(
            int id,
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime? releaseDate,
            int authorId)
        {
            var book = await this.db.Books.FindAsync(id);
            if (book == null)
            {
                return;
            }

            if (book.AuthorId != authorId ||
                book.Title != title ||
                book.Description != description ||
                book.Price != price ||
                book.Copies != copies ||
                book.Edition != edition ||
                book.ReleaseDate != releaseDate ||
                book.AgeRestriction != ageRestriction)
            {
                book.AuthorId = authorId;
                book.Title = title;
                book.Description = description;
                book.Price = price;
                book.Copies = copies;
                book.Edition = edition;
                book.ReleaseDate = releaseDate;
                book.AgeRestriction = ageRestriction;

                await this.db.SaveChangesAsync();
            }            
        }
    }
}
