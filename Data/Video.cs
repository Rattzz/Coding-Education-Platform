using System.Collections.Generic;

namespace efcoreApp.Data
{
    public class Video
    {
        public int VideoId { get; set; }
        public int KursId { get; set; } // Hangi kursa ait olduğunu belirtmek için
        public string? VideoUrl { get; set; }
        public string? VideoTitle { get; set; }
        public Kurs? Kurs { get; set; } // Kurs ile ilişkilendirme
        public ICollection<VideoWatchedCheck> VideoWatchedChecks { get; set; } = new List<VideoWatchedCheck>();
    }
}
