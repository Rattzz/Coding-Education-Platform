using Microsoft.AspNetCore.Mvc;
using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using efcoreApp.Areas.Identity.Data;

namespace efcoreApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OgrenciController : Controller
    {
        private readonly efcoreAppContext _context;
        private readonly UserManager<efcoreAppUser> _userManager;
        private readonly StudentService _studentService;

        public OgrenciController(efcoreAppContext context, UserManager<efcoreAppUser> userManager, StudentService studentService)
        {
            _context = context;
            _userManager = userManager;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ogrenciler.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> DeleteGet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var ogr = await _context.Ogrenciler.FindAsync(id);
            if (ogr == null)
            {
                return NotFound();
            }

            return View("DeleteConfirm", ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);

            if (ogrenci == null)
            {
                return NotFound();
            }

            // Kullanýcýyý bul ve sil
            var user = await _userManager.FindByIdAsync(ogrenci.UserId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            // Ýlgili VideoWatchedCheck kayýtlarýný sil
            var watchedChecks = await _context.VideoWatchedChecks.Where(c => c.OgrenciId == ogrenci.OgrenciId).ToListAsync();
            _context.VideoWatchedChecks.RemoveRange(watchedChecks);

            // Ýlgili KursKayit kayýtlarýný sil
            var kursKayitlari = await _context.KursKayitlari.Where(k => k.OgrenciId == ogrenci.OgrenciId).ToListAsync();
            _context.KursKayitlari.RemoveRange(kursKayitlari);

            _context.Remove(ogrenci);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var d = await _context.Ogrenciler.Include(y => y.KursKayitlari).ThenInclude(y => y.Kurs)
                .FirstOrDefaultAsync(y => y.OgrenciId == id);
            if(d == null)
            {
                return NotFound();
            }
            return View(d);
        }
    }
}