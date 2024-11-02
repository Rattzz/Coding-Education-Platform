namespace efcoreApp.Data
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string? QuizName { get; set;}
        public ICollection<Question> QuizQuestions { get; set; } = new List<Question>();
    }
}
