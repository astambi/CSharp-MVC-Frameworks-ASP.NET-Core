namespace Prestissimo.Services.Implementations
{
    using Models;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> shoppingCarts; // NB! 

        public ShoppingCartManager()
        {
            this.shoppingCarts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string cartId, int recordingId, int formatId)
            => this.GetShoppingCart(cartId)
                   .AddToCart(recordingId, formatId);

        public IEnumerable<CartItem> GetItems(string cartId)
            => new List<CartItem>(this.GetShoppingCart(cartId).GetItems);

        public void RemoveFromCart(string cartId, int recordingId, int formatId)
            => this.GetShoppingCart(cartId)
                   .RemoveFromCart(recordingId, formatId);

        public void Clear(string cartId)
            => this.GetShoppingCart(cartId)
                   .ClearCartItems();

        private ShoppingCart GetShoppingCart(string cartId)
            => this.shoppingCarts.GetOrAdd(cartId, new ShoppingCart());
    }
}
