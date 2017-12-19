namespace Prestissimo.Web.Areas.Admin.Models.Recordings
{
    using Services.Admin.Models.Formats;
    using Services.Admin.Models.Recordings;

    public class RecordingPricingModel
    {
        public AdminRecordingListingServiceModel Recording { get; set; }

        public RecordingFormatPricingFormModel FormatPricing { get; set; }
    }
}
