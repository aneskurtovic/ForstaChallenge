using Quiz.Domain.Models;

namespace Quiz.Application.Repositories
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<Answer>> GetAnswersByQuizId(int quizId);
    }
}
