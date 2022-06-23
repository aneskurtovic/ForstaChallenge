using Quiz.Application.Repositories;
using Dapper;
using System.Data;

namespace Quiz.SQL.Repositories
{
    using Quiz = Domain.Models.Quiz;

    public class QuizRepository : IQuizRepository
    {
        private readonly IDbConnection _connection;

        public QuizRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Quiz>> Get()
        {
            const string sql = "SELECT * FROM Quiz;";

            var quiz = await _connection.QueryAsync<Quiz>(sql);

            return quiz;
        }

        public async Task<Quiz> GetById(int id)
        {
            const string quizSql = "SELECT * FROM Quiz WHERE Id = @Id;";

            var quiz = await _connection.QuerySingleAsync<Quiz>(quizSql, new { Id = id });

            if (quiz == null)
                throw new KeyNotFoundException();
            
            return quiz;
        }
    }
}
