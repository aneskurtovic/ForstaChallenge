namespace Quiz.Application.Repositories
{
    using Quiz = Domain.Models.Quiz;

    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> Get();
        Task<Quiz> GetById(int id);

    }
}
