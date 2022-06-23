using Quiz.Domain.Models;

namespace Quiz.Application.Services
{
    using Quiz = Domain.Models.Quiz;

    // In more complex systems, I would use MediatR with CQRS where handlers would act like integration services
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> Get();
        Task<Quiz> GetById(int id);
        Task<IEnumerable<Question>> GetQuestionsByQuizId(int quizId);
        Task<IEnumerable<Answer>> GetAnswersByQuizId(int quizId);
        Task<int> EvaluateAnswers(int quizId, int[] answerIds);
    }
}
