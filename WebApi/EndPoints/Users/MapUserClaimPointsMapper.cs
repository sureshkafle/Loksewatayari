using QuizCore.Modules.UserModule.Identities;
using Microsoft.AspNetCore.Mvc;
using WebApi.EndPoints.Responses;
using QuizCore.Common.Exceptions;
namespace WebApi.EndPoints;
public static class MapUserClaimPointsMapper
{
    public static RouteGroupBuilder MapUserClaimPoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/user-claim").WithTags("UserClaim")
        .RequireAuthorization("AdminAndHR");
        group.MapPost("/" , async ([FromServices] CreateUserClaimHandler service,
            UserPremissionRequest request
            ) => {
                var response  = new ApiResultModel<bool>();
                try 
                {
                    var res= await service.AddUserClaims(request);
                    response.SuccessResult(res);
                
                }
                catch(ValidationServiceException e)
                {
                    response.Errors = e.ErrorsList;
                }
                catch(CommandExecutionException ex)
                {
                    response.Errors.Add(ex.Message);
                }
                return Results.Ok(response);
                    
                }); 
        
       
        group.MapGet("/{userId:guid}" , async ([FromServices] IdentityService service,
            Guid userId
            ) => {
                return Results.Ok(await service.GetUserClaims(userId));    
                }); 
        group.MapGet("/permission" , ([FromServices] GetUserClaimHandler service
            ) => {
                return Results.Ok( service.GetUserListClaims());    
                }); 
       
        return group;
    }
 
        

}

