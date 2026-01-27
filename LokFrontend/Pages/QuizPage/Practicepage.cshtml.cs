using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using LokFrontend.Application.Models;
using LokFrontend.Application.Interfaces;

namespace LokFrontend.Pages.QuizPage
{
    public class PracticepageModel : PageModel
    {
        private readonly IQuizService _service;
        public PracticepageModel(IQuizService service)
        {
            _service=service;
        }

        public List<QuizViewModel> Questions { get; set; }

        [BindProperty]
        public List<string> UserAnswers { get; set; }

        public int Score { get; set; }

        public bool IsSubmitted { get; set; }

        public async Task OnGetAsync()
        {
            Questions= await _service.GetQuiz();
            System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(Questions));
        }

        public void OnPost()
        {
            IsSubmitted = true;
            Score = 0;
            System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(Questions));
            for (int i = 0; i < Questions.Count; i++)
            {
                if (UserAnswers != null && UserAnswers[i] == Questions[i].Answer)
                {
                    Score++;
                }
            }
        }

    }
}
