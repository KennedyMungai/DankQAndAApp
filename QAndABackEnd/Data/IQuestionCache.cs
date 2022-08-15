using QAndABackEnd.Data.Models;

namespace QAndABackEnd.Data;

public interface IQuestionCache
{
    QuestionGetSingleResponse Get(int questionId);
    void Remove(int questionId);
    void Set(QuestionGetSingleResponse question);
}