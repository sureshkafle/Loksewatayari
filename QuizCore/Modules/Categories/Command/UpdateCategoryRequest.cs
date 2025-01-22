using QuizCore.Common.Enums;

namespace QuizCore.Modules.Categories;

public class UpdateCategoryRequest
{
     public Guid Id {get;set;}
     public required string Title {get;set;}
     public required string Slug {get;set;}
     public required string Description {get; set;}
     public ActiveStatus ActiveStatus {get;set;}
     public Guid[] SubCategory {get;set;}=[];
     public required DateTime UpdatedDate {get;set;}=DateTime.UtcNow;
}
