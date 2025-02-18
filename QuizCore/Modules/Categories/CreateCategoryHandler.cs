using FluentValidation;
using QuizCore.Common.Helpers;
using QuizCore.Common.ValidationServices;

namespace QuizCore.Modules.Categories;
public class CreateCategoryHandler
{
     public readonly CategoryRepository _repo;
     public readonly CategoryRelationRepository _categoryRelationRepo;
     public CreateCategoryHandler(
          CategoryRepository repo,
          CategoryRelationRepository categoryRelationRepo)
     {
          _repo = repo;
          _categoryRelationRepo = categoryRelationRepo;
     }
     public async Task<Guid> Handle (CreateCategoryRequest request)
     {
          CreateCategoryValidator validator=new();
          ValidationService.Validate(await validator.ValidateAsync(request));
          request.Slug=SlugHelper.Create(true,request.Slug);
          var response= await _repo.Create(request);
          if(response!=Guid.Empty && request.SubCategory.Count()>0)
          {
               await _categoryRelationRepo.Create(response,request.SubCategory);
          }
          return response;
     }
}

public class CreateCategoryValidator :AbstractValidator<CreateCategoryRequest>
{
     public CreateCategoryValidator()
     {
          RuleFor(x=>x.Title).NotNull().NotEmpty();
          RuleFor(x=>x.Slug).NotNull().NotEmpty();
          RuleFor(x=>x.Description).NotNull().NotEmpty();
          RuleFor(x=>x.ActiveStatus).IsInEnum();
          RuleFor(x=>x.CategoryLevel).NotEmpty();
          RuleFor(x=>x.SubCategory).NotEmpty();
     }
}