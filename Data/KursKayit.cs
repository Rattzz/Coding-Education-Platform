namespace efcoreApp.Data
{
    public class KursKayit
    {
        public int KursKayitId { get; set; }

        public string? OgrenciId { get; set; }

        public Ogrenci Ogrenci { get; set; } = null!; 

        public int KursId { get; set; }

        public Kurs Kurs { get; set; } = null!;

        public DateTime KayitTarihi { get; set; }
    }
}