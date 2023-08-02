namespace Zen.Common.Infrastructure;

public static class Logger
{
    public static void LogException(string className, Exception exception)
    {
        File.AppendAllLines(Constants.ExceptionsLogFileName, new[]
                                                             {
                                                                 "--------------------------------------------------------------------------------",
                                                                 $"Time: {DateTime.UtcNow:u}",
                                                                 $"Component: {className}",
                                                                 exception.ToString()
                                                             });
    }
}