
using QuizCore.Common.Enums;

namespace QuizCore.Modules.UserModule.Identities;
public class CreateUserRequest
{
     public required string FirstName { get; set; }
     public string? MiddleName { get; set; }
     public required string LastName { get; set; }
     public required string UserName { get; set; }
     public required string Email { get; set; }
     public required string Password { get; set; }
     public ActiveStatus ActiveStatus { get; set; }
     public string? PhoneNumber { get; set; }
     public DateTime DateOfBirth { get; set; }
    
}
