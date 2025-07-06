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

        // Remove <style>...</style> (CSS)
        input = Regex.Replace(input, "<style[^>]*>.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        // Remove <script>...</script> (JavaScript)
        input = Regex.Replace(input, "<script[^>]*>.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        // Remove any inline JavaScript-like code such as window.something or __wm.something
        input = Regex.Replace(input, @"window\.[\s\S]*?;\s*", "", RegexOptions.IgnoreCase);
        input = Regex.Replace(input, @"__wm\.[\s\S]*?;\s*", "", RegexOptions.IgnoreCase);

        // Remove archive_analytics or other inline JS variables
        input = Regex.Replace(input, @"archive_analytics[\s\S]*?;\s*", "", RegexOptions.IgnoreCase);

        // Remove all remaining HTML tags
        input = Regex.Replace(input, "<.*?>", "", RegexOptions.Singleline);

        // Decode HTML entities like &nbsp;, &lt;, etc.
        input = WebUtility.HtmlDecode(input);

        // Normalize whitespace
        input = Regex.Replace(input, @"\s{2,}", " ").Trim();

        return input;

    }
}