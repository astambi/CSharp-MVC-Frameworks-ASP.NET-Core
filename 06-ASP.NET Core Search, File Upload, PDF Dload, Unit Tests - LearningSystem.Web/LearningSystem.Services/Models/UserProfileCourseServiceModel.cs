namespace LearningSystem.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System.Linq;

    public class UserProfileCourseServiceModel : IMapFrom<Course>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Grade? Grade { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            // NB! variable in mapper
            string studentId = null;

            mapper
                .CreateMap<Course, UserProfileCourseServiceModel>()
                .ForMember(
                    c => c.Grade,
                    cfg => cfg.MapFrom(c => 
                           c.Students
                            .Where(sc => sc.StudentId == studentId)
                            .Select(sc => sc.Grade)
                            .FirstOrDefault()));
        }
    }
}
