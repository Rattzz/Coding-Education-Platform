namespace efcoreApp.Data
{
    public class Kurs
    {
        public int KursId { get; set; }

        public string? Baslik { get; set; }

        public string? ImageURL { get; set; }

        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

        public ICollection<Video> KursVideos { get; set; } = new List<Video>();

    }
}