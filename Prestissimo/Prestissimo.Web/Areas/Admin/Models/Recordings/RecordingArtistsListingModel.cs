namespace Prestissimo.Web.Areas.Admin.Models.Recordings
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models.Recordings;
    using System.Collections.Generic;

    public class RecordingArtistsListingModel
    {
        public AdminRecordingListingWithArtistsServiceModel Recording { get; set; }

        public IEnumerable<SelectListItem> Artists { get; set; } = new List<SelectListItem>();
    }
}
