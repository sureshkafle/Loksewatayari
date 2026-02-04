using LokFrontend.Application.Models;
namespace LokFrontend.Application.Interfaces;
public interface INoticeService
{
     Task<List<NoticeViewModel>> GetNotices();
     Task<NoticeDetailViewModel> GetNoticeById(Guid id);
}