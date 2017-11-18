namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Logs;
    using Services;
    using System;

    public class LogsController : Controller
    {
        private const int PageSize = 20;

        private readonly ILogService logService;

        public LogsController(ILogService logService)
        {
            this.logService = logService;
        }

        [Authorize]
        public IActionResult All(string search, int page = 1)
        {
            var totalPages = (int)Math.Ceiling(this.logService.Total(search) / (decimal)PageSize);
            totalPages = Math.Max(totalPages, 1);

            page = Math.Max(page, 1);
            page = Math.Min(page, totalPages);

            var model = new LogPageListingModel
            {
                Logs = this.logService.All(search, page, PageSize),
                Search = search,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return this.View(model);
        }

        [Authorize]
        public IActionResult Clear()
        {
            this.logService.Clear();

            return this.RedirectToAction(
                nameof(All),
                new { search = string.Empty, page = 1 });
        }
    }
}
