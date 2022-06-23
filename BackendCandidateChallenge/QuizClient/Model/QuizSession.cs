using System.Collections.Generic;

namespace QuizClient.Tests;

// We could have used DTO's from API project instead of creating this new one
public class QuizSession
{
    public List<int> SelectedAnswerIds { get; set; }
}

