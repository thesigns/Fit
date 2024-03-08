namespace Fit.Repository;

public class LogHeader
{
    public int Version;

    public void TryGetField(string line)
    {
        if (!IsHeaderLine(line))
        {
            return;
        }
        var spaceIndex = line.IndexOf(' ');
        if (spaceIndex < 0)
        {
            return;
        }
        var trimmedLine = line[spaceIndex..];
        line = line.Remove(0, "#Header ".Length);
        var colonIndex = trimmedLine.IndexOf(':');
        if (colonIndex < 0)
        {
            return;
        }
        var fieldName = line[..(colonIndex - 1)].Trim();
        var fieldValue = line[colonIndex..].Trim();
        
        switch (fieldName)
        {
            case "Version":
                Version = int.Parse(fieldValue);
                break;
        }
    }

    private static bool IsHeaderLine(string line)
    {
        return line.StartsWith("#Header ") && line.Contains(':');
    }
    
}