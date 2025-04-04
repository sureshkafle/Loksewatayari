using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Data;
using QuizCore.Data;

namespace Quiz.Console;

public static class Configuration
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
        services.AddScoped<CategoryImportService>();
        services.AddScoped<QuizImportService>();
        return services;
    }
}
