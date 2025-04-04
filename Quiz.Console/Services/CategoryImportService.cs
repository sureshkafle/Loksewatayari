using System.Text.Json;
using Microsoft.Extensions.Logging;
using QuizCore.Common.Enums;
using QuizCore.Common.Helpers;
using QuizCore.Data.EF;
using QuizCore.Modules.Categories.Entities;

namespace MyProject.Data;
public class CategoryImportService
{
     private readonly ApplicationDbContext _dbContext;
     private readonly ILogger<CategoryImportService> _logger;

     public CategoryImportService(ApplicationDbContext dbContext, ILogger<CategoryImportService> logger)
     {
          _dbContext = dbContext;
          _logger = logger;
     }

     public async Task ImportCategoryAsync(string filePath)
     {
          if (!File.Exists(filePath))
          {
               _logger.LogError("CSV file not found at {FilePath}", filePath);
               return;
          }

          try
          {
               var lines = await File.ReadAllLinesAsync(filePath);

               if (lines.Length <= 1)
               {
               _logger.LogWarning("CSV file is empty or has no data.");
               return;
               }

               for (int i = 1; i < lines.Length; i++) // skip header
               {
               var line = lines[i];
               var parts = line.Split(',');

               if (parts.Length < 3)
               {
                    _logger.LogWarning("Skipping malformed line {LineNumber}: {Line}", i + 1, line);
                    continue;
               }

               var category = new Category
               {
                    Id= Guid.NewGuid(),
                    Title=parts[0].Trim(),
                    Slug=SlugHelper.Create(true,parts[1].Trim()),
                    Description=parts[2].Trim(),
                    CategoryLevel=int.Parse( parts[3].Trim()),
                    ActiveStatus=(ActiveStatus)int.Parse(parts[4].Trim()),
                    CreatedDate= DateTime.UtcNow,
                    UpdatedDate=DateTime.UtcNow
               };
               System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(category));
               if(IfSlugAlreadyExist(category.Slug))
               {
                    System.Console.WriteLine($"category already exist");

                    continue;
               }
               _dbContext.Categories.Add(category);
               await _dbContext.SaveChangesAsync();

               }
          }
          catch (Exception ex)
          {
               _logger.LogError(ex, "Error occurred while importing CSV.");
          }
     }

     private bool IfSlugAlreadyExist(string category)
     {
          return _dbContext.Categories
          .Where(x=>x.Slug==category)
          .Any();
     }
}
