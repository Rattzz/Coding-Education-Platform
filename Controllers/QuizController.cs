using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using efcoreApp.Models;
using System;
using System.Linq;

namespace efcoreApp.Controllers
{
    public class QuizController : BaseController
    {
        private readonly efcoreAppContext _context;
        public QuizController(efcoreAppContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Quizzes.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .Include(q => q.QuizQuestions)
                    .ThenInclude(q => q.QuestionAnswers)
                .FirstOrDefaultAsync(q => q.QuizId == id);

            if (quiz == null)
            {
                return NotFound();
            }

            var quizViewModel = new QuizViewModel
            {
                QuizId = quiz.QuizId,
                QuizName = quiz.QuizName,
                QuestionList = quiz.QuizQuestions
                    .Select(question => new QuestionViewModel
                    {
                        QuestionId = question.QuestionId,
                        QuizId = question.QuizId,
                        QuestionText = question.QuestionText,
                        AnswerList = question.QuestionAnswers
                            .OrderBy(a => Guid.NewGuid()) // Rastgele sıralama yap
                            .Select(answer => new AnswerViewModel
                            {
                                AnswerId = answer.AnswerId,
                                QuestionId = answer.QuestionId,
                                AnswerText = answer.AnswerText,
                                Correct = answer.Correct
                            }).ToList()
                    })
                    .OrderBy(q => Guid.NewGuid()) // Soruları rastgele sırala
                    .ToList()
            };

            return View(quizViewModel);
        }
    }
}
