namespace Quiz.API.Models;

// Possibly make base class for this request models with property Text to reduce code duplication
public class AnswerCreateModel
{
    public AnswerCreateModel(string text)
    {
        Text = text;
    }

    public string Text { get; set; }
}