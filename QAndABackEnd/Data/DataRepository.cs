using Microsoft.Data.SqlClient;
using Dapper;

namespace QAndABackEnd.Data;

public class DataRepository : IDataRepository
{
    private readonly string _connectionString;

    public DataRepository(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionStrings:DefaultConnection"];
    }

    public AnswerGetResponse GetAnswer(int answerId)
    {
        throw new NotImplementedException();
    }

    public QuestionGetSingleResponse GetQuestion(int questionId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<QuestionGetManyResponse> GetQuestions()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
    }

    public IEnumerable<QuestionGetManyResponse> GetQuestionsBySearch(string search)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<QuestionGetManyResponse> GetUnansweredQuestions()
    {
        throw new NotImplementedException();
    }

    public bool QuestionExists(int questionId)
    {
        throw new NotImplementedException();
    }
}