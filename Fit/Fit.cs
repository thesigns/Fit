using Fit.Attributes;
using Fit.Commands;
using Fit.Measures;
using Fit.Repository;

namespace Fit;

public class Fit
{
    public Time Birth { get; set; }
    public Sex Sex { get; set; }
    public List<(long tick, Length height)> Heights { get; set; } = [];
    public List<(long tick, Mass weight)> Weights { get; set; } = [];
    public List<(long tick, Mood mood)> Moods { get; set; } = [];

    public Fit(Repo repo)
    {
        var log = new Log(repo.FitLogPath, Repo.Version);
        var lines = log.GetLines();
        foreach (var line in lines)
        {
           Command.ApplyToFit(line.tick, line.content, this);
        }
    }
}
