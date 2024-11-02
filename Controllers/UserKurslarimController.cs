using System.Security.Claims;
using efcoreApp.Data;
using efcoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    [Authorize(Roles = "User")]
    public class UserKurslarimController : BaseController
    {
        private readonly efcoreAppContext _context;
        private readonly StudentService _studentService;
        public UserKurslarimController(efcoreAppContext context, StudentService studentService) : base(context)
        {
            _context = context;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var viewModel = new KurslarViewModel();

            viewModel.OgrencininKurslari = await _context.KursKayitlari
                .Where(k => k.OgrenciId == currentUserID)
                .Select(kk => kk.KursId)
                .ToListAsync();

            viewModel.Kurslar = await _context.Kurslar
                .ToListAsync();

            return View(viewModel);
        }



        public async Task<IActionResult> Details(int id)
        {
            var selectedKurs = await _context.Kurslar
    .Include(k => k.KursVideos)
        .ThenInclude(v => v.VideoWatchedChecks) // VideoWatchedChecks'i yükle
    .FirstOrDefaultAsync(k => k.KursId == id);


            if (selectedKurs == null)
            {
                return NotFound();
            }

            var ogrId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Giriş yapan öğrencinin ID'sini alır.
            var watchedCheck = selectedKurs.KursVideos.SelectMany(v => v.VideoWatchedChecks)
                                             .FirstOrDefault(c => c.OgrenciId == ogrId);


            return View(selectedKurs);
        }

        
        public IActionResult MarkAsWatched(int videoId)
        {
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var videoWatchedCheck = _context.VideoWatchedChecks.FirstOrDefault(c => c.VideoId == videoId && c.OgrenciId == studentId);

            if (videoWatchedCheck == null)
            {
                // Kayıt bulunamadıysa yeni bir kayıt oluşturun
                videoWatchedCheck = new VideoWatchedCheck
                {
                    OgrenciId = studentId,
                    VideoId = videoId,
                    Watched = true
                };
                _context.VideoWatchedChecks.Add(videoWatchedCheck);
            }
            else
            {
                // Kayıt varsa sadece watched değerini güncelleyin
                videoWatchedCheck.Watched = true;
            }

            _context.SaveChanges();
            _studentService.UpdateStudentLevelsAsync().Wait();

            return Json(new { success = true });
        }

        public IActionResult MarkAsUnwatched(int videoId)
        {
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var videoWatchedCheck = _context.VideoWatchedChecks.FirstOrDefault(c => c.VideoId == videoId && c.OgrenciId == studentId);

            if (videoWatchedCheck != null)
            {
                _context.VideoWatchedChecks.Remove(videoWatchedCheck);
                _context.SaveChanges();
                _studentService.UpdateStudentLevelsAsync().Wait();
            }

            return Json(new { success = true });
        }



    }
}