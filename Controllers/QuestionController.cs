using efcoreApp.Data;
using efcoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class QuestionController : Controller
    {
        private readonly efcoreAppContext _context;
        public QuestionController(efcoreAppContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questions.ToListAsync());
        }
        public IActionResult Create()
        {
            
            ViewBag.QuizList = _context.Quizzes.Select(q => new SelectListItem
            {
                Value = q.QuizId.ToString(),
                Text = q.QuizName
            }).ToList();

            var model = new QuestionViewModel();
            model.AnswerList = new List<AnswerViewModel> { new AnswerViewModel() }; 
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(QuestionViewModel model)
        {
                var question = new Question
                {
                    QuizId = model.QuizId,
                    QuestionText = model.QuestionText,
                    QuestionAnswers = model.AnswerList.Select(a => new Answer
                    {
                        AnswerText = a.AnswerText,
                        Correct = a.Correct
                    }).ToList()
                };

                _context.Questions.Add(question);
                _context.SaveChanges();

                return RedirectToAction("Index");
           
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View("DeleteConfirm", question);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Question model)
        {
            if (id != model.QuestionId)
            {
                return NotFound();
            }


            var questionAnswers = await _context.Answers.Where(v => v.QuestionId == id).ToListAsync();


            _context.RemoveRange(questionAnswers);


            _context.Remove(model);


            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
