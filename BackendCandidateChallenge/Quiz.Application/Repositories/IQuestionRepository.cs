using Quiz.Domain.Models;

namespace Quiz.Application.Repositories
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetQuestionsByQuizId(int quizId);
    }
}
