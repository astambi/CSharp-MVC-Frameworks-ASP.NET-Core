namespace CarDealer.Services
{
    using Models.Suppliers;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierListingModel> AllByType(bool isImporter);

        IEnumerable<SupplierModel> AllDropDown();

        bool Exists(int id);
    }
}
