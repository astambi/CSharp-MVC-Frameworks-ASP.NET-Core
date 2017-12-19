using Prestissimo.Common.Mapping;

namespace Prestissimo.Services.Models
{
    public class CartItemWithDetailsServiceModel : CartItem
    {
        public string RecordingTitle { get; set; }

        public string FormatName { get; set; }

    }
}
