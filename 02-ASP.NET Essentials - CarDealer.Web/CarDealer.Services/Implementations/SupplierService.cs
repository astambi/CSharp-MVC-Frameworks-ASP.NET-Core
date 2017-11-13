namespace CarDealer.Services.Implementations
{
    using Data;
    using Models.Suppliers;
    using System.Collections.Generic;
    using System.Linq;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierListingModel> AllByType(bool isImporter)
        {
            return this.db
                .Suppliers
                .Where(s => s.IsImporter == isImporter)
                .Select(s => new SupplierListingModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Parts = s.Parts.Count()
                })
                .ToList();
        }

        public IEnumerable<SupplierModel> AllDropDown()
        {
            return this.db
                .Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToList();
        }

        public bool Exists(int id)
        {
            return this.db.Suppliers.Any(s => s.Id == id);
        }
    }
}
