using efcoreApp.Data;

namespace efcoreApp.Models
{
    public class KurslarViewModel
    {
        public List<int> OgrencininKurslari { get; set; }
        public List<Kurs> Kurslar { get; set; }
    }
}
