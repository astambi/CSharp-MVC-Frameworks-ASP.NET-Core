namespace BookShop.Services.Models.Books
{
    using Common.Mapping;
    using Data.Models;

    public class BookListingServiceModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
