using Microsoft.AspNetCore.Identity;
using QuizCore.Common.Enums;
namespace QuizCore.Data.Identities;
public class ApplicationUser : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public ActiveStatus ActiveStatus {get;set;}
    public required DateTime CreatedDate {get;set;}
    public required DateTime UpdatedDate {get;set;}
    public required DateTime DateOfBirth {get;set;}
     public Guid LoginIdentifier { get; set; }
}
