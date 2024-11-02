using efcoreApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    [Authorize(Roles = "User")]
    public class UserPostController : BaseController
    {
        private readonly efcoreAppContext _context;
        public UserPostController(efcoreAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int category, string searchQuery)
        {
            IQueryable<Post> query = _context.Posts;

            // Kategoriye göre filtreleme
            if (category != 0)
            {
                query = query.Where(p => p.CategoryId == category);
            }

            // Arama sorgusuna göre filtreleme
            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Arama sorgusunu küçük harfe dönüştürme
                searchQuery = searchQuery.ToLower();
                query = query.Where(p => p.Title.ToLower().Contains(searchQuery));
            }

            var posts = await query.OrderByDescending(p => p.PostDate).ToListAsync();

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name");

            return View(posts);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.
                FirstOrDefaultAsync(y => y.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
    }
}
