using System.Globalization;

namespace Fit;

public static class Units
{
    
    public static double GetBmi(double weightKg, double heightM)
    {
        return Math.Round(weightKg / (heightM * heightM), 1);
    }

    public static string GetBmiDescription(double bmi)
    {
        return bmi switch
        {
            < 16 => "severe underweight",
            < 17 => "moderate underweight",
            < 18.5 => "mild underweight",
            < 25 => "normal weight",
            < 30 => "overweight",
            < 35 => "obese class 1",
            < 40 => "obese class 2",
            _ => "obese class 3"
        };
    }

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