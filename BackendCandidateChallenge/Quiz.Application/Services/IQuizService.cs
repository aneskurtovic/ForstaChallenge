using Quiz.Domain.Models;

namespace Quiz.Application.Services
{
    using Quiz = Domain.Models.Quiz;

    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> Get();
        Task<Quiz> GetById(int id);
        Task<IEnumerable<Question>> GetQuestionsByQuizId(int quizId);
        Task<IEnumerable<Answer>> GetAnswersByQuizId(int quizId);
    }
}
