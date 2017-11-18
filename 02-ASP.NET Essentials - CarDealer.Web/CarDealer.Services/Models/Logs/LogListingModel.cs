namespace CarDealer.Services.Models.Logs
{
    using System;

    public class LogListingModel
    {
        public string User { get; set; }

        public string Operation { get; set; }

        public string ModifiedTable { get; set; }

        public DateTime Time { get; set; }
    }
}
