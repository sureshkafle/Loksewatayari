using QuizCore.Common.Enums;

namespace QuizCore.Modules.UserModule.Identities;

public class UpdateUserRequest
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public ActiveStatus ActiveStatus { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? PhoneNumber {get;set;}
    public DateTime DateOfBirth {get;set;}=DateTime.UtcNow;
    
}
