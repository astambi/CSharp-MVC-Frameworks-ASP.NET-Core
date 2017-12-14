namespace LearningSystem.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class CourseListingServiceModel : IMapFrom<Course>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Trainer { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Course, CourseListingServiceModel>()
                .ForMember(c => c.Trainer,
                           cfg => cfg.MapFrom(c => $"{c.Trainer.Name} ({c.Trainer.UserName})"));
        }
    }
}
