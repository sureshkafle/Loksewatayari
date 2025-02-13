using QuizCore.Common.Enums;

namespace QuizCore.Modules.Categories;

public class GetCategoryResponse
{
     public int TotalPages {get; set;}
     public List<CategoryDto> Categories { get; set; } =[];
     public class CategoryDto
     {
          public Guid Id { get; set; }
          public required string Title {get;set;}
          public required string Slug {get;set;}
          public int CategoryLevel {get; set;}
          public ActiveStatus ActiveStatus {get;set;}
    }
}
public sealed class CategoryFilterRequest
{
     public int Limit { get; set; } = 50;
     public int Offset { get; set; } = 0;
     public string? Title {get;set;}
     public string? Slug {get;set;}
     public int? CategoryLevel {get; set;}
     public ActiveStatus? ActiveStatus {get;set;}

}
