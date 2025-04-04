using System.Text.Json;
using Microsoft.Extensions.Logging;
using QuizCore.Data.EF;
using QuizEntity=QuizCore.Modules.Quizzes.Entities.Quiz;

namespace MyProject.Data;
public class QuizImportService
{
     private readonly ApplicationDbContext _dbContext;
     private readonly ILogger<QuizImportService> _logger;

     public QuizImportService(ApplicationDbContext dbContext, ILogger<QuizImportService> logger)
     {
          _dbContext = dbContext;
          _logger = logger;
     }

     public async Task ImportQuizAsync(string filePath)
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

               if (!int.TryParse(parts[1], out var age))
               {
                    _logger.LogWarning("Invalid age on line {LineNumber}: {Line}", i + 1, line);
                    continue;
               }

               var quiz = new QuizEntity
               {
                    Id= Guid.NewGuid(),
                    Question=parts[0].Trim(),
                    Options=JsonSerializer.Deserialize<string[]>(parts[1].Trim())!,
                    Answer=parts[2].Trim(),
                    CategoryId= GetCategoryId(parts[3].Trim()),
                    CreatedDate= DateTime.UtcNow,
                    UpdatedDate=DateTime.UtcNow
               };
               if(quiz.CategoryId==Guid.Empty)
               {
                    System.Console.WriteLine($"this question does not contain valid category:  {quiz.Question}");
                    continue;
               }
               _dbContext.Quizzes.Add(quiz);
               await _dbContext.SaveChangesAsync();

               }
          }
          catch (Exception ex)
          {
               _logger.LogError(ex, "Error occurred while importing CSV.");
          }
     }

     private Guid GetCategoryId(string category)
     {
          return _dbContext.Categories
          .Where(x=>x.Slug==category.ToLower())
          .Select(x=>x.Id)
          .FirstOrDefault();
     }
}
