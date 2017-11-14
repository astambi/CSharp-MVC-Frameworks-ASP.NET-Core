namespace CarDealer.Services.Implementations
{
    using Data;
    using Data.Models;
    using Models;
    using Models.Cars;
    using Models.Customers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerService : ICustomerService
    {
        private const double AdditionalDiscount = 0.05;

        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerBasicModel> AllBasic()
        {
            return this.db
                .Customers
                .OrderBy(c => c.Name)
                .Select(c => new CustomerBasicModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }

        public IEnumerable<CustomerModel> AllOrdered(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                                    .OrderBy(c => c.BirthDate)
                                    .ThenBy(c => c.IsYoungDriver);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery
                                    .OrderByDescending(c => c.BirthDate)
                                    .ThenBy(c => c.IsYoungDriver);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid order direction: {order}");
            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
        }

        public void Create(string name, DateTime birthDate, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDate = birthDate,
                IsYoungDriver = isYoungDriver
            };

            this.db.Customers.Add(customer);
            this.db.SaveChanges();
        }

        public bool Exists(int id)
        {
            return this.db.Customers.Any(c => c.Id == id);
        }

        public CustomerModel GetById(int id)
        {
            return this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .FirstOrDefault();
        }

        public CustomerTotalSalesModel TotalSalesById(int id)
        {
            return this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerTotalSalesModel
                {
                    Name = c.Name,
                    CarSales = c.Sales.Select(s =>
                        new CarPriceModel
                        {
                            Price = s.Car.Parts.Sum(p => p.Part.Price),
                            Discount = s.Discount
                                     + (c.IsYoungDriver ? AdditionalDiscount : 0)
                        })
                        .ToList()
                })
                .FirstOrDefault();
        }

        public void Update(int id, string name, DateTime birthDate, bool isYoungDriver)
        {
            var customer = this.db.Customers.Where(c => c.Id == id).FirstOrDefault();
            if (customer == null)
            {
                return;
            }

            customer.Name = name;
            customer.BirthDate = birthDate;
            customer.IsYoungDriver = isYoungDriver; // TODO? YoungDriver can be edited?

            this.db.Update(customer);
            this.db.SaveChanges();
        }
    }
}
