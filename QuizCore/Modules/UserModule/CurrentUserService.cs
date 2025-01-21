using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace QuizCore.Modules.UserModule.Identities;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId=> GetUserId();
    
    public string Username => _httpContextAccessor.HttpContext!.User?.FindFirstValue(ClaimTypes.Name)??string.Empty;


    public bool HasClaim(string claim)
    {
        return HasClaims(new []{claim});
    }
    public bool HasClaims(string[] claims)
    {
        return _httpContextAccessor.HttpContext!.
        User.Claims.Any(c => claims.Contains(c.Value));
    }
    private Guid GetUserId()
    {
        var userId=_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if(userId==null)
        {
            return Guid.Empty;
        }
        return Guid.Parse(userId);
    } 
    

    
}
