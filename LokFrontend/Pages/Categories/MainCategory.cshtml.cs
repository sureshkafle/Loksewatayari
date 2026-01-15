using LokFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LokFrontend.Pages.Categories
{
    public class MainCategoryModel : PageModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public void OnGet()
        {
            Categories = new()
        {
            new CategoryViewModel
            {
                Id = 1,
                Name = "प्रहरी निरीक्षक (जनपद)",
                Description = "Gazetted exams & competitive tests",
                SubCategories =
                {
                    new() { Id = 1, Name = "General Knowledge", PageUrl = "/Categories/SubCatPage" },
                    new() { Id = 2, Name = "IQ", PageUrl = "/Categories/SubCatPage" },
                    new() { Id = 3, Name = "Subjective", PageUrl = "/Categories/SubCatPage" }
                }
            },
            new CategoryViewModel
            {
                Id = 2,
                Name = "प्राविधिक समुह (अफिसर)",
                Description = "Previous year & mock tests",
                SubCategories =
                {
                    new() { Id = 4, Name = "GK & Current Affairs", PageUrl = "/Practice/SubCatPage" },
                    new() { Id = 5, Name = "Maths", PageUrl = "/Practice/SubCatPage" }
                }
            },
            new CategoryViewModel
            {
                Id = 3,
                Name = "प्रहरी सहायक निरीक्षक (जनपद)",
                Description = "Previous year & mock tests",
                SubCategories =
                {
                    new() { Id = 4, Name = "GK & Current Affairs", PageUrl = "/Practice/SubCatPage" },
                    new() { Id = 5, Name = "Maths", PageUrl = "/Practice/SubCatPage" }
                }
            },
            new CategoryViewModel
            {
                Id = 4,
                Name = "प्रहरी जवान (जनपद)",
                Description = "Previous year & mock tests",
                SubCategories =
                {
                    new() { Id = 4, Name = "GK & Current Affairs", PageUrl = "/Practice/SubCatPage" },
                    new() { Id = 5, Name = "Maths", PageUrl = "/Practice/SubCatPage" }
                }
            },
            new CategoryViewModel
            {
                Id = 5,
                Name = "प्रहरी जवान (प्राविधिक)",
                Description = "Previous year & mock tests",
                SubCategories =
                {
                    new() { Id = 4, Name = "GK & Current Affairs", PageUrl = "/Practice/SubCatPage" },
                    new() { Id = 5, Name = "Maths", PageUrl = "/Practice/SubCatPage" }
                }
            },
            new CategoryViewModel
            {
                Id = 6,
                Name = "प्रहरी कार्यालय सहयोगी",
                Description = "Previous year & mock tests",
                SubCategories =
                {
                    new() { Id = 4, Name = "GK & Current Affairs", PageUrl = "/Practice/SubCatPage" },
                    new() { Id = 5, Name = "Maths", PageUrl = "/Practice/SubCatPage" }
                }
            }
        };
        }
    }
}
