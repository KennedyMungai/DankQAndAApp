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

    public QuestionsController(IDataRepository dataRepository, IQuestionCache questionCache)
    {
        _cache = questionCache;
        _dataRepository = dataRepository;
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
    public ActionResult<QuestionGetSingleResponse> PostQuestion(QuestionPostRequest questionPostRequest)
    {
        var savedQuestion = _dataRepository.PostQuestion(
            new QuestionPostFullRequest
            {
                Title = questionPostRequest.Title,
                Content = questionPostRequest.Content,
                UserId = "1",
                UserName = "example@email.com",
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
    public ActionResult<AnswerGetResponse> PostAnswer(AnswerPostRequest answerPostRequest)
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
                UserId = "1",
                UserName = "example@email.com",
                Created=DateTime.UtcNow
            }
        );

        _cache.Remove(answerPostRequest.QuestionId.Value);

        return savedAnswer;
    }
}