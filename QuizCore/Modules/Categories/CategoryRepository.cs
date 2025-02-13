using Microsoft.EntityFrameworkCore;
using QuizCore.Data.EF;
using QuizCore.Modules.Categories.Entities;

namespace QuizCore.Modules.Categories;
public class CategoryRepository
{
     private readonly ApplicationDbContext _dbContext;

     public CategoryRepository(ApplicationDbContext dbContext)
     {
          _dbContext = dbContext;
     }
     public async Task<Guid> Create (CreateCategoryRequest request)
     {
          var entity = new Category 
          {
              Id=Guid.NewGuid(),
              Title=request.Title,
              Slug=request.Slug,
              ActiveStatus=request.ActiveStatus,
              Description=request.Description,
              CategoryLevel=request.CategoryLevel,
              CreatedDate=request.CreatedDate,
              UpdatedDate=request.UpdatedDate

          };
          _dbContext.Categories.Add(entity);
          await _dbContext.SaveChangesAsync();
          return entity.Id;
     }
     public async Task<int> Update (UpdateCategoryRequest request)
     {
          return await _dbContext.Categories
          .Where(x=>x.Id==request.Id)
          .ExecuteUpdateAsync(p=>p.SetProperty(x=>x.Title,x=>request.Title)
          .SetProperty(x=>x.Slug,x=>request.Slug)
          .SetProperty(x=>x.ActiveStatus,x=>request.ActiveStatus)
          .SetProperty(x=>x.Description,x=>request.Description)
          .SetProperty(x=>x.UpdatedDate,x=>request.UpdatedDate));
     }
     public async Task<int> Delete (Guid id)
     {
          return await _dbContext.Categories
          .Where(x=>x.Id==id)
          .ExecuteDeleteAsync();
     }
     public async Task<GetCategoryIdResponse> GetById (Guid id)
     {
          return await _dbContext.Categories
          .Where(x=>x.Id==id)
          .Select(x=> new GetCategoryIdResponse {
               Id=x.Id,
               Title=x.Title,
               Slug=x.Slug,
               Description=x.Description,
               ActiveStatus=x.ActiveStatus
          }).FirstOrDefaultAsync()?? new GetCategoryIdResponse() ;
     }
     public async Task<GetCategoryResponse> GetMany (CategoryFilterRequest request)
     {
          var response= new GetCategoryResponse();
          var q=  _dbContext.Categories.AsNoTracking();
          if(request.Title==null)
          {
               q=q.Where(x=>x.Title.Contains(request.Title!));
          }
          if(request.Slug==null)
          {
               q=q.Where(x=>x.Slug.Contains(request.Slug!));
          }
          if(request.ActiveStatus==null)
          {
               q=q.Where(x=>x.ActiveStatus==request.ActiveStatus);
          }
          response.Categories=await q.Select(x=> new GetCategoryResponse.CategoryDto {
               Id=x.Id,
               Title=x.Title,
               Slug=x.Slug,
               ActiveStatus=x.ActiveStatus
          }).Skip(request.Offset).Take(request.Limit).ToListAsync();
          return response;
     }
}
