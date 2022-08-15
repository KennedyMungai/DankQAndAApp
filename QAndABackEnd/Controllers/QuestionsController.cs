using Microsoft.AspNetCore.Mvc;
using QAndABackEnd.Data;
using QAndABackEnd.Data.Models;

namespace QAndABackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IDataRepository _dataRepository;
    private readonly IQuestionCache questionCache;

    public QuestionsController(IDataRepository dataRepository, IQuestionCache questionCache)
    {
            this.questionCache = questionCache;
        _dataRepository = dataRepository;
    }

    [HttpGet]
    public IEnumerable<QuestionGetManyResponse> GetQuestions()
    {
        var questions = _dataRepository.GetQuestions();
        return questions;
    }

    [HttpGet]
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
    public async Task<IEnumerable<QuestionGetManyResponse>> GetUnansweredQuestions()
    {
        return await _dataRepository.GetUnansweredQuestionsAsync();
    }

    [HttpGet("{questionId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<QuestionGetSingleResponse> GetQuestion(int questionId)
    {
        var question = _dataRepository.GetQuestion(questionId);

        if (question is null)
        {
            return NotFound();
        }

        return question;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
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

    [HttpPut("{questionId}")]
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

        return savedQuestion;
    }

    [HttpDelete("{questionId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteQuestion(int questionId)
    {
        var question = _dataRepository.GetQuestion(questionId);

        if (question is null)
        {
            return NotFound();
        }

        _dataRepository.DeleteQuestion(questionId);

        return NoContent();
    }

    [HttpPost("answer")]
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

        return savedAnswer;
    }
}