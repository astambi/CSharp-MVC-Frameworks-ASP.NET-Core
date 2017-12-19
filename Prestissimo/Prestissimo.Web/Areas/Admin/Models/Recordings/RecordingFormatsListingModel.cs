namespace Prestissimo.Web.Areas.Admin.Models.Recordings
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models.Recordings;
    using System.Collections.Generic;

    public class RecordingFormatsListingModel
    {
        public AdminRecordingListingWithFormatsServiceModel Recording { get; set; }

        public IEnumerable<SelectListItem> Formats { get; set; } = new List<SelectListItem>();
    }
}
