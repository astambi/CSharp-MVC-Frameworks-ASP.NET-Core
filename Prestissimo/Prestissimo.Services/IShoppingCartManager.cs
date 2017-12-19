namespace Prestissimo.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IShoppingCartManager
    {
        void AddToCart(string cartId, int recordingId, int formatId);

        IEnumerable<CartItem> GetItems(string cartId);

        IEnumerable<CartItemWithDetailsServiceModel> GetItemsWithDetails(string cartId);

        void RemoveFromCart(string cartId, int recordingId, int formatId);
    }
}
