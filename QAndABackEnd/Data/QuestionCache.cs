using QAndABackEnd.Data.Models;

namespace QAndABackEnd.Data;

public class QuestionCache : IQuestionCache
{
    public QuestionGetSingleResponse Get(int questionId)
    {
        throw new NotImplementedException();
    }

    public void Remove(int questionId)
    {
        throw new NotImplementedException();
    }

    public void Set(QuestionGetSingleResponse question)
    {
        throw new NotImplementedException();
    }
}