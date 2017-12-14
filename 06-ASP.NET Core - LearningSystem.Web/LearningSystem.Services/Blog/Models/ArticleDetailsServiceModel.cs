namespace LearningSystem.Services.Blog.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class ArticleDetailsServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
               .CreateMap<Article, ArticleDetailsServiceModel>()
               .ForMember(a => a.Author,
                          cfg => cfg.MapFrom(a => $"{a.Author.Name} ({a.Author.UserName})"));
        }
    }
}
