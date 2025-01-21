using FluentValidation;
using QuizCore.Common.DateTimeProviders;
using QuizCore.Common.Enums;
using QuizCore.Common.Exceptions;
using QuizCore.Common.ValidationServices;

namespace QuizCore.Modules.UserModule.Identities;

public class CreateUserHandler
{
    private readonly IdentityService _identityService;
    private readonly IDateTime _dateTime;

    public CreateUserHandler(
        IdentityService identityService,
        IDateTime dateTime)
    {
        _identityService = identityService;
        _dateTime = dateTime;
    }

    public async Task<Guid> CreateUser(CreateUserRequest request)
    {
        CreateUserValidator validator = new();
        ValidationService.Validate(await validator.ValidateAsync(request));
        request.UserName = request.UserName.Trim().ToLower();
        request.DateOfBirth= _dateTime.ConvertToUtc(request.DateOfBirth);
        if (!PassworValidatorService.PasswordValidator(request.Password))
        {
            throw new CommandExecutionException("Password should contain atleat one lowercase, one uppercase,one number and one special character . Length should Be between 6 and 32 character. For eg Pa$$word123");
        }
        var isExist = await _identityService.FindByNameAsync(request.UserName);
        var isMailExist = await _identityService.FindByEmailAsync(request.Email);
        if (isExist is not null)
        {
            throw new CommandExecutionException($"We regret to inform you that the username {request.UserName} is already in use. Please choose a different username to proceed with creating your account.");
        }
        if (isMailExist is not null)
        {
            throw new CommandExecutionException($"We regret to inform you that the email {request.Email} is already in use. Please choose a different email to proceed with creating your account.");
        }
        var userId = await _identityService.Create(request);
        return userId;
    }

}
public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().NotNull();
        RuleFor(x => x.LastName).NotEmpty().NotNull();
        RuleFor(x => x.Email).NotEmpty().NotNull();
        RuleFor(x => x.Password).NotEmpty().NotNull();
        RuleFor(x => x.ActiveStatus).IsInEnum();
        RuleFor(x => x.UserName).NotEmpty()
        .Matches("^[a-zA-Z0-9]+$")
        .WithMessage("Username can only contain letters and numbers.");
       
    }
}
