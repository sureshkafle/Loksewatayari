using QuizCore.Common.Enums;

namespace QuizCore.Modules.UserModule.Identities;

public class User
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? PasswordHash { get; set; }
    public string? MiddleName { get; set; } = null;
    public string? LastName { get; set; }
    public ActiveStatus IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid LoginIdentifier { get; set; }
    public string? Designation {get; set;}
    public string? Department { get; set; }
    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }

}
