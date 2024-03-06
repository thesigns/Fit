using System.Globalization;

namespace Fit;

public static class Units
{
    
    public static long GetTick(string timeString)
    {
        string[] formats = [
            "yyyy-M-d",
            "yyyy.M.d",
            "yyyy-MM-dd H:m",
            "yyyy.MM.dd H:m"
        ];
        var success = DateTime.TryParseExact(timeString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateValue);
        if (success)
        {
            return dateValue.Ticks;
        }
        throw new FormatException("Invalid datetime format.");
    }
    
    public static string TicksToDate(long ticks, string format = "yyyy.MM.dd HH:mm")
    {
        var dateTime = new DateTime(ticks).ToLocalTime();
        var formattedDate = dateTime.ToString(format, CultureInfo.CurrentCulture);
        return formattedDate;
    }
   
    public static int YearsSince(long ticks)
    {
        var dateFromTicks = new DateTime(ticks).ToLocalTime();
        var currentDate = DateTime.Now.ToLocalTime();
        var years = currentDate.Year - dateFromTicks.Year;
        if (currentDate.Month < dateFromTicks.Month || (currentDate.Month == dateFromTicks.Month && currentDate.Day < dateFromTicks.Day))
        {
            years--;
        }
        return years;
    }

}