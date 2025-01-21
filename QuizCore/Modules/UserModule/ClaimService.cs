using QuizCore.Common.Enums;

namespace QuizCore.Modules.UserModule.Identities;

public class ClaimService 
{
    private readonly ICurrentUserService _currentUserService;

    public ClaimService(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }
    public bool AdminClaim()
    {
        return _currentUserService.HasClaim(AvailableClaims.Admin.ToString());
    }
    
    public bool UserClaim()
    {
        return _currentUserService.HasClaim(AvailableUserClaims.GetClaimName(AvailableClaims.User));
    }
    

    public bool DeveloperClaim()
    {
        return _currentUserService.HasClaim(AvailableClaims.Developer.ToString());
    }
}
