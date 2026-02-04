using QuizCore.Common.Enums;

namespace QuizCore.Modules.Notices.Entities;
public class Notice
{
     public required Guid Id {get;set;}
     public required string Title {get;set;}
     public string[] File {get;set;}=[];
     public required string Description  {get; set;}
     public string? AttachmentUrl { get; set; }
     public ActiveStatus ActiveStatus {get;set;}
     public required DateTime CreatedDate {get; set;}
     public required DateTime UpdatedDate {get;set;}
     public required DateTime ExpiryAt { get; set; }
     public required DateTime PublishDate {get;set;}
}