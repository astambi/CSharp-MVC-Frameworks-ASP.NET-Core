namespace Prestissimo.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Linq;
    using Web.Infrastructure.Extensions;
    using Web.Models.ShoppingCart;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly PrestissimoDbContext db;

        public ShoppingCartController(
            IShoppingCartManager shoppingCartManager,
            PrestissimoDbContext db)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.db = db;
        }

        public IActionResult AddToCart(int recordingId, int formatId)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            this.shoppingCartManager.AddToCart(shoppingCartId, recordingId, formatId);

            return this.RedirectToAction(nameof(Items));
        }

        public IActionResult Items()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var itemIds = items
                .Select(i => $"{i.RecordingId}:{i.FormatId}")
                .ToList();

            var itemQuantities = items.ToDictionary(i => $"{i.RecordingId}:{i.FormatId}", i => i.Quantity);

            var itemsWithDetails = this.db
                .RecordingFormats
                .Where(rf => itemIds.Contains($"{rf.RecordingId}:{rf.FormatId}")) // todo ?
                .ProjectTo<CartItemViewModel>()
                .ToList();

            itemsWithDetails
                .ForEach(i => i.Quantity = itemQuantities[$"{i.RecordingId}:{i.FormatId}"]);

            return View(itemsWithDetails);
        }

        public IActionResult Remove(int recordingId, int formatId)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            this.shoppingCartManager.RemoveFromCart(shoppingCartId, recordingId, formatId);

            return this.RedirectToAction(nameof(Items));
        }

            // todo 1.13.49 15 /12 / 2017

        // TODO
        public IActionResult Up(int recordingId, int formatId)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            //this.shoppingCartManager.RemoveFromCart(shoppingCartId, recordingId, formatId);

            return this.RedirectToAction(nameof(Items));
        }

        public IActionResult Down(int recordingId, int formatId)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            //this.shoppingCartManager.RemoveFromCart(shoppingCartId, recordingId, formatId);

            return this.RedirectToAction(nameof(Items));
        }

        [Authorize]
        [HttpPost]
        public IActionResult FinishOrder() // todo model ???
        {
            // todo save order

            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            this.shoppingCartManager.Clear(shoppingCartId);

            this.TempData.AddSuccessMessage(WebConstants.OrderFinishSuccess);

            return this.RedirectToAction(nameof(Items));
        }
    }
}
