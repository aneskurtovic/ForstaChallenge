using Quiz.Application.Repositories;
using Quiz.Domain.Models;

namespace Quiz.Application.Services
{
    using Quiz = Domain.Models.Quiz;

    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _repository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;

        public QuizService(IQuizRepository repository, IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _repository = repository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }
        public async Task<IEnumerable<Quiz>> Get()
        {
            var quizzes = await _repository.Get();

            return quizzes;
        }

        public async Task<Quiz> GetById(int id)
        {
            // This can be refactored to be a single call to DB instead of 3 separate calls
            var quiz = await _repository.GetById(id);

            var questions = await _questionRepository.GetQuestionsByQuizId(id);

            var answers = await _answerRepository.GetAnswersByQuizId(id);

            return new Quiz
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Questions = questions.Select(x => new Question
                {
                    Id = x.Id,
                    Text = x.Text,
                    CorrectAnswerId = x.CorrectAnswerId,
                    Answers = answers.Where(y => y.QuestionId == x.Id)
                })
            };
        }

        public async Task<IEnumerable<Question>> GetQuestionsByQuizId(int quizId)
        {
            var questions = await _questionRepository.GetQuestionsByQuizId(quizId);

            return questions;
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuizId(int quizId)
        {
            var answers = await _answerRepository.GetAnswersByQuizId(quizId);

            return answers;
        }

        public async Task<int> EvaluateAnswers(int quizId, int[] answerIds)
        {
            var quiz = await GetById(quizId);
            var score = 0;

            if (quiz.Questions.Count() < answerIds.Count())
            {
                throw new ArgumentException("You cannot have more selected answers than questions in this quizz");
            };

            for (int i = 0; i < answerIds.Count(); i++)
            {
                var question = await _answerRepository.GetById(answerIds[i]);

                if (question == null)
                    continue;

                var correctAnswer = quiz.Questions.Single(x => x.Id == question.QuestionId).CorrectAnswerId;

                if (correctAnswer == answerIds[i])
                    score++;
            }

            return score;
        }
    }
}
