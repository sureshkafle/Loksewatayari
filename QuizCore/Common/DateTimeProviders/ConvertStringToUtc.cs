namespace QuizCore.Common.DateTimeProviders;

public static class DateTimeProviderExtensions
{
    public static DateTime? ConvertToUtc(string? dateTime)
    {
        if(dateTime==null)
        {
            return null;
        }
        DateTime dt=DateTime.Parse(dateTime);
        if (dt.Kind == DateTimeKind.Unspecified)
        {
            return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        }
        return dt.ToUniversalTime();
    }
    public static DateTime ConvertToUtc(DateTime dt)
    {
      
        if (dt.Kind == DateTimeKind.Unspecified)
        {
            return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        }
        return dt.ToUniversalTime();
    }
}
