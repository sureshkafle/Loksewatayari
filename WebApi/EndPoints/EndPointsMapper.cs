namespace WebApi.EndPoints;

public static class ManageEndPoints
{
    public static IEndpointRouteBuilder AddEndpoints(this IEndpointRouteBuilder routes)
    {

        routes.MapUserClaimPoints();
        routes.MapUserPoints();
        routes.MapUserAuthApi();
        routes.MapCategoryPoints();
        routes.MapQuizPoints();
        return routes;
    }

}

