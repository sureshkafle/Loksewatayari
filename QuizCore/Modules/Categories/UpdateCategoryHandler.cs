using FluentValidation;
using QuizCore.Common.Helpers;
using QuizCore.Common.ValidationServices;

namespace QuizCore.Modules.Categories;
public class UpdateCategoryHandler
{
     public readonly CategoryRepository _repo;
     public readonly CategoryRelationRepository _categoryRelationRepo;

     public UpdateCategoryHandler(
          CategoryRepository repo,
          CategoryRelationRepository categoryRelationRepo)
     {
          _repo = repo;
          _categoryRelationRepo = categoryRelationRepo;
     }
     public async Task<int> UpdateCategory (UpdateCategoryRequest request)
     {
          UpdateCategoryValidator validator=new();
          ValidationService.Validate(await validator.ValidateAsync(request));
          request.Slug=SlugHelper.Create(true,request.Slug);
          var response= await _repo.Update(request);
          if(response>0 && request.SubCategory.Count()>0)
          {
               await _categoryRelationRepo.DeleteByCategory(request.Id);
               await _categoryRelationRepo.Create(request.Id,request.SubCategory);
          }
          return response;

     }
}
public class UpdateCategoryValidator :AbstractValidator<UpdateCategoryRequest>
{
     public UpdateCategoryValidator()
     {
          RuleFor(x=>x.Title).NotNull().NotEmpty();
          RuleFor(x=>x.Slug).NotNull().NotEmpty();
          RuleFor(x=>x.Description).NotNull().NotEmpty();
          RuleFor(x=>x.ActiveStatus).IsInEnum();
          RuleFor(x=>x.SubCategory).NotEmpty();
     }
}