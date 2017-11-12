namespace CarDealer.Services.Implementations
{
    using Data;
    using Models;
    using Models.Cars;
    using System.Collections.Generic;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarModel> AllByMake(string make)
        {
            var cars = this.db.Cars.AsQueryable();

            if (make != null)
            {
                cars = cars.Where(c => c.Make.ToLower() == make.ToLower());
            }

            return cars
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new CarModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();
        }

        public IEnumerable<CarWithPartsModel> AllWithParts()
        {
            return this.db
                .Cars
                .Select(c => new CarWithPartsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.Parts
                            .Select(p => new PartModel
                            {
                                Name = p.Part.Name,
                                Price = p.Part.Price
                            })
                })
                .ToList();
        }
    }
}
