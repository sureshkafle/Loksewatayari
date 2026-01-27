using Dapper;
using LokFrontend.Infrastructure.Data;
using LokFrontend.Application.Models;
using LokFrontend.Application.Interfaces;
namespace LokFrontend.Infrastructure.Repositories;
public class CategoryRepository : ICategoryRepository
{
    private readonly DapperContext _context;

    public CategoryRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<HomeCategoryViewModel>> GetHomeCategory()
    {
        string sql = "Select title, slug, description, id, category_level from categories";
        return await _context.LoadDataAsync<HomeCategoryViewModel>(sql);

    }
}
