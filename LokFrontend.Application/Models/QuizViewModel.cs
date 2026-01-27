namespace LokFrontend.Application.Models;
public class QuizViewModel
{
     public required string Question { get; set; }
     public string[] Options {get;set;}=[];
     public required string Answer { get; set; }
}