using FluentValidation;
using QuizCore.Common.ValidationServices;

namespace QuizCore.Modules.Quizzes;
public class CreateQuizHandler
{
     public readonly QuizRepository _repo;

     public CreateQuizHandler(QuizRepository repo)
     {
          _repo = repo;
     }
     public async Task<Guid> Handler (CreateQuizRequest request)
     {
          CreateQuizValidator validator=new();
          ValidationService.Validate(await validator.ValidateAsync(request));
          var response= await _repo.Create(request);
          return response;
     }
}
public class CreateQuizValidator :AbstractValidator<CreateQuizRequest>
{
     public CreateQuizValidator()
     {
          RuleFor(x=>x.Question).NotNull().NotEmpty();
          RuleFor(x=>x.Answer).NotNull().NotEmpty();
          RuleFor(x=>x.ActiveStatus).IsInEnum();
          RuleFor(x=>x.Options).NotEmpty();
     }
}
