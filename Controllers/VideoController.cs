using Microsoft.AspNetCore.Mvc;
using efcoreApp.Models;
using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using efcoreApp.Migrations;

namespace efcoreApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VideoController : Controller
    {
        private readonly efcoreAppContext context_;
        public VideoController(efcoreAppContext context)
        {
            context_ = context;
        }
        public IActionResult Index()
        {
            var videos = context_.Videos.ToList(); // Video verilerini alır

            return View(videos); // View'e Video verilerini geçirir
        }

        public IActionResult Create()
        {
            var courses = context_.Kurslar.ToList(); // Var olan kursları alır

            ViewBag.CourseList = new SelectList(courses, "KursId", "Baslik"); // ViewBag kullanarak kursları View'e geçirir

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VideoViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Model doğrulaması geçerli, yeni bir Video nesnesi oluşturun
                var video = new Video
                {
                    KursId = model.KursId,
                    VideoTitle = model.VideoTitle,
                    VideoUrl = model.VideoUrl
                };

                // Video nesnesini veritabanına ekleyin
                context_.Videos.Add(video);
                context_.SaveChanges();

                // Create a VideoWatchedCheck record for each user and mark them as unwatched
                var users = context_.Ogrenciler.ToList();
                foreach (var user in users)
                {
                    var videoWatchedCheck = new VideoWatchedCheck
                    {
                        VideoId = video.VideoId,
                        OgrenciId = user.OgrenciId,
                        Watched = false
                    };
                    context_.VideoWatchedChecks.Add(videoWatchedCheck);
                }
                context_.SaveChanges();

                return RedirectToAction("Index");
            }

            var courses = context_.Kurslar.ToList(); // Var olan kursları alır
            ViewBag.CourseList = new SelectList(courses, "KursId", "Baslik"); // ViewBag kullanarak kursları View'e geçirir

            // Model doğrulaması geçerli değilse, formu tekrar gösterin
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var video = context_.Videos.Find(id);
            if (video == null)
            {
                return NotFound();
            }

            // İlgili VideoWatchedCheck kayıtlarını sil
            var watchedChecks = await context_.VideoWatchedChecks.Where(c => c.VideoId == video.VideoId).ToListAsync();
            context_.VideoWatchedChecks.RemoveRange(watchedChecks);

            context_.Videos.Remove(video);
            context_.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
