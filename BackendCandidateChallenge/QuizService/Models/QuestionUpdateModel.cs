namespace Quiz.API.Readmodels;

// Possibly make base class for this request models with property Text to reduce code duplication
public class QuestionUpdateModel
{
    public string Text { get; set; }
    public int CorrectAnswerId { get; set; }
}