using Microsoft.AspNetCore.Identity;
namespace QuizCore.Data.Identities;
public class ApplicationUser : IdentityUser<Guid>
{
    public required string FullName { get; set; }
     public Guid LoginIdentifier { get; set; }
}
