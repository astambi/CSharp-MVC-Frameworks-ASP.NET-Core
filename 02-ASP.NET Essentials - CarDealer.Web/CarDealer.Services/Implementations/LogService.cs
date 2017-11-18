namespace CarDealer.Services.Implementations
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Logs;
    using System.Collections.Generic;
    using System.Linq;

    public class LogService : ILogService
    {
        private readonly CarDealerDbContext db;

        public LogService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<LogListingModel> All(string search, int page, int pageSize)
            => this.GetLogsAsQuerable(search)
                .OrderBy(l => l.Time)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(l => new LogListingModel
                {
                    User = l.User,
                    Operation = l.Operation,
                    ModifiedTable = l.ModifiedTable,
                    Time = l.Time
                })
                .ToList();

        public void Clear()
        {
            var sqlQuery = $"DELETE FROM {nameof(Log)}s";
            this.db.Database.ExecuteSqlCommand(sqlQuery);
            this.db.SaveChanges();
        }

        public int Total(string search) 
            => this.GetLogsAsQuerable(search).Count();

        private IQueryable<Log> GetLogsAsQuerable(string search)
        {
            var logsAsQuerable = this.db.Logs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                logsAsQuerable = logsAsQuerable
                                .Where(l => l.User.ToLower().Contains(search.ToLower()));
            }

            return logsAsQuerable;
        }
    }
}
