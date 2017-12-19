namespace Prestissimo.Services.Admin.Models.Recordings
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using Services.Admin.Models.Formats;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminRecordingListingWithFormatsServiceModel : IMapFrom<Recording>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string LabelName { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IEnumerable<AdminFormatPriceQuantityServiceModel> AvailableFormats { get; set; }
            = new List<AdminFormatPriceQuantityServiceModel>(); // not mapped, received with an additional query

        public void ConfigureMapping(Profile mapper)
        {
            // NB! Mapping Formats (Id & Name) along with Recording data
            var id = default(int);

            mapper
                .CreateMap<Recording, AdminRecordingListingWithFormatsServiceModel>()
                //.ForMember(r => r.AvailableFormats, cfg => cfg
                //    .MapFrom(r => r.Formats
                //                   .Where(a => a.RecordingId == id)
                //                   .Select(a => a.Format)))
                .ForMember(r => r.LabelName, cfg => cfg
                    .MapFrom(r => r.Label.Name));
        }
    }
}
