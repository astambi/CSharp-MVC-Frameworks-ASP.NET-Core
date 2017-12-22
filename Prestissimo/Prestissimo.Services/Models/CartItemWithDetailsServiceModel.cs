namespace Prestissimo.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class CartItemWithDetailsServiceModel : CartItem, IMapFrom<RecordingFormat>, IHaveCustomMapping
    {
        public string RecordingTitle { get; set; }

        public string FormatName { get; set; }

        public string LabelName { get; set; }

        public string ImageUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<RecordingFormat, CartItemWithDetailsServiceModel>()
                .ForMember(rf => rf.RecordingTitle, cfg => cfg.MapFrom(rf => rf.Recording.Title))
                .ForMember(rf => rf.LabelName, cfg => cfg.MapFrom(rf => rf.Recording.Label.Name))
                .ForMember(rf => rf.ImageUrl, cfg => cfg.MapFrom(rf => rf.Recording.ImageUrl))
                .ForMember(rf => rf.ReleaseDate, cfg => cfg.MapFrom(rf => rf.Recording.ReleaseDate))
                .ForMember(rf => rf.FormatName, cfg => cfg.MapFrom(rf => rf.Format.Name));
        }
    }
}
