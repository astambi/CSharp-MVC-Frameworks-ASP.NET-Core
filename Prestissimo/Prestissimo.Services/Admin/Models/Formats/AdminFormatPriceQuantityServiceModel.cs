namespace Prestissimo.Services.Admin.Models.Formats
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class AdminFormatPriceQuantityServiceModel : IMapFrom<RecordingFormat>, IHaveCustomMapping // todo !!!
    {
        public int Id { get; set; }

        [Display(Name = "Format")]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Discount, %")]
        public double Discount { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<RecordingFormat, AdminFormatPriceQuantityServiceModel>()
                .ForMember(rf => rf.Name, cfg => cfg.MapFrom(rf => rf.Format.Name))
                .ForMember(rf => rf.Id, cfg => cfg.MapFrom(rf => rf.Format.Id));
        }
    }
}
