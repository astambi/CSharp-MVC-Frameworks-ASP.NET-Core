namespace Prestissimo.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int RecordingId { get; set; }

        public int FormatId { get; set; }

        public string RecordingTitle { get; set; }

        public string FormatName { get; set; }

        public string Label { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public double Discount { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
