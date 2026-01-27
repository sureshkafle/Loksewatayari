using LokFrontend.Application.Models;
namespace LokFrontend.Application.Interfaces;
public interface ICategoryRepository
{
     Task<List<HomeCategoryViewModel>> GetHomeCategory();
}