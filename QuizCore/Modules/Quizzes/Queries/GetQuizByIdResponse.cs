using QuizCore.Common.Enums;

namespace QuizCore.Modules.Quizzes;

public class GetQuizByIdResponse
{
     public Guid Id {get;set;}
     public string? Question {get;set;}
     public string[] Options {get;set;}=[];
     public string? Answer {get; set;}
     public ActiveStatus ActiveStatus {get;set;}
}