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
     public async Task<GetQuizByIdResponse> GetById (Guid id)
     {
          return await _dbContext.Quizzes
          .Where(x=>x.Id==id)
          .Select(x=> new GetQuizByIdResponse {
               Id=x.Id,
               Question=x.Question,
               Answer=x.Answer,
               Options=x.Options,
               ActiveStatus=x.ActiveStatus
          }).FirstOrDefaultAsync()?? new GetQuizByIdResponse() ;
     }
     public async Task<GetQuizResponse> GetMany (QuizFilterRequest request)
     {
          var response= new GetQuizResponse();
          var q=  _dbContext.Quizzes.AsNoTracking();
          if(request.Question==null)
          {
               q=q.Where(x=>x.Question.Contains(request.Question!));
          }
          if(request.CategoryId!= Guid.Empty)
          {
               q=q.Where(x=>x.CategoryId==request.CategoryId);
          }
          if(request.ActiveStatus==null)
          {
               q=q.Where(x=>x.ActiveStatus==request.ActiveStatus);
          }
          response.Quizzes=await q.Select(x=> new GetQuizResponse.QuizDto {
               Id=x.Id,
               Question=x.Question,
               CategoryId=x.CategoryId,
               ActiveStatus=x.ActiveStatus
          }).Skip(request.Offset).Take(request.Limit).ToListAsync();
          return response;
     }
}
