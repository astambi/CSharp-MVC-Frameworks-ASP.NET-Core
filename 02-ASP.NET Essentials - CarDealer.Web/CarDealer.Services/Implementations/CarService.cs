namespace CarDealer.Services.Implementations
{
    using Data;
    using Data.Models;
    using Models.Cars;
    using Models.Parts;
    using System.Collections.Generic;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarBasicModel> AllBasic()
        {
            return this.db
                .Cars
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Select(c => new CarBasicModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model
                })
                .ToList();
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

        public void Create(string make, string model, long travelledDistance,
                           IEnumerable<int> selectedPartIds)
        {
            var existingPartIds = this.db
                .Parts
                .Where(p => selectedPartIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToList();

            var car = new Car
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance
            };

            foreach (var partId in existingPartIds)
            {
                car.Parts.Add(new PartCar { PartId = partId });
            }

            this.db.Cars.Add(car);
            this.db.SaveChanges();
        }
    }
}
