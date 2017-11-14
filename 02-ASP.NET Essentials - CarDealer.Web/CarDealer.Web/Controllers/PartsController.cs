namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Parts;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PartsController : Controller
    {
        private const int PageSize = 25;

        private readonly IPartService partService;
        private readonly ISupplierService supplierService;

        public PartsController(
            IPartService partService,
            ISupplierService supplierService)
        {
            this.partService = partService;
            this.supplierService = supplierService;
        }

        public IActionResult All(int page = 1)
        {
            var model = new PartPageListingModel
            {
                Parts = this.partService.All(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.partService.Total() / (decimal)PageSize)
            };

            return this.View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            var model = new PartFormModel
            {
                SuppliersDropdown = this.GetSuppliersDropDown()
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(PartFormModel model)
        {
            if (!this.supplierService.Exists(model.SupplierId))
            {
                this.ModelState.AddModelError(nameof(PartFormModel.SupplierId), "Invalid Supplier");
            }

            if (!this.ModelState.IsValid)
            {
                model.SuppliersDropdown = this.GetSuppliersDropDown();
                return this.View(model);
            }

            this.partService.Create(
                model.Name,
                model.Price,
                model.Quantity,
                model.SupplierId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        [Authorize]
        public IActionResult ConfirmDelete(int id)
        {
            this.partService.Delete(id);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var part = this.partService.GetById(id);
            if (part == null)
            {
                return RedirectToAction(nameof(All));
            }

            var model = new PartFormModel
            {
                Name = part.Name,
                Price = part.Price,
                Quantity = part.Quantity,
                IsEdit = true
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, PartFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.IsEdit = true;
                return this.View(model);
            }

            this.partService.Update(
                id,
                model.Price,
                model.Quantity);

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<SelectListItem> GetSuppliersDropDown()
        {
            return this.supplierService
                .AllDropDown()
                .Select(s => new SelectListItem // NB!
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                });
        }
    }
}
