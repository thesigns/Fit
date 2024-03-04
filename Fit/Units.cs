using System.Globalization;
using System.Text.RegularExpressions;

namespace Fit;

public static partial class Units
{
   
    [GeneratedRegex(@"^(\d*[.,]?\d*)([a-zA-Z]+)$")]
    private static partial Regex SplitMeasurementRegex();
    
    public enum Sex
    {
        Male,
        Female,
    }
    
    public enum Length
    {
        Mm = 1000,
        Cm = 100,
        M = 1,
    }

    public enum Mass
    {
        G = 1000,
        Dag = 100,
        Kg = 1,
    }
    
    public static Sex GetSex(string sexString)
    {
        return sexString.ToLower() switch
        {
            "male" => Sex.Male,
            "m" => Sex.Male,
            "female" => Sex.Female,
            "f" => Sex.Female,
            _ => throw new FormatException("Invalid sex string format.")
        };
    }
    
    public static double GetMeasurement<TUnitsEnum>(string str) where TUnitsEnum : struct, Enum
    {
        var match = SplitMeasurementRegex().Match(str);
        if (!match.Success)
        {
            throw new FormatException($"Invalid {typeof(TUnitsEnum).Name} measurement format.");
        }
        var numericPart = match.Groups[1].Value.Replace(',', '.');
        var unitPart = match.Groups[2].Value;
        var number = double.Parse(numericPart, CultureInfo.InvariantCulture);
        if (Enum.TryParse<TUnitsEnum>(unitPart, true, out var lengthUnit))
        {
            var unitValue = Convert.ToInt32(lengthUnit);
            return Math.Round(number / unitValue, 3);
        }
        throw new FormatException($"Invalid {typeof(TUnitsEnum).Name} measurement format.");
    }
   
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