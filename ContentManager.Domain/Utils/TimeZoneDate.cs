namespace ContentManager.Domain.Utils
{
    public static class TimeZoneDate
    {
        const string TIMEZONE_ID = "E. South America Standard Time";
        public static DateTime Now { get { return Moment(); } }
        private static DateTime Moment()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(TIMEZONE_ID));
        }
    }
}
