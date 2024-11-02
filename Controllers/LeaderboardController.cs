using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class LeaderboardController : BaseController
    {
        private readonly efcoreAppContext _context;
        public LeaderboardController(efcoreAppContext context) : base(context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var students = _context.Ogrenciler.OrderByDescending(s => s.Level).ToList();

            return View(students);
        }
    }
}
