using QuizCore.Common.Enums;
using QuizCore.Modules.Quizzes.Entities;

namespace QuizCore.Modules.Categories.Entities;
public class Category
{
     public required Guid Id {get;set;}
     public required string Title {get;set;}
     public required string Slug {get;set;}
     public required string Description {get; set;}
     public int CategoryLevel {get; set;}
     public ActiveStatus ActiveStatus {get;set;}
     public required DateTime CreatedDate {get; set;}
     public required DateTime UpdatedDate {get;set;}
     public  ICollection<Quiz> Quizzes { get; set; }=[];
}
