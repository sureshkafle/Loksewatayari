using QuizCore.Common.Enums;

namespace QuizCore.Modules.Quizzes;

public class GetQuizResponse
{
     public int TotalPages {get; set;}
     public List<QuizDto> Quizzes { get; set; } =[];
     public class QuizDto
     {
          public Guid Id { get; set; }
          public required string Question {get;set;}
          public required string Category {get; set;}
          public ActiveStatus ActiveStatus {get;set;}
    }
}
public sealed class QuizFilterRequest
{
     public int Limit { get; set; } = 50;
     public int Offset { get; set; } = 0;
     public string? Question {get;set;}
     public Guid CategoryId {get;set;}
     public ActiveStatus? ActiveStatus {get;set;}

}
