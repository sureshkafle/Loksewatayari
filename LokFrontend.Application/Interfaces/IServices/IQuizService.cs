using LokFrontend.Application.Models;
namespace LokFrontend.Application.Interfaces;
public interface IQuizService
{
     Task<List<QuizViewModel>> GetQuiz();
}