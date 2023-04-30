using System.Globalization;

namespace Zen.Z80.Tests.Fuse.Models;

public class TestResultStep
{
    public int Time { get; }

    public EventType EventType { get; }

    public int Address { get; }

    public byte? Data { get; }

    public TestResultStep(string data)
    {
        var parts = data.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        Time = int.Parse(parts[0]);

        EventType = parts[1] switch
        {
            "MR" => EventType.MemoryRead,
            "MW" => EventType.MemoryWrite,
            "MC" => EventType.MemoryContend,
            "PR" => EventType.PortRead,
            "PW" => EventType.PortWrite,
            "PC" => EventType.PortContend,
            // TODO: Proper exception?
            _ => throw new Exception(parts[1])
        };
        

        Address = int.Parse(parts[2], NumberStyles.HexNumber);

        if (parts.Length > 3)
        {
            Data = byte.Parse(parts[3], NumberStyles.HexNumber);
        }
    }
}