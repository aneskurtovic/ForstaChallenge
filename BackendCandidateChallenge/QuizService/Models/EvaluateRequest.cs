using System.Collections.Generic;

namespace Quiz.API.Models
{
    public class EvaluateRequest
    {
        public List<int> SelectedAnswerIds { get; set; }
    }
}
