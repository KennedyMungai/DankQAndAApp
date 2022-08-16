using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QAndABackEnd.Data;

namespace QAndABackEnd.Authorization;

public class MustBeQuestionAuthorHandler : AuthorizationHandler<MustBeQuestionAuthorRequirement>
{
    private readonly IDataRepository _dataRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MustBeQuestionAuthorHandler(IDataRepository dataRepository, IHttpContextAccessor httpContextAccessor)
    {
        _dataRepository = dataRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, MustBeQuestionAuthorRequirement requirement)
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return;
        }

        var questionId = _httpContextAccessor.HttpContext.Request.RouteValues["questionId"];
        int questionIdAsInt = Convert.ToInt32(questionId);
    }
}