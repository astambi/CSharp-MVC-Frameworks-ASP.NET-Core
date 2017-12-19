namespace Prestissimo.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using Prestissimo.Data;
    using Prestissimo.Services.Models;
    using Services;
    using System.Linq;
    using Web.Infrastructure.Extensions;

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
                .Select(i => new
                {
                    i.RecordingId,
                    i.FormatId
                })
                .ToList();

            var itemsWithDetails = this.db
                .RecordingFormats
                .Where(rf => itemIds.Contains(new { rf.RecordingId, rf.FormatId }))
                .Select(i => new CartItemWithDetailsServiceModel
                {
                    RecordingId = i.RecordingId,
                    FormatId = i.FormatId,
                    RecordingTitle = i.Recording.Title,
                    FormatName = i.Format.Name,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    Discount = i.Discount
                })
                .ToList();

            // todo 1.01.49 15 /12 / 2017

            return View(itemsWithDetails);
        }
    }
}
