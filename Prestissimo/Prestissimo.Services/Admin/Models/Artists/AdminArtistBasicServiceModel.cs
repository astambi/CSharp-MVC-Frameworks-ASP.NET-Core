namespace Prestissimo.Services.Admin.Models.Artists
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class AdminArtistBasicServiceModel : IMapFrom<Artist>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ArtistCategory { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Artist, AdminArtistBasicServiceModel>()
                .ForMember(a => a.ArtistCategory, cfg => cfg
                    .MapFrom(a => a.ArtistType.ToString()));
        }
    }
}
