using LokFrontend.Application.Models;
namespace LokFrontend.Application.Interfaces;
public interface ICategoryService
{
     Task<List<HomeCategoryViewModel>> GetHomeCategory();
}