using QuizCore.Data.EF;
using QuizCore.Data.Identities;
using QuizCore.Modules.UserModule.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QuizCore.Data;
public static class ApplicationExtensions
{
  
    // public static IServiceCollection AddApplicationJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    // {
    //     var jwtConfig = new JwtConfig(configuration["Jwt:Key"]??string.Empty, 
    //         configuration["Jwt:Issuer"]??string.Empty,
    //         Convert.ToInt32(configuration["Jwt:ExpiresInHour"]??string.Empty));
        
    //     JwtTokenService tokenService = new JwtTokenService(jwtConfig);
    //     services.AddSingleton(tokenService);
    //     services.AddAuthentication(x =>
    //     {
    //         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //         x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;    
    //     })
    //     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,options =>
    //     {
    //         options.Cookie.HttpOnly = true;
    //         options.Cookie.SameSite = SameSiteMode.None;
    //         options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    //         options.LoginPath = "/login";
    //         options.LogoutPath = "/logout";
    //         options.ExpireTimeSpan = TimeSpan.FromDays(1);
    //         options.SlidingExpiration = true;
    //     })
    //     .AddJwtBearer(options =>
    //     {
    //         options.SaveToken = true;
    //         options.RequireHttpsMetadata = false;
    //         options.TokenValidationParameters = new TokenValidationParameters()
    //         {
    //             ValidateIssuer = false,
    //             ValidateAudience = false,
    //             ValidIssuer = configuration["Jwt:Issuer"],
    //             IssuerSigningKey = tokenService.GetSymmetricSecurityKey()
    //         };
    //         options.Events = new JwtBearerEvents
    //         {
    //             OnAuthenticationFailed = context =>
    //             {
    //                 if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
    //                 {
    //                     context.Response.Headers["Token-Expired"] = "true";
    //                 }
    //                 System.Console.WriteLine("TOken failed ");
    //                 return Task.CompletedTask;
    //             }
    //         };
    //     });
        
    //     return services;
    // }

    public static IServiceCollection AddApplicationDbContextAndIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<ApplicationDbContext>(option =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            option.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();                 
        })
        .AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Lockout.AllowedForNewUsers = false;
        }).AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
        return services;
    }

}
