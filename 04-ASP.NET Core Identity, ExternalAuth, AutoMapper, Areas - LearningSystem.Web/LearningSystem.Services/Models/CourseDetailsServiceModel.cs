namespace LearningSystem.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class CourseDetailsServiceModel : IMapFrom<Course>, IHaveCustomMapping
    {
        
    }
}
