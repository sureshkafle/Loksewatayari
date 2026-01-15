using LokFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LokFrontend.Pages.Categories
{
    public class SubCatPageModel : PageModel
    {
        public List<SubCategoryViewModel> SubCategories { get; set; }
        public void OnGet()
        {
        }
    }
}
