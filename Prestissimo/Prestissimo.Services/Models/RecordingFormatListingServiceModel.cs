namespace Prestissimo.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class RecordingFormatListingServiceModel : IMapFrom<RecordingFormat>, IHaveCustomMapping
    {
        public int FormatId { get; set; }

        public string FormatName { get; set; }

        public decimal Price { get; set; }

        public double Discount { get; set; }

        public int Quantity { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<RecordingFormat, RecordingFormatListingServiceModel>()
                .ForMember(rf => rf.FormatName, cfg => cfg.MapFrom(rf => rf.Format.Name));
        }
    }
}
