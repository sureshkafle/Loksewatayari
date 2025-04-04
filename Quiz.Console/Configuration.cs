using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizCore.Data;

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
        services.AddApplicationDbContextAndIdentity(configuration);

        services.AddScoped<MyService>();
        return services;
    }
}
