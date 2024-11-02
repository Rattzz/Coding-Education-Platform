namespace efcoreApp.Data
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public bool Correct { get; set; }
        public Question? Question { get; set; }
    }
}
