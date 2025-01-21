namespace QuizCore.Modules.UserModule.Identities;

public class FindAllUserHandler
{
     private readonly IdentityService _identityService;
     private readonly ClaimService _clainService;
     private readonly ICurrentUserService _currentUserService;

    public FindAllUserHandler(
         IdentityService identityService,
         ClaimService clainService,
         ICurrentUserService currentUserService)
    {
        _identityService = identityService;
        _clainService = clainService;
        _currentUserService = currentUserService;
    }
    public async Task<GetUsersResponse> Handle(UserFilterRequest query)
     {
        var isAdminAndHr=  _clainService.AdminClaim(); 
        var userId= _currentUserService.UserId;
        return await _identityService.FindAll(query,isAdminAndHr,userId);      
     }
}