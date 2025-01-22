using QuizCore.Common.Enums;

namespace QuizCore.Modules.Quizzes;

public class UpdateQuizRequest
{
     public required Guid Id {get;set;}
     public required string Question {get;set;}
     public string[] Options {get;set;}=[];
     public required string Answer {get; set;}
     public Guid CategoryId {get; set;}
     public ActiveStatus ActiveStatus {get;set;}
     public required DateTime UpdatedDate {get;set;}= DateTime.UtcNow;
}