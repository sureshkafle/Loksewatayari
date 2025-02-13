using QuizCore.Common.Enums;
using QuizCore.Modules.Categories.Entities;

namespace QuizCore.Modules.Quizzes.Entities;
public class Quiz
{
     public required Guid Id {get;set;}
     public required string Question {get;set;}
     public string[] Options {get;set;}=[];
     public required string Answer {get; set;}
     public Guid CategoryId {get; set;}
     public ActiveStatus ActiveStatus {get;set;}
     public required DateTime CreatedDate {get; set;}
     public required DateTime UpdatedDate {get;set;}
     public Category? Category { get; set; }
}