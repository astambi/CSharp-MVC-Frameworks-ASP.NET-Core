namespace Prestissimo.Web.Models.ShoppingCart
{
    using System.Collections.Generic;

    public class OrderViewModel
    {
        public IEnumerable<CartItemViewModel> Items { get; set; }

        public decimal OrderTotal { get; set; }
    }
}
