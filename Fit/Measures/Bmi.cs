namespace Fit.Measures;

public static class Bmi
{
    public static double GetValue(Mass weight, Length height)
    {
        return Math.Round(weight.GetValue(Mass.Unit.Kilogram) / Math.Pow(height.GetValue(Length.Unit.Metre), 2), 1);
    }

    public static string GetDescription(double bmi)
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
}