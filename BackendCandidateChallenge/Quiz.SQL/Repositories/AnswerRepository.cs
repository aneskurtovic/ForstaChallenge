using Quiz.Application.Repositories;
using Quiz.Domain.Models;
using System.Data;
using Dapper;

namespace Quiz.SQL.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly IDbConnection _connection;

        public AnswerRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuizId(int quizId)
        {
            const string answersSql = "SELECT a.Id, a.Text, a.QuestionId FROM Answer a INNER JOIN Question q ON a.QuestionId = q.Id WHERE q.QuizId = @QuizId;";
            var answers = await _connection.QueryAsync<Answer>(answersSql, new { QuizId = quizId });
            return answers;
        }
    }
}
