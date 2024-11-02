using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Models
{
    public class QuizViewModel
    {
        public int QuizId { get; set; }

        [Required(ErrorMessage = "Quiz Name is Required")]
        public string? QuizName { get; set; }
        public int QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public int AnswerId { get; set; }
        public string? AnswerText { get; set; }
        public bool Correct { get; set; }
        
        public List<QuestionViewModel>? QuestionList { get; set; }
    }

    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Question Text is required")]
        public int QuizId { get; set; }
        public string? QuestionText { get; set;}
        public int AnswerId { get;set; }
        public string? AnswerText { get;set; }
        public bool Correct { get; set; }

        public List<AnswerViewModel>? AnswerList { get; set; }
    }
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Answer Text is required")]
        public string? AnswerText { get; set; }
        public bool Correct { get; set; }
    }
}
