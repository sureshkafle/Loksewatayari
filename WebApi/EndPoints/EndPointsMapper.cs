namespace WebApi.EndPoints;

public static class ManageEndPoints
{
    public static IEndpointRouteBuilder AddEndpoints(this IEndpointRouteBuilder routes)
    {

        routes.MapUserClaimPoints();
        routes.MapUserPoints();
        return routes;
    }

}

