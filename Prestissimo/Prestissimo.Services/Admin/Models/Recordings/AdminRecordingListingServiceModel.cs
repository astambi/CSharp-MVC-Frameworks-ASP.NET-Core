namespace Prestissimo.Services.Admin.Models.Recordings
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class AdminRecordingListingServiceModel : IMapFrom<Recording>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string LabelName { get; set; }

        public DateTime ReleaseDate { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Recording, AdminRecordingListingServiceModel>()
                .ForMember(r => r.LabelName, cfg => cfg
                    .MapFrom(r => r.Label.Name));
        }
    }
}
