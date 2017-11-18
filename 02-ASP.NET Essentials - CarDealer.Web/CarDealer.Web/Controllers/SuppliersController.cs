namespace CarDealer.Web.Controllers
{
    using CarDealer.Web.Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
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

        public IActionResult All()
        {
            var suppliers = this.supplierService.All();

            return this.View(suppliers);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [Log(actionName: "Add")]
        public IActionResult Create(SupplierFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.supplierService.Create(
                model.Name,
                model.IsImporter);

            // todo

            return this.RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        [Authorize]
        [Log(actionName: "Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            this.supplierService.Delete(id);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var supplier = this.supplierService.GetById(id);
            if (supplier == null)
            {
                return RedirectToAction(nameof(All));
            }

            var model = new SupplierFormModel
            {
                Name = supplier.Name,
                IsImporter = supplier.IsImporter
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [Log]
        public IActionResult Edit(int id, SupplierFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.supplierService.Update(
                id,
                model.Name,
                model.IsImporter);

            return RedirectToAction(nameof(All));
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
