namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Suppliers;
    using Services;

    public class SuppliersController : Controller
    {
        private const string SuppliersView = "Suppliers";

        private readonly ISupplierService supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }

        public IActionResult Importers()
        {
            var model = this.GetSuppliersByTypeModel(nameof(Importers));

            return this.View(SuppliersView, model);
        }

        public IActionResult Local()
        {
            var model = this.GetSuppliersByTypeModel(nameof(Local));

            return this.View(SuppliersView, model);
        }

        private SuppliersByType GetSuppliersByTypeModel(string type)
        {
            var isImporter = type == nameof(Importers);
            var suppliers = this.supplierService.AllByType(isImporter);

            return new SuppliersByType
            {
                Suppliers = suppliers,
                Type = type
            };
        }
    }
}
