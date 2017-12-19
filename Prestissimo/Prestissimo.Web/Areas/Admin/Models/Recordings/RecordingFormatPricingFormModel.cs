namespace Prestissimo.Web.Areas.Admin.Models.Recordings
{
    using Data;
    using System.ComponentModel.DataAnnotations;

    public class RecordingFormatPricingFormModel
    {
        public int Id { get; set; }

        public int FormatId { get; set; }

        [Range(DataConstants.RecordingFormatMinQuantity, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(DataConstants.RecordingFormatMinPrice, double.MaxValue)]
        public decimal Price { get; set; }

        // In %
        [Range(DataConstants.RecordingFormatMinDiscount, DataConstants.RecordingFormatMaxDiscount)]
        public double Discount { get; set; }
    }
}
