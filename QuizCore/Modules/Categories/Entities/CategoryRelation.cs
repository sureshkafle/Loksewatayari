namespace QuizCore.Modules.Categories.Entities;

public class CategoryRelation
{
     public required Guid Id {get;set;}
     public required Guid ParentCategoryId { get; set; }
     public required Guid ChildCategoryId { get; set; }
     public Category ParentCategory { get; set; } = null!;
     public Category ChildCategory { get; set; } = null!;
}
