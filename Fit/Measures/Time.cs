﻿using System.Globalization;

namespace Fit.Measures;

public class Time
{
    public const long Second = 10_000_000;
    public const long Minute = Second * 60;
    public const long Hour = Minute * 60;
    public const long Day = Hour * 24;
    public const long Week = Day * 7;
    
    public long Value;

    public Time(long tick)
    {
        Value = tick;
    }
    
    public Time(string notation)
    {
        Value = Parse(notation);
    }

    public static long Now => DateTime.Now.ToLocalTime().Ticks;
    
    private static long Parse(string timeNotation)
    {
        string[] formats = [
            "yyyy-M-d",
            "yyyy.M.d",
            "yyyy-MM-dd H:m",
            "yyyy.MM.dd H:m"
        ];
        var success = DateTime.TryParseExact(timeNotation, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateValue);
        if (success)
        {
            return dateValue.Ticks;
        }
        throw new FormatException("Invalid datetime format.");
    }
    
    public string ToString(string format)
    {
        var dateTime = new DateTime(Value).ToLocalTime();
        var formattedDate = dateTime.ToString(format, CultureInfo.CurrentCulture);
        return formattedDate;
    }

    public override string ToString()
    {
        var dateTime = new DateTime(Value).ToLocalTime();
        var formattedDate = dateTime.ToString("yyyy.MM.dd HH:mm", CultureInfo.CurrentCulture);
        return formattedDate;
    }

    public int YearsElapsed()
    {
        var dateFromTicks = new DateTime(Value).ToLocalTime();
        var currentDate = DateTime.Now.ToLocalTime();
        var years = currentDate.Year - dateFromTicks.Year;
        if (currentDate.Month < dateFromTicks.Month || (currentDate.Month == dateFromTicks.Month && currentDate.Day < dateFromTicks.Day))
        {
            years--;
        }
        return years;
    }
}