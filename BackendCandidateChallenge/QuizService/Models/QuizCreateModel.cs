namespace Quiz.API.Readmodels;

// Possibly make base class for this request models with property Text to reduce code duplication
public class QuizCreateModel
{
    public QuizCreateModel(string title)
    {
        Title = title;
    }

    public string Title { get; set; }
}