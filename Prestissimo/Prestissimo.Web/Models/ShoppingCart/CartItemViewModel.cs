namespace Prestissimo.Web.Models.ShoppingCart
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class CartItemViewModel : IMapFrom<RecordingFormat>, IHaveCustomMapping
    {
        public int RecordingId { get; set; }

        public string RecordingTitle { get; set; }

        public int FormatId { get; set; }

        public string FormatName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public double Discount { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<RecordingFormat, CartItemViewModel>()
                .ForMember(rf => rf.RecordingTitle, cfg => cfg.MapFrom(rf => rf.Recording.Title))
                .ForMember(rf => rf.FormatName, cfg => cfg.MapFrom(rf => rf.Format.Name));
        }
    }
}
