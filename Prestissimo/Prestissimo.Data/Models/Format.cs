namespace Prestissimo.Data.Models
{
    using System.Collections.Generic;

    public class Format
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<RecordingFormat> Recordings { get; set; } = new List<RecordingFormat>();
    }
}
