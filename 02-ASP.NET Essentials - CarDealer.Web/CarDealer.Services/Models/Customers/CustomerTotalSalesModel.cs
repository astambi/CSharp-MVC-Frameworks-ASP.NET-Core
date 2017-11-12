using CarDealer.Services.Models.Cars;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Services.Models.Customers
{
    public class CustomerTotalSalesModel
    {
        public string Name { get; set; }

        public bool IsYoungDriver { get; set; }

        public List<CarPriceModel> CarSales { get; set; }

        public decimal TotalMoneySpent 
            => this.CarSales.Sum(s => s.Price * (decimal)(1 - s.Discount));

        public int CarsBought => this.CarSales.Count;
    }
}
