using QuizCore.Common.Exceptions;

namespace QuizCore.Modules.UserModule.Identities;

public class UpdateOwnPasswordHandler
{
     private readonly IdentityService _service;
     private readonly ICurrentUserService _currentUserService;

     public UpdateOwnPasswordHandler(
          IdentityService service,
          ICurrentUserService currentUserService)
     {
          _service = service;
          _currentUserService = currentUserService;
     }
     public async Task<bool> UpdateOwnPassword(UpdatePasswordRequest request)
     {
          if (request.ConfirmPassword != request.Password)
          {
               throw new CommandExecutionException("Password and ConfirmPassword is not Same");
          }
          if (!PassworValidatorService.PasswordValidator(request.Password))
          {
               throw new CommandExecutionException("Password should contain atleat one lowercase, one uppercase, one number and one special character. For eg Pa$$word123");
          }
          request.Id = _currentUserService.UserId;
          var response = await _service.UpdatePassword(request);
          return response;
     }
}
