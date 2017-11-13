namespace CarDealer.Web.Models.Suppliers
{
    using Services.Models.Suppliers;
    using System.Collections.Generic;

    public class SuppliersByType
    {
        public IEnumerable<SupplierListingModel> Suppliers { get; set; }

        public string Type { get; set; }
    }
}
