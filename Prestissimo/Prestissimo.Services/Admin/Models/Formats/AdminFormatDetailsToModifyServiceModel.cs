namespace Prestissimo.Services.Admin.Models.Formats
{
    using Common.Mapping;
    using Data.Models;

    public class AdminFormatDetailsToModifyServiceModel : IMapFrom<Format>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
