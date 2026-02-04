namespace LokFrontend.Application.Models;
public class NoticeDetailViewModel
{
     public required Guid Id {get;set;}
     public required string Title {get;set;}
     public string[] File {get;set;}=[];
     public required string Description  {get; set;}
     public string? AttachmentUrl { get; set; }
     public required DateTime ExpiryAt { get; set; }
     public required DateTime PublishDate {get;set;}
}