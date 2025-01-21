namespace QuizCore.Common.DateTimeProviders
{
    public static class DateTimeExtensions
    {
    
        public static DateTime AddHoursAndMinutes(this DateTime  dateTime ,int hours,  int minutes)
        {
            return dateTime.AddHours(hours).AddMinutes(minutes);
        }
        
        public static DateTime ConvertToTimeZoneFromUtc(this DateTime utcDateTime,string timezoneId)
        {
            //timezoneId ="Asia/Kathmandu";            
            DateTime temp = new DateTime(utcDateTime.Ticks, DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeFromUtc(temp, TimeZoneInfo.FindSystemTimeZoneById(timezoneId));

        }
         public static DateTime ConvertFromTimeZoneToUtc(this DateTime localDateTime, string timeZone)
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            var utc = TimeZoneInfo.ConvertTimeToUtc(localDateTime, tz);
            return utc;
        } 
        public static DateTime ConvertToNepalStandardTime(this DateTime utcDateTime)
        {
            return ConvertToTimeZoneFromUtc(utcDateTime,"Asia/Kathmandu");
        }
        public static DateTime ConvertFromNepalStandardTimeToUtc(this DateTime localDateTime)
        {
            return ConvertFromTimeZoneToUtc(localDateTime,"Asia/Kathmandu");
        }

        public static string ToMysqlDateFormat(this DateTime dateTime)
        {
            return dateTime.Date.ToString("yyyy-MM-dd");
        }
    }
}