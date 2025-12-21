using LokFrontend.Enums;
namespace LokFrontend.Modules;
public class Category
{
     public required Guid Id {get;set;}
     public required string Title {get;set;}
     public required string Slug {get;set;}
     public int CategoryLevel {get; set;}
     public ActiveStatus ActiveStatus {get;set;}
}