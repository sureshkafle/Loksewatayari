namespace QuizCore.Common.DateTimeProviders;

public class DateTimeService : IDateTime
{
    public DateTime UtcNow { get; set; } = DateTime.UtcNow;
    public DateTime Now { get; set; } = DateTime.Now;

    public DateTime GetDaysAgo( int days, DateTime date)
    {
        return date.AddDays(-days);
    }

    public DateTime GetDaysAgo(int days)
    {
        return GetDaysAgo(days, UtcNow); 
    }

    public DateTime GetFirstDateOfTheMonth()
    {
        return GetFirstDateOfTheMonth(UtcNow);
    }

    public DateTime GetFirstDateOfTheMonth(DateTime date)
    {
        return new DateTime(date.Year, date.Month, 1);
    }
    public DateTime GetLastDateOfTheMonth()
    {
        return GetFirstDateOfTheMonth(UtcNow);
    }

   
    public DateTime GetLastDateOfTheMonth(DateTime date)
    {
        var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
        return new DateTime(date.Year, date.Month, daysInMonth);
    }

    public string ToCalendarDateFormat(DateTime date)
    {

        return ToFormat(date, "yyyy-MM-dd");
    }

    public string ToCalendarDateTimeFormat(DateTime date)
    {
        return ToFormat(date, "yyyy-MM-dd HH:mm:ss");
    }

    public string ToFormat(DateTime date,string format)
    {
        return date.ToString(format);
    }
    public  DateTime ConvertToUtc(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Utc)
        {
            return dateTime;
        }
        else if (dateTime.Kind == DateTimeKind.Local)
        {
            return dateTime.ToUniversalTime();
        }
        else
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToUniversalTime();
        }
    }
    
}
