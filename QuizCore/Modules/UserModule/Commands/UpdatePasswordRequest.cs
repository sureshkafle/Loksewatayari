namespace QuizCore.Modules.UserModule.Identities;

public class UpdatePasswordRequest
{
    public Guid Id { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}