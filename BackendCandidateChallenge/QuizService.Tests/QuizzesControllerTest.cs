using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Quiz.API;
using Quiz.API.Models;
using Quiz.API.Readmodels;
using Xunit;

namespace QuizService.Tests;

public class QuizzesControllerTest
{
    const string QuizApiEndPoint = "/api/quizzes/";

    [Fact]
    public async Task PostNewQuizAddsQuiz()
    {
        var quiz = new QuizCreateModel("Test title");
        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(quiz));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"),
                content);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Headers.Location);
        }
    }

    [Fact]
    public async Task AQuizExistGetReturnsQuiz()
    {
        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            const long quizId = 1;
            var response = await client.GetAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}{quizId}"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var quiz = JsonConvert.DeserializeObject<QuizResponseModel>(await response.Content.ReadAsStringAsync());
            Assert.Equal(quizId, quiz.Id);
            Assert.Equal("My first quiz", quiz.Title);
        }
    }

    [Fact]
    public async Task AQuizDoesNotExistGetFails()
    {
        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            const long quizId = 999;
            var response = await client.GetAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}{quizId}"));
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    [Fact]

    public async Task AQuizDoesNotExists_WhenPostingAQuestion_ReturnsNotFound()
    {
        const string QuizApiEndPoint = "/api/quizzes/999/questions";

        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            const long quizId = 999;
            var question = new QuestionCreateModel("The answer to everything is what?");
            var content = new StringContent(JsonConvert.SerializeObject(question));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"), content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    [Fact]

    public async Task GivenTwoCorrectAnswers_WhenEvaluateIsCalled_ReturnsTwo()
    {
        const string QuizApiEndPoint = "/api/quizzes/1/evaluate";

        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();

            var request = new EvaluateRequest
            {
                SelectedAnswerIds = new List<int> { 1, 5 }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"), content);
            var result = response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, int.Parse(result.Result));
        }
    }

    [Fact]

    public async Task GivenAllIncorrectAnswers_WhenEvaluateIsCalled_ReturnsZero()
    {
        const string QuizApiEndPoint = "/api/quizzes/1/evaluate";

        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();

            var request = new EvaluateRequest
            {
                SelectedAnswerIds = new List<int> { 2, 3 }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"), content);
            var result = response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(0, int.Parse(result.Result));
        }
    }

    [Fact]

    public async Task GivenAllNonexistingAnswers_WhenEvaluateIsCalled_ReturnsZeroScore()
    {
        const string QuizApiEndPoint = "/api/quizzes/1/evaluate";

        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();

            var request = new EvaluateRequest
            {
                SelectedAnswerIds = new List<int> { 0, 15 }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"), content);
            var result = response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(0, int.Parse(result.Result));
        }
    }

    [Fact]

    public async Task GivenMoreSelectedAnswersThenTotalInQuiz_WhenEvaluateIsCalled_ReturnsBadRequest()
    {
        const string QuizApiEndPoint = "/api/quizzes/1/evaluate";

        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();

            var request = new EvaluateRequest
            {
                SelectedAnswerIds = new List<int> { 0, 15, 22 }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"), content);
            var result = response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}