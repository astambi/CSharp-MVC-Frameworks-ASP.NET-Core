namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> AllActive()
            => await this.db
            .Courses
            .Where(c => c.StartDate >= DateTime.UtcNow)
            .OrderByDescending(c => c.StartDate)
            .ThenBy(c => c.Name)
            .ProjectTo<CourseListingServiceModel>()
            .ToListAsync();

        public async Task<CourseDetailsServiceModel> ByIdAsync(int id)
            => await this.db
            .Courses
            .Where(c => c.Id == id)
            .ProjectTo<CourseDetailsServiceModel>()
            .FirstOrDefaultAsync();
    }
}
