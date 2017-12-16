namespace Prestissimo.Data.Models
{
    public class RecordingArtist
    {
        public int RecordingId { get; set; }

        public Recording Recording { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }
    }
}
