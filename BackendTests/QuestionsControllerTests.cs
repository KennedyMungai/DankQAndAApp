using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using QAndABackEnd.Controllers;
using QAndABackEnd.Data;
using QAndABackEnd.Data.Models;

namespace BackendTests;

public class QuestionsControllerTests
{
    [Fact]
    public async void GetQuestions_WhenNoParameters_ReturnsAllQuestions()
    {
        // Given
        var mockQuestions = new List<QuestionGetManyResponse>();
        for (int i = 0; i <= 10; i++)
        {
            mockQuestions.Add(new QuestionGetManyResponse{
                QuestionId = 1,
                Title = $"Test title {i}",
                Content=$"Test Content {i}",
                UserName="User1",
                Answers=new List<AnswerGetResponse>()
            });
        }

        // When
        var mockDataRepository = new Mock<IDataRepository>();
        mockDataRepository
            .Setup(repo => repo.GetQuestions())
            .Returns(() => Task.FromResult(mockQuestions.AsEnumerable()));
        var mockConfigurationRoot = new Mock<IConfigurationRoot>();
        mockConfigurationRoot.SetupGet(
            config => config[It.IsAny<string>()]
        ).Returns("Some setting");

        var questionsController = new QuestionsController(
            mockDataRepository.Object,
            null,
            null,
            mockConfigurationRoot.Object
        );

        var result = await questionsController.GetQuestions(null, false);
        // Then
        Assert.Equal(10, result.Count());
        mockDataRepository.Verify(
            mock => mock.GetQuestions(),
            Times.Once()
        );

    }

    [Fact]
    public async void GetQuestions_WhenHaveSearchParameter_ReturnsCorrectQuestions()
    {
        // Given
        var mockQuestions = new List<QuestionGetManyResponse>();
        mockQuestions.Add(
            new QuestionGetManyResponse{
                QuestionId = 1,
                Title = "test",
                Content = "Test content",
                UserName = "User1",
                Answers = new List<AnswerGetResponse>()
            }
        );

        // When
        var mockDataRepository = new Mock<IDataRepository>();
        mockDataRepository
            .Setup(repo => repo.GetQuestionsBySearchingWithPaging("Test", 1, 20))
            .Returns(() => Task.FromResult(mockQuestions.AsEnumerable()));

        var mockConfigurationRoot = new Mock<IConfigurationRoot>();
        mockConfigurationRoot.SetupGet(
            config => config[It.IsAny<string>()]
        ).Returns("Some settings")l

        var questionsController = new QuestionsController(
            mockDataRepository.Object,
            null,
            null,
            mockConfigurationRoot.Object
        );

        var result = await questionsController.GetQuestions("Test", false);

        // Then
        Assert.Single(result);
        mockDataRepository.Verify(
            mock => mock.GetQuestionsBySearchingWithPaging("Test", 1, 20),
            Times.Once()
        );

    }

    [Fact]
    public async void GetQuestion_WhenQuestionNotFound_Returns404()
    {
        // Given
        var mockDataRepository = new Mock<IDataRepository>();
        mockDataRepository
                .Setup(repo => repo.GetQuestion(1))
                .Returns(() => Task.FromResult(default(QuestionGetSingleResponse)));

        var mockQuestionCache = new Mock<IQuestionCache>();
        mockQuestionCache
                .Setup(cache => cache.Get(1))
                .Returns(() => null);

        var mockConfigurationRoot = new Mock<IConfigurationRoot>();
        mockConfigurationRoot.SetupGet(
            config => config[It.IsAny<string>()]
        ).Returns("Some setting");

        // When

        var questionsController = new QuestionsController(
            mockDataRepository.Object,
            mockQuestionCache.Object,
            null,
            mockConfigurationRoot.Object
        );

        var result = await questionsController.GetQuestion(1);

    
        // Then
        var actionResult = Assert.IsType<ActionResult<QuestionGetSingleResponse>>(result);
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async void GetQuestion_WhenQuestionIsFound_ReturnsQuestion()
    {
        // Given
        var mockQuestion = new QuestionGetSingleResponse{
            QuestionId = 1,
            Title = "Test"
        };

        var mockDataRepository = new Mock<IDataRepository>();
        mockDataRepository
                .Setup(repo => repo.GetQuestion(1))
                .Returns(() => Task.FromResult(mockQuestion));

        var mockQuestionCache = new Mock<IQuestionCache>();
        mockQuestionCache
                .Setup(cache => cache.Get(1))
                .Returns(()=>mockQuestion);

        // When
        var mockConfigurationRoot = new Mock<IConfigurationRoot>();
        mockConfigurationRoot.SetupGet(config => 
            config[It.IsAny<string>()]
        ).Returns("Some settings");

        var questionsController = new QuestionsController(
            mockDataRepository.Object,
            mockQuestionCache.Object,
            null,
            mockConfigurationRoot.Object
        );

        var result = await questionsController.GetQuestion(1);
    
        // Then
        var actionResult = Assert.IsType<ActionResult<QuestionGetSingleResponse>>(result);
        var questionResult = Assert.IsType<QuestionGetSingleResponse>(actionResult.Value);
        Assert.Equal(1, questionResult.QuestionId);
    }
}