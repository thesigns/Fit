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
    
    public bool Exists()
    {
        return Directory.Exists(Path);
    }
    
    public bool ExistsInDir(string path)
    {
        return Directory.Exists(System.IO.Path.Combine(path, Name));
    }

    public void Log(string text)
    {
        if (string.IsNullOrEmpty(text)) return;
        var tick = DateTime.UtcNow.Ticks;
        var logContent = $"{tick} {text}";
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Adding log: '{logContent}'");
        Console.ForegroundColor = ConsoleColor.Gray;
        var fi = new FileInfo(LogPath);
        var newLine = fi.Length > 0 ? "\n" : "log.fit|1\n";
        File.AppendAllText(LogPath, newLine + logContent);
    }

    public void UndoLog()
    {
        var lines = File.ReadAllLines(LogPath);
        if (lines.Length > 0)
        {
            var lastLine = lines.Last();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Removing log: '{lastLine}'");
            Console.ForegroundColor = ConsoleColor.Gray;
            var content = string.Join('\n', lines.Take(lines.Length - 1).ToArray()).Trim();
            File.WriteAllText(LogPath, string.Empty);
            File.AppendAllText(LogPath, content);
        }
    }
}