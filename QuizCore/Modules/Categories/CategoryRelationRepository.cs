using QuizCore.Data.EF;

namespace QuizCore.Modules.Categories;

public class CategoryRelationRepository
{
     private readonly ApplicationDbContext _dbContext;

     public CategoryRelationRepository(ApplicationDbContext dbContext)
     {
          _dbContext = dbContext;
     }
     
}