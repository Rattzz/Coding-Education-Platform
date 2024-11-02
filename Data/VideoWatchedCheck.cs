namespace efcoreApp.Data
{
    public class VideoWatchedCheck
    {
        public int VideoWatchedCheckId { get; set; }
        public string? OgrenciId { get; set; }
        public int VideoId { get; set; }
        public bool Watched { get; set; }
    }
}
