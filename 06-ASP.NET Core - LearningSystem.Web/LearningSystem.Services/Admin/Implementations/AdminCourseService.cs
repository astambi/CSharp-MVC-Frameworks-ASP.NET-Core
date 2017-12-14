namespace LearningSystem.Services.Admin.Implementations
{
    using Data;
    using Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminCourseService : IAdminCourseService
    {
        private readonly LearningSystemDbContext db;

        public AdminCourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task Create(
            string name,
            string description,
            DateTime startDate,
            DateTime endDate,
            string trainerId)
        {
            var trainerExists = this.db.Users.Any(u => u.Id == trainerId);
            if (!trainerExists)
            {
                return;
            }

            var course = new Course
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                TrainerId = trainerId
            };

            await this.db.Courses.AddAsync(course);
            await this.db.SaveChangesAsync();
        }
    }
}
