using QuizCore.Common.Enums;

namespace QuizCore.Modules.Categories;
public class CreateCategoryRequest
{
     public required string Title {get;set;}
     public required string Slug {get;set;}
     public required string Description {get; set;}
     public int CategoryLevel {get; set;}
     public ActiveStatus ActiveStatus {get;set;}
     public Guid[] SubCategory {get;set;}=[];
     public required DateTime CreatedDate {get; set;}=DateTime.UtcNow;
     public required DateTime UpdatedDate {get;set;}=DateTime.UtcNow;
}
