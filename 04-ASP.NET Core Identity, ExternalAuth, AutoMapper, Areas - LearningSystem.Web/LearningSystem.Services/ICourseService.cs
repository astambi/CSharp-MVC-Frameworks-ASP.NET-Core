namespace LearningSystem.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingServiceModel>> AllActiveAsync();

        Task<TModel> ByIdAsync<TModel>(int id) where TModel : class;

        Task<bool> IsUserSignedInCourseAsync(int courseId, string userId);

        Task<bool> SignUpStudentAsync(int courseId, string userId);

        Task<bool> SignOutStudentAsync(int courseId, string userId);
    }
}
