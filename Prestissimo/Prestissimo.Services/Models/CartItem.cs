namespace Prestissimo.Services.Models
{
    public class CartItem
    {
        public int RecordingId { get; set; }

        public int FormatId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public double Discount { get; set; }
    }
}
