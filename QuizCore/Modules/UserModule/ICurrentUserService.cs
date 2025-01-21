namespace QuizCore.Modules.UserModule.Identities;

public interface ICurrentUserService
{
   Guid UserId { get; }
   string Username {get; }

   bool HasClaim(string claim);

   bool HasClaims (string[] claims);
   
}
