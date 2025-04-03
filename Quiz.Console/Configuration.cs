using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Quiz.Console;

public static class KpoConfiguration
{

    public static IServiceCollection ConfigureConsoleServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var defaultConnectionString = new DefaultConnectionString();
        defaultConnectionString.SetConnectionString(
            configuration.GetConnectionString("DefaultConnection")!
        );

        services.AddScoped<MyService>();
        return services;
    }
}
