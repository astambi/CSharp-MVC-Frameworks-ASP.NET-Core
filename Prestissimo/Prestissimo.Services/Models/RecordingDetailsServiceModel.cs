namespace Prestissimo.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;

    public class RecordingDetailsServiceModel : IMapFrom<Recording>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int Length { get; set; }

        public string ImageUrl { get; set; }

        public string LabelName { get; set; }

        public IEnumerable<RecordingFormatListingServiceModel> Formats { get; set; } = new List<RecordingFormatListingServiceModel>();

        public IEnumerable<RecordingArtistListingServiceModel> Artists { get; set; } = new List<RecordingArtistListingServiceModel>();

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Recording, RecordingDetailsServiceModel>()
                .ForMember(r => r.LabelName, cfg => cfg.MapFrom(r => r.Label.Name));
        }
    }
}
