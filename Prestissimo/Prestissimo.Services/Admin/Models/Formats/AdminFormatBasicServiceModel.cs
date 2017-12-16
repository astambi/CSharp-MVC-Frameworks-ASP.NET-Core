namespace Prestissimo.Services.Admin.Models.Formats
{
    using Common.Mapping;
    using Data.Models;

    public class AdminFormatBasicServiceModel : IMapFrom<Format>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
