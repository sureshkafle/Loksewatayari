using QuizCore.Common.Enums;

namespace QuizCore.Modules.UserModule.Identities;

public class GetUserClaimHandler
{
     private readonly IdentityService _identityService;

     public GetUserClaimHandler(
          IdentityService identityService)
     {
          _identityService = identityService;
     }
     public string[] GetUserListClaims()
     {
          return AvailableUserClaims.GetAvailableClaims();
     }
}


