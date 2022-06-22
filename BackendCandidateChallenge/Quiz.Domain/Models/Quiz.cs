namespace Quiz.Domain.Models;

public class Quiz
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<Question> Questions { get; set; }
}