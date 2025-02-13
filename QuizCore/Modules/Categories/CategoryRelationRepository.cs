using Microsoft.EntityFrameworkCore;
using QuizCore.Data.EF;
using QuizCore.Modules.Categories.Entities;

namespace QuizCore.Modules.Categories;

public class CategoryRelationRepository
{
     private readonly ApplicationDbContext _dbContext;

     public CategoryRelationRepository(ApplicationDbContext dbContext)
     {
          _dbContext = dbContext;
     }

     public async Task<int> Create (Guid categoryId, Guid[] subCategory)
     {
          foreach(var id in subCategory)
          {
               var entity = new CategoryRelation
               {
               Id=Guid.NewGuid(),
               ParentCategoryId=categoryId,
               ChildCategoryId=id,
               };
               _dbContext.CategoryRelations.Add(entity);
          }
          return await _dbContext.SaveChangesAsync();
     }
     public async Task<int> DeleteByCategory(Guid categoryId)
     {
          return await _dbContext.CategoryRelations
          .Where(x=>x.ParentCategoryId==categoryId)
          .ExecuteDeleteAsync();
     }

     
}