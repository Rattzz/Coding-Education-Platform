using System.ComponentModel.DataAnnotations;
using efcoreApp.Areas.Identity.Data;
using System.Collections.Generic;

namespace efcoreApp.Data
{
    public class Ogrenci
    {
        // id => primary key
        // [Key] eðer mesela OgrenciId yerine OgrenciKimlik ya da baþka þeyler yazsam bunu yazmam gerekirdi ekstra
        public string? OgrenciId { get; set; }

        public string? UserId { get; set; }

        public string? OgrenciAd { get; set; }

        public string? OgrenciSoyad { get; set; }
        public string? UserName { get; set; }

        public string AdSoyad
        {
            get
            {
                return this.OgrenciAd + " " + this.OgrenciSoyad;
            }
        }

        [EmailAddress]
        public string? Eposta { get; set; }

        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

        public ICollection<VideoWatchedCheck> VideoWatchedChecks { get; set; } = new List<VideoWatchedCheck>();

        public string? ImageURL { get; set; }
        public int Level { get; set; }

    }
}
