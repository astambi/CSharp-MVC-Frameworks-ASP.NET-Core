namespace CarDealer.Web.Models.Suppliers
{
    using Services.Models;
    using System.Collections.Generic;

    public class SuppliersByType
    {
        public IEnumerable<SupplierModel> Suppliers { get; set; }

        public string Type { get; set; }
    }
}
