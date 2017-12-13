namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly LearningSystemDbContext db;

        public UserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<UserListingServiceModel>> FindAsync(string searchText)
            => await this.db
            .Users
            .OrderBy(u => u.UserName)
            .Where(u => u.UserName.ToLower().Contains((searchText ?? string.Empty).ToLower()))
            .ProjectTo<UserListingServiceModel>()
            .ToListAsync();

        public async Task<UserProfileServiceModel> ProfileAsync(string id)
            => await this.db
            .Users
            .Where(u => u.Id == id)
            .ProjectTo<UserProfileServiceModel>(new { studentId = id })
            .FirstOrDefaultAsync();
    }
}
