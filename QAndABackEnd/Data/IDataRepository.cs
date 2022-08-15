using QAndABackEnd.Data.Models;

namespace QAndABackEnd.Data;

public interface IDataRepository
{
    IEnumerable<QuestionGetManyResponse> GetQuestions();
    IEnumerable<QuestionGetManyResponse> GetQuestionsWithAnswers();
    IEnumerable<QuestionGetManyResponse> GetQuestionsBySearch(string search);
    IEnumerable<QuestionGetManyResponse> GetUnansweredQuestions();
    Task<IEnumerable<QuestionGetManyResponse>> GetUnansweredQuestionsAsync();
    IEnumerable<QuestionGetManyResponse> GetQuestionsBySearchingWithPaging(string search, int pageNumber, int pageSize);
    QuestionGetSingleResponse GetQuestion(int questionId);
    bool QuestionExists(int questionId);
    AnswerGetResponse GetAnswer(int answerId);
    QuestionGetSingleResponse PostQuestion(QuestionPostFullRequest question);
    QuestionGetSingleResponse PutQuestion(int questionId, QuestionPutRequest question);
    void DeleteQuestion(int questionId);
    AnswerGetResponse PostAnswer(AnswerPostFullRequest answer);
}