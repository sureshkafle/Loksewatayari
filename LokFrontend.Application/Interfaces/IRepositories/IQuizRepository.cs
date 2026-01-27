using LokFrontend.Application.Models;
namespace LokFrontend.Application.Interfaces;
public interface IQuizRepository
{
     Task<List<QuizViewModel>> GetQuiz();
}