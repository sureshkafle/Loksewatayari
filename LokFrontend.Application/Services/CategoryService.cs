using LokFrontend.Application.Models;
using LokFrontend.Application.Interfaces;
namespace LokFrontend.Application.Services;
public class CategoryService :ICategoryService
{
     private readonly ICategoryRepository _repo;
     public CategoryService (ICategoryRepository repo)
     {
          _repo=repo;
     }
     public async Task<List<HomeCategoryViewModel>> GetHomeCategory()
     {
          return await _repo.GetHomeCategory();
     }
}