namespace Quiz.Domain.Models;

// Domain models shoudn't be anemic, and should include model behaviour
public class Quiz
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<Question> Questions { get; set; }
}