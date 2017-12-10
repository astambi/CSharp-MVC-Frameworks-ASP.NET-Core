namespace News.Api.Models.News
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class NewsModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }
    }
}
