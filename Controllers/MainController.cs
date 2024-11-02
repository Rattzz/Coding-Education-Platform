using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace efcoreApp.Controllers
{
    [AllowAnonymous]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
