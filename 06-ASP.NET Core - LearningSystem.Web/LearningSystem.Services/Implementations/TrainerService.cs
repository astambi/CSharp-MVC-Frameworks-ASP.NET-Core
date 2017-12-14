namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TrainerService : ITrainerService
    {
        private readonly LearningSystemDbContext db;

        public TrainerService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddGradeAsync(int courseId, string studentId, Grade grade)
        {
            var studentInCourse = await this.db
                .FindAsync<StudentCourse>(studentId, courseId); // NB keys order!

            if (studentInCourse == null)
            {
                return false;
            }

            studentInCourse.Grade = grade;
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> CoursesAsync(string trainerId)
            => await this.db
            .Courses
            .Where(c => c.TrainerId == trainerId)
            .ProjectTo<CourseListingServiceModel>()
            .ToListAsync();

        public async Task<byte[]> GetExamSubmission(int courseId, string studentId)
        {
            var studentInCourse = await this.db
                .FindAsync<StudentCourse>(studentId, courseId); // NB keys order!

            return studentInCourse?.ExamSubmission;
        }

        public async Task<bool> IsTrainer(int courseId, string trainerId)
            => await this.db
            .Courses
            .AnyAsync(c => c.Id == courseId && c.TrainerId == trainerId);

        public async Task<StudentInCourseNamesServiceModel> StudentInCourseNamesAsync(int courseId, string studentId)
        {
            var studentUsername = await this.db.Users
                .Where(u => u.Id == studentId)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync();

            if (studentUsername == null)
            {
                return null;
            }

            var courseName = await this.db.Courses
                .Where(c => c.Id == courseId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();

            if (courseName == null)
            {
                return null;
            }

            return new StudentInCourseNamesServiceModel
            {
                UserName = studentUsername,
                CourseName = courseName
            };
        }

        public async Task<IEnumerable<StudentInCourseServiceModel>> StudentsInCourseAsync(int courseId)
            => await this.db
            .Courses
            .Where(c => c.Id == courseId)
            .SelectMany(c => c.Students.Select(s => s.Student))
            .ProjectTo<StudentInCourseServiceModel>(new { courseId })
            .ToListAsync();
    }
}
