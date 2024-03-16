using Fit.Charts;
using Fit.Exceptions;

namespace Fit.Repository;

public class Repo(string startDirectory)
{
    public const int Version = 5;
    public const string Name = ".fit";
    
    public string CurrentPath { get; private set; } = FindDirectory(startDirectory, Name);
    public string FitLogPath => Path.Combine(CurrentPath, "log.fit");
    public string FitChartStylesPath => Path.Combine(CurrentPath, "charts.css");
    public string FitChartsPath => Path.Combine(CurrentPath, "charts");
    public bool Exists => Directory.Exists(CurrentPath);

    private static string FindDirectory(string start, string dirName)
    {
        var dirInfo = new DirectoryInfo(start);
        var firstPath = Path.Combine(dirInfo.FullName, dirName);
        while (true)
        {
            var currentPath = Path.Combine(dirInfo.FullName, dirName);
            if (Path.Exists(currentPath))
            {
                return currentPath;
            }
            dirInfo = dirInfo.Parent;
            if (dirInfo == null) return firstPath;
        }
    }
    
    public void Create(string locationPath = "")
    {
        if (string.IsNullOrWhiteSpace(locationPath))
        {
            locationPath = Environment.CurrentDirectory;
        }
        var path = Path.Combine(locationPath, Name);
        if (Directory.Exists(path))
        {
            throw new DirectoryAlreadyExistsException($"Cannot create Fit repo. The directory '{path}' already exists.");
        }
        Directory.CreateDirectory(path);
        CurrentPath = path;
        using (var streamWriter = new StreamWriter(FitLogPath, false))
        {
            streamWriter.WriteLine($"#Header Version: {Version}");
        }
        using (var streamWriter = new StreamWriter(FitChartStylesPath, false))
        {
            streamWriter.Write(Chart.DefaultStyles);
        }
        Directory.CreateDirectory(FitChartsPath);
    }

}