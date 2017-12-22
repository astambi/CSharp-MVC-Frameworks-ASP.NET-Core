namespace Prestissimo.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class RecordingArtistListingServiceModel : IMapFrom<RecordingArtist>, IHaveCustomMapping
    {
        public int ArtistId { get; set; }

        public string ArtistName { get; set; }

        public string ArtistType { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<RecordingArtist, RecordingArtistListingServiceModel>()
                .ForMember(ra => ra.ArtistName, cfg => cfg.MapFrom(ra => ra.Artist.Name))
                .ForMember(ra => ra.ArtistType, cfg => cfg.MapFrom(ra => ra.Artist.ArtistType));
        }
    }
}
