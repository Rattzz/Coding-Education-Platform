using efcoreApp.Areas.Identity.Data;
using efcoreApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    [Authorize(Roles = "User")]
    public class UserHomeController : BaseController
    {
        private readonly UserManager<efcoreAppUser> _userManager;
        public UserHomeController(UserManager<efcoreAppUser> userManager, efcoreAppContext context) : base(context)
        {
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }
    }
}
