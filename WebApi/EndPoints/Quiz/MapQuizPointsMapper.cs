using QuizCore.Modules.Quizzes;
using Microsoft.AspNetCore.Mvc;
using WebApi.EndPoints.Responses;
using QuizCore.Common.Exceptions;
using QuizCore.Common.Enums;
namespace WebApi.EndPoints;
public static class MapQuizPointsMapper
{
    public static RouteGroupBuilder MapQuizPoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/quiz").WithTags("Quiz")
        .RequireAuthorization();
        group.MapPost("/", async ([FromServices] CreateQuizHandler service,
            CreateQuizRequest request
            ) =>
        {
            var response = new ApiResultModel<Guid>();
            try
            {
                var res = await service.Handler(request);
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
        group.MapPut("/{id:guid}", async ([FromServices] UpdateQuizHandler service,
            Guid id,
            UpdateQuizRequest request
            ) =>
        {
            var response = new ApiResultModel<int>();
            try
            {
                if (id == request.Id)
                {
                    var res = await service.Handler(request);
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
        group.MapDelete("/{id:guid}", async ([FromServices] QuizRepository service,
            Guid id
            ) =>
        {
            var response = new ApiResultModel<int>();
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
        string? question,
        Guid categoryId,
        int QuizLevel,
        ActiveStatus? activeStatus,
        [FromServices] QuizRepository service)
        =>
        {
            var request = new QuizFilterRequest
            {
                Offset = offset,
                Question=question,
                CategoryId = categoryId,
                ActiveStatus = activeStatus
            };
            return await service.GetMany(request);
        });
        group.MapGet("/{id:guid}", async ([FromServices] QuizRepository service,
            Guid id
            ) =>
        {
            return await service.GetById(id);
        });


        return group;
    }



}

