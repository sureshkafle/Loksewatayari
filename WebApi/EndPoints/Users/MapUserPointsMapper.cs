using QuizCore.Modules.UserModule.Identities;
using Microsoft.AspNetCore.Mvc;
using WebApi.EndPoints.Responses;
using QuizCore.Common.Exceptions;
using QuizCore.Common.Enums;
namespace WebApi.EndPoints;
public static class MapUserPointsMapper
{
    public static RouteGroupBuilder MapUserPoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/user").WithTags("User")
        .RequireAuthorization();
        group.MapPost("/", async ([FromServices] CreateUserHandler service,
            CreateUserRequest request
            ) =>
        {
            var response = new ApiResultModel<Guid>();
            try
            {
                var res = await service.CreateUser(request);
                response.SuccessResult(res);

            }
            catch (ValidationServiceException e)
            {
                response.Errors = e.ErrorsList;
            }
            catch (CommandExecutionException ex)
            {
                response.Errors.Add(ex.Message);
            }
            return Results.Ok(response);

        }).RequireAuthorization("AdminAndHR");
        group.MapPut("/{id:guid}", async ([FromServices] UpdateUserHandler service,
            Guid id,
            UpdateUserRequest request
            ) =>
        {
            var response = new ApiResultModel<int>();
            try
            {
                if (id == request.Id)
                {
                    var res = await service.UpdateUser(request);
                    response.SuccessResult(res);
                }
                else
                {
                    response.Errors.Add("Requested Id does not match");
                }
            }
            catch (ValidationServiceException e)
            {
                response.Errors = e.ErrorsList;
            }
            catch (CommandExecutionException ex)
            {
                response.Errors.Add(ex.Message);
            }
            return Results.Ok(response);

        }).RequireAuthorization("AdminAndHR");
        group.MapDelete("/{id:guid}", async ([FromServices] IdentityService service,
            Guid id
            ) =>
        {
            var response = new ApiResultModel<bool>();
            try
            {
                var res = await service.Delete(id);
                response.SuccessResult(res);

            }
            catch (CommandExecutionException ex)
            {
                response.Errors.Add(ex.Message);
            }
            return Results.Ok(response);

        }).RequireAuthorization("AdminAndHR");
        group.MapGet("/", async (int offset,
        string? username,
        string? designation,
        ActiveStatus? activeStatus,
        [FromServices] FindAllUserHandler service)
        =>
        {
            var request = new UserFilterRequest
            {
                Offset = offset,
                Username = username,
                ActiveStatus = activeStatus
            };
            return await service.Handle(request);
        });
        group.MapGet("/{id:guid}", async ([FromServices] GetByIdUserHandler service,
            Guid id
            ) =>
        {
            return Results.Ok(await service.FindById(id));
        });

        group.MapPut("/own-password", async ([FromServices] UpdateOwnPasswordHandler service,
            UpdatePasswordRequest request
            ) =>
        {
            var response = new ApiResultModel<bool>();
            try
            {
                var res = await service.UpdateOwnPassword(request);
                response.SuccessResult(res);
            }
            catch (ValidationServiceException e)
            {
                response.Errors = e.ErrorsList;
            }
            catch (CommandExecutionException ex)
            {
                response.Errors.Add(ex.Message);
            }
            return Results.Ok(response);

        });
        group.MapPut("/password/{userId:guid}", async ([FromServices] UpdatePasswordHandler service,
        UpdatePasswordRequest request,
        Guid userId
        ) =>
        {
            if(userId != request.Id)
            {

                return Results.BadRequest();
            }
            var response = new ApiResultModel<bool>();
            try
            {
                var res = await service.UpdatePassword(request);
                response.SuccessResult(res);
            }
            catch (ValidationServiceException e)
            {
                response.Errors = e.ErrorsList;
            }
            catch (CommandExecutionException ex)
            {
                response.Errors.Add(ex.Message);
            }
            return Results.Ok(response);

        }).RequireAuthorization("AdminAndHR");
        group.MapGet("/locked-user", async ([FromServices] IdentityService service
        ) =>
        {
            return Results.Ok(await service.GetLockedUser());
        }).RequireAuthorization("AdminAndHR");

        group.MapPut("/unlock/{id:guid}", async ([FromServices] IdentityService service,
            Guid id
        ) =>
        {
            var response = new ApiResultModel<int>();
            try
            {
                var res = await service.UnlockedUser(id);
                response.SuccessResult(res);
            }
            catch (ValidationServiceException e)
            {
                response.Errors = e.ErrorsList;
            }
            catch (CommandExecutionException ex)
            {
                response.Errors.Add(ex.Message);
            }
            return Results.Ok(response);

        }).RequireAuthorization("AdminAndHR");

        return group;
    }



}

