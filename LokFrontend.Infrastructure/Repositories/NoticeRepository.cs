using Dapper;
using LokFrontend.Infrastructure.Data;
using LokFrontend.Application.Models;
using LokFrontend.Application.Interfaces;
namespace LokFrontend.Infrastructure.Repositories;
public class NoticeRepository :INoticeRepository
{
     private readonly DapperContext _context;
     public NoticeRepository(DapperContext context)
     {
          _context=context;
     }
     public async Task<List<NoticeViewModel>> GetNotices()
     {
          var sql ="select title , description, publish_date from notices";
          return await _context.LoadDataAsync<NoticeViewModel>(sql);
     }

     public async Task<NoticeDetailViewModel> GetNoticeById(Guid id)
     {
          var sql=@"select title,file, description,attachment_url, publish_date, 
          expiry_at from notices where id=@Id";
          return await _context.LoadSingleDataAsync<NoticeDetailViewModel,object>(sql,new {Id=id});
     }
}