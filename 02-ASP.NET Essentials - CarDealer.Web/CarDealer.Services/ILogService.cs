namespace CarDealer.Services
{
    using Models.Logs;
    using System.Collections.Generic;

    public interface ILogService
    {
        IEnumerable<LogListingModel> All(string search, int page, int pageSize);

        void Clear();

        int Total(string search);
    }
}
