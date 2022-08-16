using Microsoft.AspNetCore.Authorization;

namespace QAndABackEnd.Authorization;

public class MustBeQuestionAuthorRequirement : IAuthorizationRequirement
{
    public MustBeQuestionAuthorRequirement()
    {
        
    }
}