namespace CarDealer.Web.Models.Logs
{
    using Services.Models.Logs;
    using System.Collections.Generic;

    public class LogPageListingModel
    {
        public IEnumerable<LogListingModel> Logs { get; set; }

        public string Search { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage
            => this.CurrentPage == 1
            ? 1
            : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
            ? this.TotalPages
            : this.CurrentPage + 1;
    }
}
