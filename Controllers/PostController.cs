using efcoreApp.Data;
using efcoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace efcoreApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private readonly efcoreAppContext _context;
        public PostController(efcoreAppContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int category)
        {
            IQueryable<Post> query = _context.Posts;

            if (category != 0)
            {
                query = query.Where(p => p.CategoryId == category);
            }

            var posts = await query.OrderByDescending(p => p.PostDate).ToListAsync();

            // ViewBag.Categories'yi ayarlayın
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name");
            return View(posts);
        }



        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Post model)
        {       
            model.PostDate = DateTime.Now;
            _context.Posts.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pst = await _context.Posts.FindAsync(id);
            if (pst == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name");
            return View(pst);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post model)
        {
            if (id != model.PostId)
            {
                return NotFound();
            }

            try
            {
                var postToUpdate = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == id);

                if (postToUpdate == null)
                {
                    return NotFound();
                }

                // Kategori bilgisini değiştirme
                postToUpdate.CategoryId = model.CategoryId;

                // Diğer alanları güncelleme
                postToUpdate.Title = model.Title;
                postToUpdate.Body = model.Body;

                _context.Update(postToUpdate);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Posts.Any(p => p.PostId == model.PostId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
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
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category model)
        {
            _context.Categories.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
