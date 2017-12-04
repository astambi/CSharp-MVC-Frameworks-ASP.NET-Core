namespace BookShop.Services.Models.Books
{
    using BookShop.Models;
    using Common.Mapping;

    public class BookListingServiceModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
