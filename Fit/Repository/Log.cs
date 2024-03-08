using Fit.Exceptions;

namespace Fit.Repository;

public class Log
{
    private string Path { get; set; }
    private int RequiredVersion { get; set; }
    private LogHeader Header { get; set; } = new();
    private List<(long tick, string content)> Lines { get; set; }
   
    public Log (string path, int requiredVersion)
    { 
        Path = path;
        RequiredVersion = requiredVersion;
        Lines = Load();
    }

    private List<(long tick, string commandLine)> Load()
    {
      List<(long tick, string commandLine)> result = [];
      var lines = File.ReadAllLines(Path);
      var headerLoaded = false;
      foreach (var line in lines)
      {
          var trimmedLine = line.Trim();
          if (IsComment(trimmedLine))
          {
              if (headerLoaded)
              {
                  continue;
              }
              Header.TryGetField(trimmedLine);
              continue;
          }
          if (!headerLoaded)
          {
              if (Header.Version < RequiredVersion)
              {
                  throw new VersionMismatchException("The log file version is too old and not supported by the current version of the application.");
              }
              if (Header.Version > RequiredVersion)
              {
                  throw new VersionMismatchException("The application version is too old to support this version of the log file.");
              }
              headerLoaded = true;              
          }
          var spaceIndex = trimmedLine.IndexOf(' ');
          if (spaceIndex <= 0) continue;
          var tickString = trimmedLine[..spaceIndex];
          var commandLine = trimmedLine[(spaceIndex + 1)..].Trim();
          if (!long.TryParse(tickString, out var tick)) continue;
          result.Add((tick, commandLine));
      }
      return result;
    }

    private static bool IsComment(string line)
    {
       return string.IsNullOrWhiteSpace(line) || line[0] == '#';
    }

    public void Write(string content)
    {
       if (string.IsNullOrWhiteSpace(content))
       {
           return;
       }
       var tick = DateTime.UtcNow.Ticks;
       var logContent = $"{tick} {content}\n";
       Lines.Add((tick, content));
       File.AppendAllText(Path, logContent);
    }
    
    public string Undo()
    {
        var contents = File.ReadAllLines(Path);
        var commented = "";
        for (var i = contents.Length - 1; i >= 0; i--)
        {
            if (string.IsNullOrWhiteSpace(contents[i]) || contents[i].Trim()[0] == '#')
            {
                continue;
            }
            commented = contents[i];
            contents[i] = "#" + contents[i];
            break;
        }
        File.WriteAllLines(Path, contents);
        Lines = Load();
        return commented;
    }
    
    public List<(long tick, string content)> GetLines(long fromTick = 0, long toTick = long.MaxValue)
    {
        return Lines.Where(line => line.tick >= fromTick && line.tick <= toTick).ToList();
    }

}