using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using efcoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using efcoreApp.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using efcoreApp.Data;

namespace efcoreApp.Controllers;

[Authorize(Roles ="Admin")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<efcoreAppUser> _userManager;
    private readonly efcoreAppContext _context;

    public HomeController(ILogger<HomeController> logger, UserManager<efcoreAppUser> userManager, efcoreAppContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _context = context;
    }

    public int GetStudentCount()
    {
        return _context.Ogrenciler.Count();
    }

    public int GetCourseCount()
    {
        return _context.Kurslar.Count();
    }

    public int GetEnrollmentCount()
    {
        return _context.KursKayitlari.Count();
    }

    public int GetVideoCount()
    {
        return _context.Videos.Count();
    }

    public int GetPostCount()
    {
        return _context.Posts.Count();
    }
    public int GetQuizCount()
    {
        return _context.Quizzes.Count();
    }
    public int GetQuestionCount()
    {
        return _context.Questions.Count();
    }

    public IActionResult Index()
    {
        var model = new AdminDashboardViewModel
        {
            StudentCount = GetStudentCount(),
            CourseCount = GetCourseCount(),
            EnrollmentCount = GetEnrollmentCount(),
            VideoCount = GetVideoCount(),
            PostCount = GetPostCount(),
            QuizCount = GetQuizCount(),
            QuestionCount = GetQuestionCount(),
        };
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
