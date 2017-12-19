namespace Prestissimo.Services.Admin.Models.Recordings
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using Models.Artists;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminRecordingListingWithArtistsServiceModel : IMapFrom<Recording>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string LabelName { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IEnumerable<AdminArtistBasicServiceModel> ContibutingArtists { get; set; } 
            = new List<AdminArtistBasicServiceModel>();

        public void ConfigureMapping(Profile mapper)
        {
            // NB! Mapping Artists (Id & Name) along with Recording data
            var id = default(int);

            mapper
                .CreateMap<Recording, AdminRecordingListingWithArtistsServiceModel>()
                .ForMember(r => r.LabelName, cfg => cfg
                    .MapFrom(r => r.Label.Name))
                .ForMember(r => r.ContibutingArtists, cfg => cfg
                    .MapFrom(r => r.Artists
                                   .Where(a => a.RecordingId == id)
                                   .Select(a => a.Artist)));
        }
    }
}
