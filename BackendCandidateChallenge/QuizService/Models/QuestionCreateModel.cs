namespace Quiz.API.Readmodels;

// Possibly make base class for this request models with property Text to reduce code duplication
public class QuestionCreateModel
{
    public QuestionCreateModel(string text)
    {
        Text = text;
    }

    public string Text { get; set; }
}