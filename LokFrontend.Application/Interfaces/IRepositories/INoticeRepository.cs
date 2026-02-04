using LokFrontend.Application.Models;
namespace LokFrontend.Application.Interfaces;
public interface INoticeRepository
{
     Task<List<NoticeViewModel>> GetNotices();
     Task<NoticeDetailViewModel> GetNoticeById(Guid id);
}