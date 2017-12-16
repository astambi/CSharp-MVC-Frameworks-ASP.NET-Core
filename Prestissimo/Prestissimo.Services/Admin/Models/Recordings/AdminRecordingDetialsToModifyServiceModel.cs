namespace Prestissimo.Services.Admin.Models.Recordings
{
    using Common.Mapping;
    using Data.Models;
    using System;

    public class AdminRecordingDetailsToModifyServiceModel : IMapFrom<Recording>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Length { get; set; }

        public string ImageUrl { get; set; }

        public int LabelId { get; set; }
    }
}
