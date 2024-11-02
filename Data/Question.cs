namespace efcoreApp.Data
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public string? QuestionText { get; set; }
        public Quiz? Quiz { get; set; }
        public ICollection<Answer> QuestionAnswers { get; set; } = new List<Answer>();
    }
}
