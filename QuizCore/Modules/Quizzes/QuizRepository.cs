using Microsoft.EntityFrameworkCore;
using QuizCore.Data.EF;
using QuizCore.Modules.Quizzes.Entities;

namespace QuizCore.Modules.Quizzes;
public class QuizRepository
{
     private readonly ApplicationDbContext _dbContext;

     public QuizRepository(ApplicationDbContext dbContext)
     {
          _dbContext = dbContext;
     }
     public async Task<Guid> Create (CreateQuizRequest request)
     {
          var entity = new Quiz 
          {
              Id=Guid.NewGuid(),
              Question=request.Question,
              Options=request.Options,
              Answer=request.Answer,
              CategoryId=request.CategoryId,
              ActiveStatus=request.ActiveStatus,
              CreatedDate=request.CreatedDate,
              UpdatedDate=request.UpdatedDate

          };
          _dbContext.Quizzes.Add(entity);
          await _dbContext.SaveChangesAsync();
          return entity.Id;
     }
     public async Task<int> Update (UpdateQuizRequest request)
     {
          return await _dbContext.Quizzes
          .Where(x=>x.Id==request.Id)
          .ExecuteUpdateAsync(p=>p.SetProperty(x=>x.Question,x=>request.Question)
          .SetProperty(x=>x.Options,x=>request.Options)
          .SetProperty(x=>x.ActiveStatus,x=>request.ActiveStatus)
          .SetProperty(x=>x.Answer,x=>request.Answer)
          .SetProperty(x=>x.UpdatedDate,x=>request.UpdatedDate));
     }
     public async Task<int> Delete (Guid id)
     {
          return await _dbContext.Quizzes
          .Where(x=>x.Id==id)
          .ExecuteDeleteAsync();
     }
     // public async Task<GetQuizIdResponse> GetById (Guid id)
     // {
     //      return await _dbContext.Categories
     //      .Where(x=>x.Id==id)
     //      .Select(x=> new GetQuizIdResponse {
     //           Id=x.Id,
     //           Title=x.Title,
     //           Slug=x.Slug,
     //           Description=x.Description,
     //           ActiveStatus=x.ActiveStatus
     //      }).FirstOrDefaultAsync()?? new GetQuizIdResponse() ;
     // }
     // public async Task<GetQuizResponse> GetMany (QuizFilterRequest request)
     // {
     //      var response= new GetQuizResponse();
     //      var q=  _dbContext.Categories.AsNoTracking();
     //      if(request.Title==null)
     //      {
     //           q=q.Where(x=>x.Title.Contains(request.Title!));
     //      }
     //      if(request.Slug==null)
     //      {
     //           q=q.Where(x=>x.Slug.Contains(request.Slug!));
     //      }
     //      if(request.ActiveStatus==null)
     //      {
     //           q=q.Where(x=>x.ActiveStatus==request.ActiveStatus);
     //      }
     //      response.Categories=await q.Select(x=> new GetQuizResponse.QuizDto {
     //           Id=x.Id,
     //           Title=x.Title,
     //           Slug=x.Slug,
     //           ActiveStatus=x.ActiveStatus
     //      }).Skip(request.Offset).Take(request.Limit).ToListAsync();
     //      return response;
     // }
}
