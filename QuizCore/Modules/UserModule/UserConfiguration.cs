using QuizCore.Modules.UserModule.Identities;
using Microsoft.Extensions.DependencyInjection;

namespace QuizCore.Modules.UserModule;
public static class UserConfiguration
{
     public static IServiceCollection AddKpoUserConfigurations(this IServiceCollection services)
     {
          services.AddScoped<CreateUserClaimHandler>();
          services.AddScoped<UpdateOwnPasswordHandler>();
          services.AddScoped<UpdatePasswordHandler>();
          services.AddScoped<UpdateUserHandler>();
          services.AddScoped<GetByIdUserHandler>();
          services.AddScoped<CreateUserHandler>();
          services.AddScoped<GetUserClaimHandler>();
          services.AddScoped<FindAllUserHandler>();
          services.AddScoped<IdentityService>();
          services.AddTransient<ICurrentUserService, CurrentUserService>();

          services.AddScoped<ClaimService>();
          return services;
     }
}