namespace Prestissimo.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecordingFormat
    {
        public int RecordingId { get; set; }

        public Recording Recording { get; set; }

        public int FormatId { get; set; }

        public Format Format { get; set; }

        [Range(DataConstants.RecordingFormatMinQuantity, DataConstants.RecordingFormatMaxQuantity)]
        public int Quantity { get; set; }

        [Range(DataConstants.RecordingFormatMinPrice, DataConstants.RecordingFormatMaxPrice)]
        public decimal Price { get; set; }

        // In %
        [Range(DataConstants.RecordingFormatMinDiscount, DataConstants.RecordingFormatMaxDiscount)]
        public double Discount { get; set; }
    }
}
