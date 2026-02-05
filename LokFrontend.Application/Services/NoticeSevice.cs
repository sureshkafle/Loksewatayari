using LokFrontend.Application.Models;
using LokFrontend.Application.Interfaces;
namespace LokFrontend.Application.Services;
public class NoticeService : INoticeService
{
     private readonly INoticeRepository _repo;
     public NoticeService(INoticeRepository repo)
     {
          _repo=repo;
     }
     public async Task<List<NoticeViewModel>> GetNotices()
     {
          return await _repo.GetNotices();
     }
     public async Task<NoticeDetailViewModel> GetNoticeById(Guid id)
     {
          var notice= await _repo.GetNoticeById(id);
          System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(notice));
          return notice;
     }
}