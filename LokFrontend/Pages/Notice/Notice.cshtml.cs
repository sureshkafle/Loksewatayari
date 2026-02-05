using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LokFrontend.Application.Models;
using LokFrontend.Application.Interfaces;
namespace LokFrontend.Pages.Notice
{
    public class NoticeModel : PageModel
    {
        private readonly INoticeService _service;
        public NoticeModel(INoticeService service)
        {
            _service=service;
        }
        public List<NoticeViewModel> Notices {get;set;}
        public async Task OnGetAsync()
        {
            Notices= await _service.GetNotices();
        }
    }
}
