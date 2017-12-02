namespace BookShop.Api.Models.Books
{
    public class BookWithCategoriesRequestModel : BookRequestModel
    {
        public string Categories { get; set; }
    }
}
