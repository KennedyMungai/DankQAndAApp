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

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MustBeQuestionAuthorRequirement requirement)
    {
        throw new NotImplementedException();
    }
}