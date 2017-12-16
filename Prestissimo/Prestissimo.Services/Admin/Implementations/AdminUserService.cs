namespace Prestissimo.Services.Admin.Implementations
{
    using Admin.Models.Users;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminUserService : IAdminUserService
    {
        private readonly PrestissimoDbContext db;

        public AdminUserService(PrestissimoDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
            => await this.db
                .Users
                .OrderBy(u => u.Name)
                .ThenBy(u => u.UserName)
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
    }
}
