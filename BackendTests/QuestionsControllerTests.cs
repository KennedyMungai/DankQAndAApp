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
}