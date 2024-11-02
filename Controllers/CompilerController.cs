using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class CompilerController : BaseController
    {
        public CompilerController(efcoreAppContext context) : base(context){ }
        public IActionResult Index()
        {
            return View();
        }
    }
}
