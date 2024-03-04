namespace Fit;

public class Repo(string startDirectory, string name)
{
    public string Name { get; } = name;
    public string Path { get; private set; } = FindDirectory(startDirectory, name);
    private string LogPath => System.IO.Path.Combine(Path, "log.fit");

    private static string FindDirectory(string start, string dirName)
    {
        var dirInfo = new DirectoryInfo(start);
        var firstPath = System.IO.Path.Combine(dirInfo.FullName, dirName);
        while (true)
        {
            var currentPath = System.IO.Path.Combine(dirInfo.FullName, dirName);
            if (System.IO.Path.Exists(currentPath))
            {
                return currentPath;
            }
            dirInfo = dirInfo.Parent;
            if (dirInfo == null) return firstPath;
        }
    }
    
    public void Create(string path)
    {
        var repoPath = System.IO.Path.Combine(path, Name);
        Directory.CreateDirectory(repoPath);
        Path = repoPath;
        using (File.Create(LogPath)) {}
    }
    
    public bool Exists() => Directory.Exists(Path);

    public bool ExistsInDir(string path) => Directory.Exists(System.IO.Path.Combine(path, Name));

    public void Log(string text)
    {
        if (string.IsNullOrEmpty(text)) return;
        var tick = DateTime.UtcNow.Ticks;
        var logContent = $"{tick} {text}";
        var fi = new FileInfo(LogPath);
        var newLine = fi.Length > 0 ? "\n" : "log.fit|1\n";
        File.AppendAllText(LogPath, newLine + logContent);
    }

    public string UndoLog()
    {
        var lines = File.ReadAllLines(LogPath);
        if (lines.Length > 0)
        {
            var lastLine = lines.Last();
            var content = string.Join('\n', lines.Take(lines.Length - 1).ToArray()).Trim();
            File.WriteAllText(LogPath, string.Empty);
            File.AppendAllText(LogPath, content);
            return lastLine;
        }
        return "";
    }

    public List<string> GetLog(long fromTick = 0, long untilTick = -1)
    {
        if (untilTick == -1)
        {
            untilTick = DateTime.UtcNow.Ticks;
        }
        var logLines = new List<string>();
        foreach (var line in File.ReadLines(LogPath))
        {
            var spaceIndex = line.IndexOf(' ');
            if (spaceIndex <= 0) continue;
            var tickPart = line[..spaceIndex];
            if (!long.TryParse(tickPart, out var tick) || tick < fromTick || tick > untilTick) continue;
            logLines.Add(line);
        }
        return logLines;
    }
    
    public static (long tick, string commandLine) SplitLogLine(string logLine)
    {
        var spaceIndex = logLine.IndexOf(' ');
        if (spaceIndex < 0) throw new FormatException("Invalid log line format.");
        var tickPart = logLine[..spaceIndex];
        var commandLine = logLine[(spaceIndex + 1)..];
        if (!long.TryParse(tickPart, out var tick)) throw new FormatException("Invalid log line format.");
        return (tick, commandLine);
    }
    
}