namespace Quiz.Domain.Models;

// Domain models shoudn't be anemic, and should include model behaviour
public class Answer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; }
}