namespace LearningSystem.Services
{
    using Microsoft.AspNetCore.Http;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingServiceModel>> AllActiveAsync();

        Task<IEnumerable<CourseListingServiceModel>> FindAsync(string searchText);

        Task<TModel> ByIdAsync<TModel>(int id) where TModel : class;

        Task<bool> IsUserSignedInCourseAsync(int courseId, string userId);

        Task<bool> SignUpStudentAsync(int courseId, string userId);

        Task<bool> SignOutStudentAsync(int courseId, string userId);

        Task<bool> SaveExamSubmission(int courseId, string studentId, byte[] examSubmission);
       
    }
}
