namespace Prestissimo.Services.Models
{
    using Common.Mapping;
    using Data.Models;

    public class CartItem : IMapFrom<RecordingFormat>
    {
        public int RecordingId { get; set; }

        public int FormatId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public double Discount { get; set; }
    }
}
