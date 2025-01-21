using FluentValidation;
using QuizCore.Common.Exceptions;
using QuizCore.Common.ValidationServices;

namespace QuizCore.Modules.UserModule.Identities;

public class UpdatePasswordHandler
{
     private readonly IdentityService _service;

     public UpdatePasswordHandler(IdentityService service)
     {
          _service = service;
     }

     public async Task<bool> UpdatePassword(UpdatePasswordRequest request)
     {
          UpdatePasswordValidator validator = new();
          ValidationService.Validate(await validator.ValidateAsync(request));
          if (request.ConfirmPassword != request.Password)
          {
               throw new CommandExecutionException("Password and ConfirmPassword is not Same");
          }
          if (!PassworValidatorService.PasswordValidator(request.Password))
          {
               throw new CommandExecutionException("Password should contain atleat one lowercase, one uppercase,one number and one special character. Length should Be between 6 and 32 character. For eg Pa$$word123");
          }
          var response = await _service.UpdatePassword(request);
          return response;
     }
}
public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordRequest>
{
     public UpdatePasswordValidator()
     {
          RuleFor(x => x.Password).NotEmpty().NotNull();
          RuleFor(x => x.ConfirmPassword).NotEmpty().NotNull();
          RuleFor(x => x.Id).NotNull().NotEmpty();

     }
}
