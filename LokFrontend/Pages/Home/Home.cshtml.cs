using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LokFrontend.Application.Models;
using LokFrontend.Application.Interfaces;
namespace LokFrontend.Pages.Home
{
    public class HomeModel : PageModel
    {
        private readonly ICategoryService _service;
        public HomeModel(ICategoryService service)
        {
            _service=service;
        }
        public List<HomeCategoryViewModel> categories {get; set;}
        public async Task OnGetAsync()
        {
            categories= await _service.GetHomeCategory();
        }
    }
}
