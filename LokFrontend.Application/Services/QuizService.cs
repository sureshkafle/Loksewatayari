using LokFrontend.Application.Models;
using LokFrontend.Application.Interfaces;
namespace LokFrontend.Application.Services;
public class QuizService :IQuizService
{
     private readonly IQuizRepository _repo;
     public QuizService (IQuizRepository repo)
     {
          _repo=repo;
     }
     public async Task<List<QuizViewModel>> GetQuiz()
     {
          return await _repo.GetQuiz();
     }
}