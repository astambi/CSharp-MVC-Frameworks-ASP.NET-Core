namespace CarDealer.Services.Models.Sales
{
    using Cars;

    public class SaleModel : CarModel
    {
        public int Id { get; set; }

        public string Customer { get; set; }

        public decimal Price { get; set; }

        public double Discount { get; set; }

        public decimal NetPrice
            => this.Price * (decimal)(1 - this.Discount);
    }
}
