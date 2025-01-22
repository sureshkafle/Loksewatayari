using QuizCore.Modules.UserModule.Identities;
using WebApi.EndPoints.Responses;
using Microsoft.AspNetCore.Mvc;
using QuizCore.Common.Enums;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
namespace WebApi.EndPoints;

public static class UserAuthEndpoints
{
    public static RouteGroupBuilder MapUserAuthApi(this IEndpointRouteBuilder routes)
    {
         var group = routes.MapGroup("/api/auth").WithTags("Auth");
        group.MapPost("/login", async( [FromBody] LoginRequestModel requestModel,
            [FromServices] IdentityService identityService, 
            [FromServices] JwtTokenService tokenService,
            HttpContext context)=>{
                  var response  = new ApiResultModel<string>();
            await identityService.UpdateLoginIdentifier(requestModel.UserName);
            User user = await identityService.FindByUserName(requestModel.UserName);
            if (user.UserName!=requestModel.UserName)
            {

                response.Errors.Add("The user you are trying to log in does not exist.");
                return response;
            }
            if (user.IsActive==ActiveStatus.InActive)
            {

                response.Errors.Add("You are trying to login Inactive user");
                return response;
            }

            var isAuthenticated = await identityService.Authenticate(requestModel.UserName, requestModel.Password);
            if (!isAuthenticated)
            {
                response.Errors.Add("Invalid User Credentials");
                return response;
            }
             var claims = await identityService.GetUserClaims(user.Id);
             claims.Add(new Claim(ClaimTypes.Role,"Authenticated"));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
            };
            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );
            var token = tokenService.GenerateJSONWebToken(user, claims.ToList());
            response.SuccessResult(token);
            return response;
            

        });
        group.MapGet("/cookie", (HttpContext context) =>
        {
            var cookie = context.Request.Cookies[CookieAuthenticationDefaults.AuthenticationScheme];
            System.Console.WriteLine("Cookie:"+cookie);
            return cookie != null ? Results.Ok("Cookie found: " + cookie) : Results.NotFound("Cookie not found");
        });
        group.MapDelete("/logout", async (
                            HttpContext context) =>
        {
            var response = new ApiResultModel<string>();
            
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            response.SuccessResult("success");
            return response;
        });
       
        group.MapGet("/userinfo",  (
            [FromServices] ICurrentUserService currentUserService,
            [FromServices] IHttpContextAccessor httpContextAccessor
        ) => {
               var response  = new ApiResultModel<UserInfoModel>();
                string _userCacheKey = "userinfo_"+currentUserService.UserId.ToString();
             
                if(httpContextAccessor.HttpContext==null)
                {
                    response.Errors.Add("Http context is null");
                }
                var claims = httpContextAccessor.HttpContext!.User.Claims;
               if (!claims.Any())  
               {
                    response.Errors.Add("User doest not have any valid claims"); 
                    return response;
               } 
                List<ClaimRespons> ClaimResponss = new();
                ClaimResponss = claims.Select(x => new ClaimRespons
                {
                    ClaimType = x.Type,
                    ClaimValue = x.Value
                }).ToList();
                UserInfoModel userInfo = new()
                {
                    UserId = currentUserService.UserId,
                    UserName = currentUserService.Username,
                    Claims = ClaimResponss
                };
            
            response.SuccessResult(userInfo);
            return response;
        }).RequireAuthorization();
        return group;
    }
}
public class LoginRequestModel
{
    public required string UserName {get;set;}
    public required string Password {get;set;}
}

public class UserInfoModel
{
    public string? UserName { get; set; }
    public Guid UserId { get; set; }
    public List<ClaimRespons> Claims { get; set; }=new();

}
public class ClaimRespons
{
    public required string ClaimType { get; set; }
    public required string ClaimValue { get; set; }
}
