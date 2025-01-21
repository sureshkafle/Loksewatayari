using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using QuizCore.Data.Identities;
using QuizCore.Modules.Categories.Entities;
using QuizCore.Modules.Quizzes.Entities;

namespace QuizCore.Data.EF;

public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    private DbSet<Category> Categories { get; set; }
    private DbSet<CategoryRelation> CategoryRelations {get;set;}
    private DbSet<Quiz> Quizzes {get; set;}
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        IdentityAndUsers(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }
    private static void IdentityAndUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>().ToTable("users");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("roles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        modelBuilder.Entity<ApplicationUser>().Property(s => s.LoginIdentifier).HasMaxLength(36).IsRequired();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DatabaseFacade GetDatabase()
    {
        return base.Database;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}