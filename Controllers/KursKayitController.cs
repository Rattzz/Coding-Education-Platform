using Microsoft.AspNetCore.Mvc;
using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace efcoreApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KursKayitController : Controller
    {
        private readonly efcoreAppContext _context;
        public KursKayitController(efcoreAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.KursKayitlari.Include(x => x.Ogrenci).Include(x => x.Kurs).ToListAsync());
        }
        
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(),"OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayit model)
        {
            model.KayitTarihi = DateTime.Now;

            // Kontrol: Öðrenci zaten seçili kursa kayýtlý mý?
            var existingRecord = await _context.KursKayitlari
                .FirstOrDefaultAsync(x => x.OgrenciId == model.OgrenciId && x.KursId == model.KursId);

            if (existingRecord != null)
            {
                ModelState.AddModelError("", "Seçilen öðrenci zaten bu kursa kayýtlý.");
                ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
                ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
                return View(model);
            }

            _context.KursKayitlari.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var kayit = await _context.KursKayitlari.FindAsync(id);
            if (kayit == null)
            {
                return NotFound();
            }
            return View("DeleteConfirm", kayit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, KursKayit model)
        {
            if (id != model.KursKayitId)
            {
                return NotFound();
            }
            _context.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}