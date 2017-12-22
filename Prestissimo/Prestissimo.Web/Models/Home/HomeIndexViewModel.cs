namespace Prestissimo.Web.Models.Home
{
    using Prestissimo.Services.Models;
    using System.Collections.Generic;

    public class HomeIndexViewModel : SearchFormModel
    {
        public IEnumerable<CartItemWithDetailsServiceModel> Recordings { get; set; } = new List<CartItemWithDetailsServiceModel>();
    }
}
