namespace LearningSystem.Services.Blog
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogService
    {
        Task<IEnumerable<ArticleListingServiceModel>> AllAsync(int page = 1);

        Task<ArticleDetailsServiceModel> ByIdAsync(int id);

        Task CreateAsync(string title, string content, string authorId);

        Task<int> TotalAsync();
    }
}
