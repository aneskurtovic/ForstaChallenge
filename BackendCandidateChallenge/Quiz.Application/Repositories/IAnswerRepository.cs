using Quiz.Domain.Models;

namespace Quiz.Application.Repositories
{
    public interface IAnswerRepository
    {
        Task<Answer> GetById(int id);
        Task<IEnumerable<Answer>> GetAnswersByQuizId(int quizId);
    }
}
