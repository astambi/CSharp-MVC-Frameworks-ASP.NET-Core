namespace CarDealer.Services.Implementations
{
    using CarDealer.Data.Models;
    using Data;
    using Models.Sales;
    using System;
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
                    Id = s.Id,
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TravelledDistance = s.Car.TravelledDistance,
                    Customer = s.Customer.Name,
                    Discount = Math.Min(1,
                            s.Discount + (s.Customer.IsYoungDriver ? AdditionalDiscount : 0)),
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
                            Math.Min(1,
                            s.Discount + (s.Customer.IsYoungDriver ? AdditionalDiscount : 0)));
            }

            return salesQuery
                .Select(s => new SaleModel
                {
                    Id = s.Id,
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TravelledDistance = s.Car.TravelledDistance,
                    Customer = s.Customer.Name,
                    Discount = Math.Min(1,
                            s.Discount + (s.Customer.IsYoungDriver ? AdditionalDiscount : 0)),
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
                     Discount = Math.Min(1,
                               s.Discount + (s.Customer.IsYoungDriver ? AdditionalDiscount : 0)),
                     Price = s.Car.Parts.Sum(p => p.Part.Price)
                 })
                 .FirstOrDefault();
        }

        public SaleReviewModel SaleReview(int carId, int customerId, double discount)
        {
            var customer = this.db
                .Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    c.Name,
                    c.IsYoungDriver
                })
                .FirstOrDefault();

            var car = this.db
                .Cars
                .Where(c => c.Id == carId)
                .Select(c => new
                {
                    Name = $"{c.Make} {c.Model}",
                    Price = c.Parts.Sum(cp => cp.Part.Price)
                })
                .FirstOrDefault();

            return new SaleReviewModel
            {
                CarId = carId,
                Car = car.Name,
                CustomerId = customerId,
                Customer = customer.Name,
                IsYoungCustomer = customer.IsYoungDriver,
                Discount = discount, // as int
                Price = car.Price
            };
        }

        public void Create(int customerId, int carId, double discount)
        {
            var sale = new Sale
            {
                CustomerId = customerId,
                CarId = carId,
                Discount = discount / 100 // from int
            };

            this.db.Sales.Add(sale);
            this.db.SaveChanges();
        }
    }
}
