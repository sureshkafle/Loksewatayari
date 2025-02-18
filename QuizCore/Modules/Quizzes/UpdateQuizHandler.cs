using FluentValidation;
using QuizCore.Common.ValidationServices;

namespace QuizCore.Modules.Quizzes;

public class UpdateQuizHandler
{
public readonly QuizRepository _repo;

     public UpdateQuizHandler(QuizRepository repo)
     {
          _repo = repo;
     }
     public async Task<int> Handler (UpdateQuizRequest request)
     {
          UpdateQuizValidator validator=new();
          ValidationService.Validate(await validator.ValidateAsync(request));
          var response= await _repo.Update(request);
          return response;
     }
}
public class UpdateQuizValidator :AbstractValidator<UpdateQuizRequest>
{
     public UpdateQuizValidator()
     {
          RuleFor(x=>x.Question).NotNull().NotEmpty();
          RuleFor(x=>x.Answer).NotNull().NotEmpty();
          RuleFor(x=>x.ActiveStatus).IsInEnum();
          RuleFor(x=>x.Options).NotEmpty();
     }
}
