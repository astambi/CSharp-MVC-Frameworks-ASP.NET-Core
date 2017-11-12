namespace CarDealer.Services.Implementations
{
    using Data;
    using Models.Sales;
    using System.Collections.Generic;
    using System.Linq;

    public class SaleService : ISaleService
    {
        private const double AdditionalDiscount = 0.05;

        private readonly CarDealerDbContext db;

        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleModel> All()
        {
            return this.db
                .Sales
                .Select(s => new SaleModel
                {
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TravelledDistance = s.Car.TravelledDistance,
                    Customer = s.Customer.Name,
                    Discount = s.Discount
                             + (s.Customer.IsYoungDriver ? AdditionalDiscount : 0),
                    Price = s.Car.Parts.Sum(p => p.Part.Price)
                })
                .ToList();
        }

        public IEnumerable<SaleModel> Discounted(int? discountPercentage)
        {
            var salesQuery = this.db
                .Sales
                .Where(s => s.Discount > 0 || s.Customer.IsYoungDriver)
                .AsQueryable();

            if (discountPercentage != null)
            {
                salesQuery = salesQuery
                    .Where(s => discountPercentage / 100.0 == 
                            s.Discount
                            + (s.Customer.IsYoungDriver ? AdditionalDiscount : 0));
            }

            return salesQuery
                .Select(s => new SaleModel
                {
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TravelledDistance = s.Car.TravelledDistance,
                    Customer = s.Customer.Name,
                    Discount = s.Discount
                             + (s.Customer.IsYoungDriver ? AdditionalDiscount : 0),
                    Price = s.Car.Parts.Sum(p => p.Part.Price)
                })
                .ToList();
        }

        public SaleModel ById(int id)
        {
            return this.db
                 .Sales
                 .Where(s => s.Id == id)
                 .Select(s => new SaleModel
                 {
                     Make = s.Car.Make,
                     Model = s.Car.Model,
                     TravelledDistance = s.Car.TravelledDistance,
                     Customer = s.Customer.Name,
                     Discount = s.Discount
                              + (s.Customer.IsYoungDriver ? AdditionalDiscount : 0),
                     Price = s.Car.Parts.Sum(p => p.Part.Price)
                 })
                 .FirstOrDefault();
        }
    }
}
