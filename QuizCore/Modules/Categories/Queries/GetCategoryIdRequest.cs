using QuizCore.Common.Enums;

namespace QuizCore.Modules.Categories;

public class GetCategoryIdResponse
{
     public Guid Id {get;set;}
     public string? Title {get;set;}
     public string? Slug {get;set;}
     public string? Description {get; set;}
     public ActiveStatus ActiveStatus {get;set;}
     public Guid[] SubCategory {get;set;}=[];
}