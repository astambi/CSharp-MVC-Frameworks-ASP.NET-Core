namespace CarDealer.Services
{
    using Models.Suppliers;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierWithTypeModel> All();

        IEnumerable<SupplierListingModel> AllByType(bool isImporter);

        IEnumerable<SupplierModel> AllDropDown();

        bool Exists(int id);

        void Create(string name, bool isImporter);

        void Delete(int id);

        SupplierWithTypeModel GetById(int id);

        void Update(int id, string name, bool isImporter);
    }
}
