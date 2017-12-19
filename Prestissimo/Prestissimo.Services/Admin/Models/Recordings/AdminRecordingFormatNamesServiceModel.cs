namespace Prestissimo.Services.Admin.Models.Recordings
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class AdminRecordingFormatNamesServiceModel : IMapFrom<RecordingFormat>, IHaveCustomMapping
    {
        public string RecordingTitle { get; set; }

        public string FormatName { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<RecordingFormat, AdminRecordingFormatNamesServiceModel>()
                .ForMember(rf => rf.RecordingTitle, cfg => cfg.MapFrom(rf => rf.Recording.Title))
                .ForMember(rf => rf.FormatName, cfg => cfg.MapFrom(rf => rf.Format.Name));
        }
    }
}
