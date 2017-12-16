namespace Prestissimo.Services.Admin.Models.Labels
{
    using Common.Mapping;
    using Data.Models;

    public class AdminLabelDetailsToModifyServiceModel : IMapFrom<Label>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
