using QuizCore.Common.Helpers;

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
          request.Slug=SlugHelper.Create(true,request.Slug);
          var response= await _repo.Create(request);
          if(response!=Guid.Empty && request.SubCategory.Count()>0)
          {
               await _categoryRelationRepo.Create(response,request.SubCategory);
          }
          return response;
     }
}