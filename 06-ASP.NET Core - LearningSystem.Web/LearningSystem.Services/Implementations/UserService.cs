namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using iTextSharp.text;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using LearningSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Services.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly LearningSystemDbContext db;
        private readonly IPdfGenerator pdfGenerator;

        public UserService(
            LearningSystemDbContext db,
            IPdfGenerator pdfGenerator)
        {
            this.db = db;
            this.pdfGenerator = pdfGenerator;
        }

        public async Task<IEnumerable<UserListingServiceModel>> FindAsync(string searchText)
            => await this.db
            .Users
            .OrderBy(u => u.UserName)
            .Where(u => u.UserName.ToLower().Contains((searchText ?? string.Empty).ToLower()))
            .ProjectTo<UserListingServiceModel>()
            .ToListAsync();

        public async Task<byte[]> GetPdfCertificate(int courseId, string studentId)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(studentId, courseId);
            if (studentInCourse == null)
            {
                return null;
            }

            var certificateData = await this.db
                .Courses
                .Where(c => c.Id == courseId)
                .Select(c => new
                {
                    CourseName = c.Name,
                    CourseStartDate = c.StartDate.ToShortDateString(),
                    CourseEndDate = c.EndDate.ToShortDateString(),
                    StudentName = c.Students
                        .Where(s => s.StudentId == studentId)
                        .Select(s => s.Student.Name)
                        .FirstOrDefault(),
                    StudentGrade = c.Students
                        .Where(s => s.StudentId == studentId)
                        .Select(s => s.Grade)
                        .FirstOrDefault(),
                    TrainerName = c.Trainer.Name,
                    IssueDate = DateTime.UtcNow.ToShortDateString()
                })
                .FirstOrDefaultAsync();

            var pdfCertificate = this.pdfGenerator
                .GeneratePdfFromHtml(string.Format(
                    ServiceConstants.PdfCertificateLayout,
                    certificateData.CourseName,
                    certificateData.CourseStartDate,
                    certificateData.CourseEndDate,
                    certificateData.StudentName,
                    certificateData.StudentGrade,
                    certificateData.TrainerName,
                    certificateData.IssueDate));

            return pdfCertificate;
        }

        public async Task<UserProfileServiceModel> ProfileAsync(string id)
            => await this.db
            .Users
            .Where(u => u.Id == id)
            .ProjectTo<UserProfileServiceModel>(new { studentId = id })
            .FirstOrDefaultAsync();
    }
}
