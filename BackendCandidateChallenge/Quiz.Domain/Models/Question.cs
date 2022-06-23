namespace Quiz.Domain.Models;

// Domain models shoudn't be anemic, and should include model behaviour
public class Question
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public string Text { get; set; }
    public int CorrectAnswerId { get; set; }
    public IEnumerable<Answer> Answers { get; set; }
}