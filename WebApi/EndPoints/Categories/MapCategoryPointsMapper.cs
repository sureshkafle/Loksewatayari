using QuizCore.Modules.Categories;
using Microsoft.AspNetCore.Mvc;
using WebApi.EndPoints.Responses;
using QuizCore.Common.Exceptions;
using QuizCore.Common.Enums;
namespace WebApi.EndPoints;
public static class MapCategoryPointsMapper
{
    public static RouteGroupBuilder MapCategoryPoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/category").WithTags("Category")
        .RequireAuthorization();
        group.MapPost("/", async ([FromServices] CreateCategoryHandler service,
            CreateCategoryRequest request
            ) =>
        {
            var response = new ApiResultModel<Guid>();
            try
            {
                var res = await service.Handle(request);
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
        group.MapPut("/{id:guid}", async ([FromServices] UpdateCategoryHandler service,
            Guid id,
            UpdateCategoryRequest request
            ) =>
        {
            var response = new ApiResultModel<int>();
            try
            {
                if (id == request.Id)
                {
                    var res = await service.UpdateCategory(request);
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
        group.MapDelete("/{id:guid}", async ([FromServices] CategoryRepository service,
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
        string? title,
        string? slug,
        int categoryLevel,
        ActiveStatus? activeStatus,
        [FromServices] CategoryRepository service)
        =>
        {
            var request = new CategoryFilterRequest
            {
                Offset = offset,
                Title=title,
                Slug = slug,
                CategoryLevel=categoryLevel,
                ActiveStatus = activeStatus
            };
            return await service.GetMany(request);
        });
        group.MapGet("/{id:guid}", async ([FromServices] CategoryRepository service,
            Guid id
            ) =>
        {
            return await service.GetById(id);
        });


        return group;
    }



}

