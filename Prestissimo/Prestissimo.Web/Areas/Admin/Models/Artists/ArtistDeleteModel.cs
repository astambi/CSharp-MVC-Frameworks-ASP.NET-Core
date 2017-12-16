namespace Prestissimo.Web.Areas.Admin.Models.Artists
{
    using Data.Models;

    public class ArtistDeleteModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ArtistType ArtistType { get; set; }
    }
}
