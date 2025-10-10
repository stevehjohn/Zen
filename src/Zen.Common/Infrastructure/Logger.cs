namespace Zen.Common.Infrastructure;

public static class Logger
{
    private static readonly string LogFile;

    static Logger()
    {
        LogFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "zen.log");
    }
    
    public static void LogException(string className, Exception exception)
    {
        File.AppendAllLines(LogFile, new[]
        {
            "--------------------------------------------------------------------------------",
            $"Time: {DateTime.UtcNow:u}",
            $"Component: {className}",
            exception.ToString()
        });
    }
}