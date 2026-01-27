using Dapper;
using LokFrontend.Infrastructure.Data;
using LokFrontend.Application.Models;
using LokFrontend.Application.Interfaces;
namespace LokFrontend.Infrastructure.Repositories;
public class QuizRepository : IQuizRepository
{
    private readonly DapperContext _context;

    public QuizRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<QuizViewModel>> GetQuiz()
    {
        string sql = "Select question,options, answer from categories";
        return await _context.LoadDataAsync<QuizViewModel>(sql);

    }
}
