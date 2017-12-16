namespace Prestissimo.Services.Admin.Models.Artists
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class AdminArtistListingServiceModel : IMapFrom<Artist>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ArtistCategory { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Artist, AdminArtistListingServiceModel>()
                .ForMember(a => a.ArtistCategory, cfg => cfg
                    .MapFrom(a => a.ArtistType.ToString()));
        }
    }
}
