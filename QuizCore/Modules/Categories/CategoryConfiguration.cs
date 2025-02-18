using Microsoft.Extensions.DependencyInjection;

namespace QuizCore.Modules.Categories;
public static class CategoryConfiguration
{
     public static IServiceCollection AddKpoUserConfigurations(this IServiceCollection services)
     {
          services.AddScoped<CreateCategoryHandler>();
          services.AddScoped<UpdateCategoryHandler>();
          services.AddScoped<CategoryRepository>();
          services.AddScoped<CategoryRelationRepository>();
          return services;
     }
}