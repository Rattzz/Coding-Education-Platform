using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using efcoreApp.Models;

namespace efcoreApp.Controllers
{
    public class AdminQuizController : Controller
    {
        private readonly efcoreAppContext _context;
        public AdminQuizController(efcoreAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Quizzes.ToListAsync());
        }
        // GET: AdminQuiz/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminQuiz/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(QuizViewModel model)
        {
            if (ModelState.IsValid)
            {
                var quiz = new Quiz
                {
                    QuizName = model.QuizName
                };

                _context.Quizzes.Add(quiz);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            var model = new QuizViewModel
            {
                QuizId = quiz.QuizId,
                QuizName = quiz.QuizName
            };

            return View(model);
        }

        // POST: AdminQuiz/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QuizViewModel model)
        {
            if (id != model.QuizId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var quiz = await _context.Quizzes.FindAsync(id);
                    if (quiz == null)
                    {
                        return NotFound();
                    }

                    quiz.QuizName = model.QuizName;

                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(model.QuizId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.QuizId == id);
        }
    


    public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return View("DeleteConfirm", quiz);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Quiz model)
        {
            if (id != model.QuizId)
            {
                return NotFound();
            }

            
            var quizQuestions = await _context.Questions.Where(v => v.QuizId == id).ToListAsync();

            
            foreach (var video in quizQuestions)
            {
                var questionAnswers = await _context.Answers.Where(vc => vc.QuestionId == video.QuestionId).ToListAsync();
                _context.Answers.RemoveRange(questionAnswers);
            }

            
            _context.RemoveRange(quizQuestions);

            
            _context.Remove(model);

            
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
