using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace LokFrontend.Pages.QuizPage
{
    public class PracticepageModel : PageModel
    {
        public class Question
        {
            public string Text { get; set; }
            public List<string> Options { get; set; }
            public int CorrectAnswer { get; set; }
        }

        public List<Question> Questions { get; set; }

        [BindProperty]
        public int SelectedAnswer { get; set; }

        public int CurrentIndex { get; set; }
        public int Score { get; set; }

        public void OnGet()
        {
            LoadQuestions();

            HttpContext.Session.SetInt32("index", 0);
            HttpContext.Session.SetInt32("score", 0);

            CurrentIndex = 0;
        }

        public IActionResult OnPostNext()
        {
            LoadQuestions();

            CurrentIndex = HttpContext.Session.GetInt32("index") ?? 0;
            Score = HttpContext.Session.GetInt32("score") ?? 0;

            if (SelectedAnswer == Questions[CurrentIndex].CorrectAnswer)
            {
                Score++;
            }

            CurrentIndex++;
            HttpContext.Session.SetInt32("index", CurrentIndex);
            HttpContext.Session.SetInt32("score", Score);

            if (CurrentIndex >= Questions.Count)
            {
                return RedirectToPage("/Result/Result");
            }

            return Page();
        }

        private void LoadQuestions()
        {
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "What is the capital of Nepal?",
                    Options = new List<string> { "Pokhara", "Kathmandu", "Biratnagar", "Lalitpur" },
                    CorrectAnswer = 1
                },
                new Question
                {
                    Text = "Which language is used in ASP.NET?",
                    Options = new List<string> { "Python", "Java", "C#", "PHP" },
                    CorrectAnswer = 2
                },
                new Question
                {
                    Text = "HTML stands for?",
                    Options = new List<string> {
                        "Hyper Text Markup Language",
                        "High Text Machine Language",
                        "Hyperlinks Text Mark Language",
                        "None"
                    },
                    CorrectAnswer = 0
                }
            };
        }
    }
}

