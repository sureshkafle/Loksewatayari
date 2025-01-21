using QuizCore.Common.Enums;

namespace QuizCore.Modules.UserModule.Identities;

public class GetUserByIdResponse
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public ActiveStatus ActiveStatus { get; set; }
    public string? PhoneNumber {get;set;}
    public DateTime DateOfBirth {get;set;}=DateTime.UtcNow;
}