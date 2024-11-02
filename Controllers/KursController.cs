using Microsoft.AspNetCore.Mvc;
using efcoreApp.Models;
using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace efcoreApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KursController : Controller
    {
        private readonly efcoreAppContext _context;
        public KursController(efcoreAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Kurslar.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(KursViewModel model)
        {
            if (ModelState.IsValid)
            {
                var kurs = new Kurs()
                {
                    Baslik = model.Baslik,
                    ImageURL = model.ImageURL
                };

                _context.Kurslar.Add(kurs);
                await _context.SaveChangesAsync();

                foreach (var videoViewModel in model.VideoList)
                {
                    if (!string.IsNullOrEmpty(videoViewModel.VideoUrl))
                    {
                        var video = new Video
                        {
                            KursId = kurs.KursId,
                            VideoUrl = videoViewModel.VideoUrl,
                            VideoTitle = videoViewModel.VideoTitle
                        };

                        _context.Videos.Add(video);
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                // ModelState geçersiz, hata mesajlarýný view'a gönder
                return View(model);
            }
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurslar
                .FirstOrDefaultAsync(k => k.KursId == id);

            if (kurs == null)
            {
                return NotFound();
            }

            var kursViewModel = new KursViewModel
            {
                KursId = kurs.KursId,
                Baslik = kurs.Baslik,
            };
            return View(kursViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KursViewModel model)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }

            var kurs = await _context.Kurslar.FirstOrDefaultAsync(k => k.KursId == id);

            if (kurs == null)
            {
                return NotFound();
            }

            kurs.Baslik = model.Baslik;
            kurs.ImageURL = model.ImageURL;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }




        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null)
            {
                return NotFound();
            }
            return View("DeleteConfirm", kurs);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Kurs model)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }

            // Kursa ait videolarý bul
            var kursVideos = await _context.Videos.Where(v => v.KursId == id).ToListAsync();

            // Her bir video için VideoWatchedCheck verilerini sil
            foreach (var video in kursVideos)
            {
                var videoWatchedChecks = await _context.VideoWatchedChecks.Where(vc => vc.VideoId == video.VideoId).ToListAsync();
                _context.VideoWatchedChecks.RemoveRange(videoWatchedChecks);
            }

            // Kursa ait videolarý ve VideoWatchedCheck verilerini sil
            _context.RemoveRange(kursVideos);

            // Kursu sil
            _context.Remove(model);
            
            // Deðiþiklikleri kaydet
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var d = await _context.Kurslar.Include(y => y.KursKayitlari).ThenInclude(y => y.Ogrenci).
                FirstOrDefaultAsync(y => y.KursId == id);
            if (d == null)
            {
                return NotFound();
            }
            return View(d);
        }
    }
}

