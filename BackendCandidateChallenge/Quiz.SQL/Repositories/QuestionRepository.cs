using Quiz.Application.Repositories;
using Quiz.Domain.Models;
using System.Data;
using Dapper;


namespace Quiz.SQL.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IDbConnection _connection;

        public QuestionRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Question>> GetQuestionsByQuizId(int quizId)
        {
            const string questionsSql = "SELECT * FROM Question WHERE QuizId = @QuizId;";
            var questions = await _connection.QueryAsync<Question>(questionsSql, new { QuizId = quizId});
            return questions;
        }
    }
}
