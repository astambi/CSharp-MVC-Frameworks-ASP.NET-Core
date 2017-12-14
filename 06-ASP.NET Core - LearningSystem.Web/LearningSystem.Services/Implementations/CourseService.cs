namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> AllActiveAsync()
            => await this.db
            .Courses
            .Where(c => c.StartDate >= DateTime.UtcNow.Date)
            .OrderByDescending(c => c.StartDate)
            .ThenBy(c => c.Name)
            .ProjectTo<CourseListingServiceModel>()
            .ToListAsync();

        public async Task<IEnumerable<CourseListingServiceModel>> FindAsync(string searchText)
            => await this.db
            .Courses
            .OrderByDescending(c => c.Id)
            .Where(c => c.Name.ToLower().Contains((searchText ?? string.Empty).ToLower()))
            .ProjectTo<CourseListingServiceModel>()
            .ToListAsync();

        public async Task<TModel> ByIdAsync<TModel>(int id) where TModel : class
            => await this.db
            .Courses
            .Where(c => c.Id == id)
            .ProjectTo<TModel>()
            .FirstOrDefaultAsync();

        public async Task<bool> IsUserSignedInCourseAsync(int courseId, string userId)
            => await this.db
            .Courses
            .AnyAsync(c => c.Id == courseId &&
                           c.Students.Any(s => s.StudentId == userId));

        public async Task<bool> SignUpStudentAsync(int courseId, string studentId)
        {
            var courseInfo = await GetCourseInfo(courseId, studentId);

            if (courseInfo == null ||
                courseInfo.StartDate < DateTime.UtcNow.Date ||
                courseInfo.IsStudentEnrolledInCourse)
            {
                return false;
            }

            var studentInCourse = new StudentCourse
            {
                CourseId = courseId,
                StudentId = studentId
            };

            await this.db.AddAsync(studentInCourse);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SignOutStudentAsync(int courseId, string studentId)
        {
            var courseInfo = await GetCourseInfo(courseId, studentId);

            if (courseInfo == null ||
                courseInfo.StartDate < DateTime.UtcNow.Date ||
                !courseInfo.IsStudentEnrolledInCourse)
            {
                return false;
            }

            // NB! Table not in the the DbSet! => with SelectMany
            //var studentCourse = await this.db.Courses
            //    .Where(c => c.Id == courseId)
            //    .SelectMany(c => c.Students)
            //    .Where(sc => sc.StudentId == studentId)
            //    .FirstOrDefaultAsync();
            //this.db.Remove(studentCourse);

            // NB! Table not in the DbSet! => use primary key values, observe keys order!
            var studentInCourse = await this.db.FindAsync<StudentCourse>(studentId, courseId);
            this.db.Remove(studentInCourse);

            await this.db.SaveChangesAsync();

            return true;
        }

        private async Task<CourseInfoServiceModel> GetCourseInfo(int courseId, string studentId)
            => await this.db
            .Courses
            .Where(c => c.Id == courseId)
            .Select(c => new CourseInfoServiceModel
            {
                StartDate = c.StartDate,
                IsStudentEnrolledInCourse = c.Students.Any(s => s.StudentId == studentId)
            })
            .FirstOrDefaultAsync();

        public async Task<bool> SaveExamSubmission(int courseId, string studentId, byte[] examSubmission)
        {
            var course = await this.db.Courses.FindAsync(courseId);
            if (course == null)
            {
                return false;
            }

            var student = await this.db.Users.FindAsync(studentId);
            if (student == null)
            {
                return false;
            }

            var studentInCourse = await this.db
                .FindAsync<StudentCourse>(studentId, courseId);
            if (studentInCourse == null)
            {
                return false; 
            }

            studentInCourse.ExamSubmission = examSubmission;
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
