using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LokFrontend.Pages.Result
{
    public class ResultModel : PageModel
    {
        public int Score { get; set; }

        public void OnGet()
        {
            Score = HttpContext.Session.GetInt32("score") ?? 0;
        }
    }
}
