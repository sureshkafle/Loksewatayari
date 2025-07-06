using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

namespace MyProject.Data;
public class RemoveHtmlTagsService
{
    public void RemoveHtml(string filePath)
    {

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found!");
            return;
        }

        // Read file content
        string htmlContent = File.ReadAllText(filePath);

        // Remove HTML and CSS
        string plainText = RemoveHtmlAndCss(htmlContent);

        // Output cleaned text to console
        Console.WriteLine("Cleaned Text:\n");
        Console.WriteLine(plainText);

        // Optional: Write to a new file
        File.WriteAllText("cleaned_output.txt", plainText);
    }

    static string RemoveHtmlAndCss(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // Remove CSS in <style> tags
        string noCss = Regex.Replace(input, "<style[^>]*>.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        // Remove HTML tags
        string noHtml = Regex.Replace(noCss, "<.*?>", "");

        // Decode HTML entities
        string decoded = WebUtility.HtmlDecode(noHtml);

        // Trim whitespace
        return decoded.Trim();
    }
}