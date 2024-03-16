using Fit.Charts;
using Fit.Measures;
using Fit.Repository;

namespace Fit.Commands;

public class Plot : Command
{
    public new static string Manual => """
                                       Usage:
                                           fit plot <chart-type>
                                       Description:
                                           Plots a chart/infographic in SVG format.
                                           The chart/infographic is saved inside .fit repository, in charts directory.
                                           Available chart types: weight.
                                       Example:
                                           fit plot weight
                                       """;
    
    
    public override string Execute(List<string> args, Repo repo)
    {
        if (!repo.Exists)
        {
            Console.WriteLine("Fit repository doesn't exist. Use init command.");
            Command.Execute("help init", repo);
            return "";
        }
        
        if ((args.Count is < 1 or > 1) || args[0] != "weight")
        {
            Command.Execute("help plot", repo);
            return "";
        }
        
        Console.WriteLine("Plotting a weight chart...");
        
        var fit = new Fit(repo);
        var chart = new WeightChart(fit);
        chart.Save(Path.Combine(repo.FitChartsPath, $"chart_{Time.Now}.svg"));

        return "";
    }

    public override void ApplyToFit(long tick, string command, List<string> args, Fit fit)
    {
    }
}