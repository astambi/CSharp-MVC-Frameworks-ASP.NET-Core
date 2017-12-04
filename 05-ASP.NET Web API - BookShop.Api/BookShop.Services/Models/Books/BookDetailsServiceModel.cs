namespace BookShop.Services.Models.Books
{
    using AutoMapper;
    using BookShop.Models;
    using Common.Mapping;
    using System.Linq;

    public class BookDetailsServiceModel : BookWithCategoriesServiceModel, IMapFrom<Book>, IHaveCustomMapping
    {
        public string Author { get; set; }

        public override void ConfigureMapping(Profile mapper)
        {
            // NB! map everything to child class
            mapper
                .CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(b => b.Categories, cfg => cfg
                    .MapFrom(b => b.Categories.Select(c => c.Category.Name)))
                .ForMember(b => b.Author, cfg => cfg
                    .MapFrom(b => $"{b.Author.FirstName} {b.Author.LastName}"));
        }
    }
}
