namespace Prestissimo.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecordingFormat
    {
        public int RecordingId { get; set; }

        public Recording Recording { get; set; }

        public int FormatId { get; set; }

        public Format Format { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        // In %
        [Range(0, 100)]
        public double Discount { get; set; }
    }
}
