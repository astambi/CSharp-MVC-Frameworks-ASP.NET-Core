namespace Prestissimo.Services.Admin.Models
{
    using Common.Mapping;
    using Data.Models;

    public class AdminArtistListingServiceModel : IMapFrom<Artist>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ArtistType ArtistType { get; set; }
    }
}
