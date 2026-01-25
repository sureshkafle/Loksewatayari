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
        public List<int> UserAnswers { get; set; }

        public int Score { get; set; }

        public bool IsSubmitted { get; set; }

        public void OnGet()
        {
            LoadQuestions();
        }

        public void OnPost()
        {
            LoadQuestions();
            IsSubmitted = true;
            Score = 0;

            for (int i = 0; i < Questions.Count; i++)
            {
                if (UserAnswers != null && UserAnswers[i] == Questions[i].CorrectAnswer)
                {
                    Score++;
                }
            }
        }

        private void LoadQuestions()
        {
            Questions = new List<Question>
            {
                new Question {
                    Text = "Capital of Nepal?",
                    Options = new List<string>{ "Pokhara", "Kathmandu", "Butwal", "Biratnagar" },
                    CorrectAnswer = 1
                },
                new Question {
                    Text = "ASP.NET uses?",
                    Options = new List<string>{ "Java", "Python", "C#", "PHP" },
                    CorrectAnswer = 2
                },
                new Question {
                    Text = "HTML stands for?",
                    Options = new List<string>{
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
