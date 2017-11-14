namespace CarDealer.Services.Models.Sales
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SaleReviewModel
    {
        private const int YoungCustomerDiscount = 5;

        public int CustomerId { get; set; }

        public string Customer { get; set; }

        public bool IsYoungCustomer { get; set; }

        public int CarId { get; set; }

        public string Car { get; set; }

        public double Discount { get; set; }

        [Display(Name = "Car Price")]
        public decimal Price { get; set; }

        // As int !
        public double TotalDiscount
            => this.IsYoungCustomer
            ? Math.Min(100, this.Discount + YoungCustomerDiscount)
            : this.Discount;

        [Display(Name = "Final Car Price")]
        public decimal FinalPrice
            => this.Price * (1 - (decimal)this.TotalDiscount / 100);
    }
}
