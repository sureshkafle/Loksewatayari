using Microsoft.EntityFrameworkCore;
using QuizCore.Data.EF;

namespace QuizCore.Data.EF;
public static class ApplicationDbContextHelper
{
public static ApplicationDbContext CreateDbContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();

        return new ApplicationDbContext(optionsBuilder.Options);
    }
    
}
