namespace Fit.Commands;

public class Init : Command
{
    public new static string Manual => """
                                       Usage:
                                           fit init <date-of-birth> <sex> <height> <weight>
                                       Description:
                                           Initializes the Fit repository in the current directory.
                                           The date of birth should be in the YYYY-M-D format (leading zeroes are optional).
                                           Sex should be "male" or "m" or "female" or "f".
                                           Height and weight should be in metric system units, for example "cm" for centimetres.
                                           Imperial system is not supported in the moment.
                                       Example:
                                           fit init 1981.5.27 male 175cm 109kg
                                       """;
    
    public override string Execute(List<string> args, Repo repo)
    {
        if (repo.ExistsInDir(Environment.CurrentDirectory))
        {
            Console.WriteLine($"There is a Fit repository ('{repo.Name}' subdirectory) in this directory already.");
            return "";
        }
        
        if (args.Count < 4)
        {
            Command.Execute("help init", repo);
            return "";
        }

        try
        {
            var birthTick = Units.GetTick(args[0]);
            var sex = Units.GetSex(args[1]);
            var height = Units.GetMeasurement<Units.Length>(args[2]);
            var weight = Units.GetMeasurement<Units.Mass>(args[3]);
            var bmi = Units.GetBmi(weight, height);
            var bmiDescription = Units.GetBmiDescription(bmi);

            Console.Write("Initializing Fit repository for a ");
            Console.WriteLine($"{Units.YearsSince(birthTick)} years old {sex.ToString().ToLower()}.");
            Console.WriteLine($"Height: {height * 100} cm");
            Console.WriteLine($"Weight: {weight} kg");
            Console.WriteLine($"BMI: {bmi} ({bmiDescription})");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Command.Execute("help init", repo);
            return "";
        }
        try
        {
            repo.Create(Environment.CurrentDirectory);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.ToString());
            return "";
        }
        Console.WriteLine("Fit repository initialized.");
        return ToCommandLine(args);
    }
    
    public override void ApplyToFit(long tick, string command, List<string> args, Fit fit)
    {
        if (args.Count < 4)
        {
            throw new ArgumentException("Too few args.");
        }
        try
        {
            var birthTick = Units.GetTick(args[0]);
            var sex = Units.GetSex(args[1]);
            var height = Units.GetMeasurement<Units.Length>(args[2]);
            var weight = Units.GetMeasurement<Units.Mass>(args[3]);
            fit.Birth = birthTick;
            fit.Sex = sex;
            fit.Heights.Add((tick, height));
            fit.Weights.Add((tick, weight));
        }
        catch
        {
            throw new FormatException("Invalid measurement.");
        }
    }
}
