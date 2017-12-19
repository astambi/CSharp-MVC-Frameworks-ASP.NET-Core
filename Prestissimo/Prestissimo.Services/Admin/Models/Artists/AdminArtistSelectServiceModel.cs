namespace Prestissimo.Services.Admin.Models.Artists
{
    using Common.Mapping;
    using Data.Models;

    public class AdminArtistSelectServiceModel : IMapFrom<Artist>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
