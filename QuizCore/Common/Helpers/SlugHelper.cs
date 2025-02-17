﻿using System.Text;
using System.Text.RegularExpressions;

namespace QuizCore.Common.Helpers;
public static class SlugHelper
{
    public static string Create(bool toLower, params string[] values)
    {
        var val = Create(toLower, String.Join("-", values));
        
            return val;
    }

    /// <summary>
    /// Creates a slug.
    /// References:
    /// http://www.unicode.org/reports/tr15/tr15-34.html
    /// https://meta.stackexchange.com/questions/7435/non-us-ascii-characters-dropped-from-full-profile-url/7696#7696
    /// https://stackoverflow.com/questions/25259/how-do-you-include-a-webpage-title-as-part-of-a-webpage-url/25486#25486
    /// https://stackoverflow.com/questions/3769457/how-can-i-remove-accents-on-a-string
    /// </summary>
    /// <param name="toLower"></param>
    /// <param name="normalised"></param>
    /// <returns></returns>
    public static string Create(bool toLower, string value)
    {
        if (value == null)
            return "";

        var normalised = value.Normalize(NormalizationForm.FormKD);

        const int maxlen = 80;
        int len = normalised.Length;
        bool prevDash = false;
        var sb = new StringBuilder(len);
        char c;

        for (int i = 0; i < len; i++)
        {
            c = normalised[i];
            if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
            {
                if (prevDash)
                {
                    sb.Append('-');
                    prevDash = false;
                }
                sb.Append(c);
            }
            else if (c >= 'A' && c <= 'Z')
            {
                if (prevDash)
                {
                    sb.Append('-');
                    prevDash = false;
                }
                // Tricky way to convert to lowercase
                if (toLower)
                    sb.Append((char)(c | 32));
                else
                    sb.Append(c);
            }
            else if (c == ' ' || c == ',' || c == '.' || c == '/' || c == '\\' || c == '-' || c == '_' || c == '=')
            {
                if (!prevDash && sb.Length > 0)
                {
                    prevDash = true;
                }
            }
            else
            {
                string swap = ConvertEdgeCases(c, toLower);

                if (swap != null)
                {
                    if (prevDash)
                    {
                        sb.Append('-');
                        prevDash = false;
                    }
                    sb.Append(swap);
                }
            }

            if (sb.Length == maxlen)
                break;
        }
        var val = sb.ToString();
        val = Regex.Replace(val,"-{2,}","-");
        return val;
    }

    static string ConvertEdgeCases(char c, bool toLower)
    {
        string swap = string.Empty;
        switch (c)
        {
            case 'ı':
                swap = "i";
                break;
            case 'ł':
                swap = "l";
                break;
            case 'Ł':
                swap = toLower ? "l" : "L";
                break;
            case 'đ':
                swap = "d";
                break;
            case 'ß':
                swap = "ss";
                break;
            case 'ø':
                swap = "o";
                break;
            case 'Þ':
                swap = "th";
                break;
                
        }
        return swap;
    }
}
