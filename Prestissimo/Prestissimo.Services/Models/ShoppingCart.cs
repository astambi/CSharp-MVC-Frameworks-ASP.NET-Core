namespace Prestissimo.Services.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCart
    {
        private readonly IList<CartItem> items;

        public ShoppingCart()
        {
            this.items = new List<CartItem>();
        }

        public void AddToCart(int recordingId, int formatId)
        {
            var cartItem = this.items.FirstOrDefault(i => i.RecordingId == recordingId
                                                       && i.FormatId == formatId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    RecordingId = recordingId,
                    FormatId = formatId,
                    Quantity = 1
                };

                this.items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
        }

        public IEnumerable<CartItem> GetItems
            => new List<CartItem>(this.items);

        public void RemoveFromCart(int recordingId, int formatId)
        {
            var cartItem = this.items.FirstOrDefault(i => i.RecordingId == recordingId
                                                       && i.FormatId == formatId);

            if (cartItem != null)
            {
                this.items.Remove(cartItem);
            }
        }

        public void ClearCartItems() => this.items.Clear();
    }
}
