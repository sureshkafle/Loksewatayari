using QuizCore.Data.EF;

namespace QuizCore.Modules.Categories;
public class CategoryRepository
{
     private readonly ApplicationDbContext _dbContext;

     public CategoryRepository(ApplicationDbContext dbContext)
     {
          _dbContext = dbContext;
     }

}
