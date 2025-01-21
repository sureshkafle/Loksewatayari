using QuizCore.Common.Enums;

namespace QuizCore.Modules.UserModule.Identities;

public class GetByIdUserHandler
{
    private readonly IdentityService _identityService;

    public GetByIdUserHandler(
        IdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task<GetUserByIdResponse> FindById(Guid userId)
    {
          var user= await _identityService.FindById(userId);
          return user;
    }
}
