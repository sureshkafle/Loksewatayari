namespace QuizCore.Common.DateTimeProviders;

public interface IDateTime
{
    public DateTime UtcNow { get; set; }
    public DateTime Now { get; set; }
    public DateTime ConvertToUtc(DateTime dateTime);
    public DateTime GetFirstDateOfTheMonth();
    public DateTime GetLastDateOfTheMonth();
    public DateTime GetFirstDateOfTheMonth(DateTime date);

    public DateTime GetLastDateOfTheMonth(DateTime date);

    public string ToCalendarDateFormat(DateTime date);
    public string ToCalendarDateTimeFormat(DateTime date);

    public DateTime GetDaysAgo(int days, DateTime date);
    public DateTime GetDaysAgo(int days);
    public string ToFormat(DateTime date,string format);
}

