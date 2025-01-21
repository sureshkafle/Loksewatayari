using FluentValidation;
using QuizCore.Common.Enums;
using QuizCore.Common.ValidationServices;

namespace QuizCore.Modules.UserModule.Identities;

public class UpdateUserHandler
{
    private readonly IdentityService _identityService;

    public UpdateUserHandler(
        IdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<int> UpdateUser(UpdateUserRequest request)
    {
          UpdateUserValidator validator = new();
          ValidationService.Validate(await validator.ValidateAsync(request));
          return await _identityService.Update(request);
    }
}
public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.ActiveStatus).IsInEnum<UpdateUserRequest, ActiveStatus>();
    }
}
