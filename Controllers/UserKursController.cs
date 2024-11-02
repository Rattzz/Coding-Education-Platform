using System.Security.Claims;
using efcoreApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    [Authorize(Roles = "User")]
    public class UserKursController : BaseController
    {
        private readonly efcoreAppContext _context;
        public UserKursController(efcoreAppContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kurslar.ToListAsync());
        }

        public async Task<IActionResult> Register(int id)
        {
            var selectedKurs = await _context.Kurslar
                .FirstOrDefaultAsync(k => k.KursId == id);

            if (selectedKurs == null)
            {
                return NotFound();
            }

            var ogrId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Giriş yapan öğrencinin ID'sini alır.

            // Kontrol: Öğrenci zaten bu kursta kayıtlı mı?
            var existingRecord = await _context.KursKayitlari
                .FirstOrDefaultAsync(x => x.OgrenciId == ogrId && x.KursId == selectedKurs.KursId);

            if (existingRecord == null)
            {
                var newKursKayit = new KursKayit
                {
                    OgrenciId = ogrId,
                    KursId = selectedKurs.KursId,
                    KayitTarihi = DateTime.Now
                };

                _context.KursKayitlari.Add(newKursKayit);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Zaten Kayıtlısınız" });
            }

        }


    }
}
