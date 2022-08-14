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
}