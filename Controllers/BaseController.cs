using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace efcoreApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly efcoreAppContext _context;

        public BaseController(efcoreAppContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var ogrenci = _context.Ogrenciler.FirstOrDefault(o => o.UserId == userId);

                if (ogrenci != null)
                {
                    ViewBag.UserLevel = ogrenci.Level;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
