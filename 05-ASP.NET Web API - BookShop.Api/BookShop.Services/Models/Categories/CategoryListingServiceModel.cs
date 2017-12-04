namespace BookShop.Services.Models.Categories
{
    using BookShop.Models;
    using Common.Mapping;

    public class CategoryServiceModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
