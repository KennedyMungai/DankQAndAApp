using Microsoft.AspNetCore.Mvc;
using QAndABackEnd.Data;
using QAndABackEnd.Data.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace QAndABackEnd.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IDataRepository _dataRepository;
    private readonly IQuestionCache _cache;
    private readonly IHttpClientFactory _clientFactory;
    private readonly string _auth0UserInfo;

    public QuestionsController(
            IDataRepository dataRepository, 
            IQuestionCache questionCache,
            IHttpClientFactory clientFactory,
            IConfiguration configuration)
    {
        _cache = questionCache;
        _clientFactory = clientFactory;
        _dataRepository = dataRepository;
        _auth0UserInfo = $"{configuration["Auth0:Authority"]}userInfo";
    }

    private async Task<string> GetUserName()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _auth0UserInfo);

        request.Headers.Add(
            "Authorization",
            Request.Headers["Authorization"].First()
        );

        var client = _clientFactory.CreateClient();

        var response = await client.SendAsync(request);

        if(response.IsSuccessStatusCode)
        {
            var jsonContent = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<User>(
                jsonContent,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            return user.Name;
        }
        else
        {
            return "";
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IEnumerable<QuestionGetManyResponse> GetQuestions()
    {
        var questions = _dataRepository.GetQuestions();
        return questions;
    }

    [HttpGet]
    [AllowAnonymous]
    public IEnumerable<QuestionGetManyResponse> GetQuestions(string search, bool includeAnswers, int page =1 , int pageSize = 20)
    {
        if (search is null)
        {
            if (includeAnswers)
            {
                return _dataRepository.GetQuestionsWithAnswers();
            }
            else
            {
                return _dataRepository.GetQuestions();
            }
        }
        else
        {
            return _dataRepository.GetQuestionsBySearchingWithPaging(
                search,
                page,
                pageSize
            );
        }
    }

    [HttpGet("unanswered")]
    [AllowAnonymous]
    public async Task<IEnumerable<QuestionGetManyResponse>> GetUnansweredQuestions()
    {
        return await _dataRepository.GetUnansweredQuestionsAsync();
    }

    [HttpGet("{questionId}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<QuestionGetSingleResponse> GetQuestion(int questionId)
    {
        var question = _cache.Get(questionId);

        if (question is null)
        {
            question = _dataRepository.GetQuestion(questionId);

            if(question is null)
            {
                return NotFound();
            }

            _cache.Set(question);
        }

        return question;
    }

    [Authorize(Policy = "MustBeQuestionAuthor")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<QuestionGetSingleResponse>> PostQuestionAsync(QuestionPostRequest questionPostRequest)
    {
        var savedQuestion = _dataRepository.PostQuestion(
            new QuestionPostFullRequest
            {
                Title = questionPostRequest.Title,
                Content = questionPostRequest.Content,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                UserName = await GetUserName(),
                Created = DateTime.Now
            }
        );

        return CreatedAtAction(
            nameof(GetQuestion),
            new { questionId = savedQuestion.QuestionId },
            savedQuestion
        );
    }

    [Authorize(Policy = "MustBeQuestionAuthor")]
    [HttpPut("{questionId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<QuestionGetSingleResponse> PutQuestion(int questionId, QuestionPutRequest questionPutRequest)
    {
        var question = _dataRepository.GetQuestion(questionId);

        if (question is null)
        {
            return NotFound();
        }

        questionPutRequest.Title = string.IsNullOrEmpty(questionPutRequest.Title) ? question.Title : questionPutRequest.Title;
        questionPutRequest.Content = string.IsNullOrEmpty(questionPutRequest.Content) ? question.Content : questionPutRequest.Content;
        
        var savedQuestion = _dataRepository.PutQuestion(questionId, questionPutRequest);

        _cache.Remove(savedQuestion.QuestionId);

        return savedQuestion;
    }

    [Authorize(Policy = "MustBeQuestionAuthor")]
    [HttpDelete("{questionId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteQuestion(int questionId)
    {
        var question = _dataRepository.GetQuestion(questionId);

        if (question is null)
        {
            return NotFound();
        }

        _dataRepository.DeleteQuestion(questionId);

        _cache.Remove(questionId);

        return NoContent();
    }

    [Authorize(Policy = "MustBeQuestionAuthor")]
    [HttpPost("answer")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AnswerGetResponse>> PostAnswer(AnswerPostRequest answerPostRequest)
    {
        var questionExists = _dataRepository.QuestionExists(answerPostRequest.QuestionId.Value);

        if(!questionExists)
        {
            return NotFound();
        }

        var savedAnswer = _dataRepository.PostAnswer(
            new AnswerPostFullRequest
            {
                QuestionId = answerPostRequest.QuestionId.Value,
                Content = answerPostRequest.Content,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                UserName = await GetUserName(),
                Created=DateTime.UtcNow
            }
        );

        _cache.Remove(answerPostRequest.QuestionId.Value);

        return savedAnswer;
    }
}