using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LokFrontend.Application.Models;
using LokFrontend.Application.Interfaces;

namespace LokFrontend.Pages.NoticeDetail
{
    public class Notice_detailModel : PageModel
    {
        private readonly INoticeService _service;
        public Notice_detailModel(INoticeService service)
        {
            _service=service;
        }
        public NoticeDetailViewModel Notice {get;set;}
        public async Task<ActionResult> OnGetAsync(Guid id)
        {
            Notice= await _service.GetNoticeById(id);
            if (Notice == null)
            {
                return NotFound();  // return if notice not found
            }
            return Page();
        }
    }
}
