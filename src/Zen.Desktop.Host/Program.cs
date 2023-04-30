namespace Zen.Desktop.Host;

public static class Program
{
    public static void Main()
    {
        using var monitor = new Infrastructure.Host();

        monitor.Run();
    }
}