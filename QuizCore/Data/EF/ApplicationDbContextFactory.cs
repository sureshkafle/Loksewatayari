using Microsoft.EntityFrameworkCore.Design;
using QuizCore.Data.EF;
#nullable disable

namespace QuizCore.Data.EF;

public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var cs = "server=localhost;user=admin;password=admin;database=quiz;CharSet=utf8;";
        
        return ApplicationDbContextHelper.CreateDbContext(cs);
    }

}   


