namespace Prestissimo.Services.Admin.Models.Artists
{
    using Common.Mapping;
    using Data.Models;

    public class AdminArtistDetailsToModifyServiceModel : IMapFrom<Artist>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ArtistType ArtistType { get; set; }
    }
}
