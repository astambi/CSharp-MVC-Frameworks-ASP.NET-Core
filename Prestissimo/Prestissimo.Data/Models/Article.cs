namespace Prestissimo.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.ArticleTitleMinLength)]
        [MaxLength(DataConstants.ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(DataConstants.ArticleContentsMinLength)]
        public string Contents { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public int RecordingId { get; set; }

        public Recording Recording { get; set; }
    }
}
