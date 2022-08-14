using Microsoft.AspNetCore.Mvc;
using QAndABackEnd.Data;
using QAndABackEnd.Data.Models;

namespace QAndABackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IDataRepository _dataRepository;

    public QuestionsController(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }

    [HttpGet]
    public IEnumerable<QuestionGetManyResponse> GetQuestions()
    {
        var questions = _dataRepository.GetQuestions();
        return questions;
    }

    [HttpGet]
    public IEnumerable<QuestionGetManyResponse> GetQuestions(string search)
    {
        if (search is null)
        {
            return _dataRepository.GetQuestions();
        }
        else
        {
            return _dataRepository.GetQuestionsBySearch(search);
        }
    }

    [HttpGet("unanswered")]
    public IEnumerable<QuestionGetManyResponse> GetUnansweredQuestions()
    {
        return _dataRepository.GetUnansweredQuestions();
    }

    [HttpGet("{questionId}")]
    public ActionResult<QuestionGetSingleResponse> GetQuestion(int questionId)
    {
        var question = _dataRepository.GetQuestion(questionId);

        if (question is null)
        {
            return NotFound();
        }

        return question;
    }
}