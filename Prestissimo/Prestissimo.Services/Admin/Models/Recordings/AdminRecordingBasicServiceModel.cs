namespace Prestissimo.Services.Admin.Models.Recordings
{
    using Common.Mapping;
    using Data.Models;

    public class AdminRecordingBasicServiceModel : IMapFrom<Recording>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
