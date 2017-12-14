namespace LearningSystem.Services
{
    using Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITrainerService
    {
        Task<IEnumerable<CourseListingServiceModel>> CoursesAsync(string trainerId);

        Task<bool> IsTrainer(int courseId, string trainerId);

        Task<IEnumerable<StudentInCourseServiceModel>> StudentsInCourseAsync(int courseId);

        Task<bool> AddGradeAsync(int courseId, string studentId, Grade grade);

        Task<byte[]> GetExamSubmission(int courseId, string studentId);

        Task<StudentInCourseNamesServiceModel> StudentInCourseNamesAsync(int courseId, string studentId);
    }
}
