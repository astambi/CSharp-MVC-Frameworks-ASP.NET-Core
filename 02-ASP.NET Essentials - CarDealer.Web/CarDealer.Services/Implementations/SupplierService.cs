namespace CarDealer.Services.Implementations
{
    using CarDealer.Data.Models;
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

        public IEnumerable<SupplierWithTypeModel> All()
        {
            return this.db
                .Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new SupplierWithTypeModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsImporter = s.IsImporter
                })
                .ToList();
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

        public void Create(string name, bool isImporter)
        {
            var supplier = new Supplier
            {
                Name = name,
                IsImporter = isImporter
            };

            this.db.Suppliers.Add(supplier);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var supplier = this.db
                .Suppliers
                .Find(id);

            if (supplier == null)
            {
                return;
            }

            var parts = supplier.Parts;
            this.db.Parts.RemoveRange(parts);

            this.db.Suppliers.Remove(supplier);
            this.db.SaveChanges();
        }

        public bool Exists(int id)
        {
            return this.db.Suppliers.Any(s => s.Id == id);
        }

        public SupplierWithTypeModel GetById(int id)
        {
            return this.db
               .Suppliers
               .Where(s => s.Id == id)
               .Select(s => new SupplierWithTypeModel
               {
                   Name = s.Name,
                   IsImporter = s.IsImporter
               })
               .FirstOrDefault();
        }

        public void Update(int id, string name, bool isImporter)
        {
            var supplier = this.db.Suppliers.Find(id);
            if (supplier == null)
            {
                return;
            }

            supplier.Name = name;
            supplier.IsImporter = isImporter;

            //this.db.Parts.Update(part);
            this.db.SaveChanges();
        }
    }
}
