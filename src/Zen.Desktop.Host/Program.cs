using System;
using Zen.Common.Infrastructure;

namespace Zen.Desktop.Host;

public static class Program
{
    public static void Main()
    {
        try
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            
            using var host = new Infrastructure.Host();

            host.Run();
        }
        catch (Exception exception)
        {
            Logger.LogException(nameof(Program), exception);
        }
    }
}