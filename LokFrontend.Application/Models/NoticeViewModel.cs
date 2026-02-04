namespace LokFrontend.Application.Models;
public class NoticeViewModel
{
     public required Guid Id {get;set;}
     public required string Title {get;set;}
     public required string Description  {get; set;}
     public required DateTime ExpiryAt { get; set; }
     public required DateTime PublishDate {get;set;}
}