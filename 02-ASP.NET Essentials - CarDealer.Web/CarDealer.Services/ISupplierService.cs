namespace CarDealer.Services
{
    using Models;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierModel> AllByType(bool isImporter);
    }
}
