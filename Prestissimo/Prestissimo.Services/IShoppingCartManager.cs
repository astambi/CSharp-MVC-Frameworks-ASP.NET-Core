namespace Prestissimo.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IShoppingCartManager
    {
        void AddToCart(string cartId, int recordingId, int formatId);

        IEnumerable<CartItem> GetItems(string cartId);

        void RemoveFromCart(string cartId, int recordingId, int formatId);

        void IncreaseQuantity(string cartId, int recordingId, int formatId);

        void DecreaseQuantity(string cartId, int recordingId, int formatId);

        void Clear(string cartId);
    }
}
