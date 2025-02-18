using Microsoft.Extensions.DependencyInjection;

namespace QuizCore.Modules.Quizzes;
public static class QuizConfiguration
{
     public static IServiceCollection AddQuizConfigurations(this IServiceCollection services)
     {
          services.AddScoped<CreateQuizHandler>();
          services.AddScoped<UpdateQuizHandler>();
          services.AddScoped<QuizRepository>();
          return services;
     }
}