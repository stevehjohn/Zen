using Zen.System.Infrastructure;
using Zen.Z80.Processor;

namespace Zen.System.FileHandling.Models;

public class ZenFile
{
    public Model Model { get; set; }

    public State? State { get; set; }

    public Dictionary<int, int> PageConfiguration { get; set; } = new();

    public Dictionary<int, byte[]> RamBanks { get; set; } = new();

    public Dictionary<string, ushort> Registers { get; set; } = new();

    public byte[]? Rom { get; set; }
        
    public string? RomTitle { get; set; }

    public byte Last7Ffd { get; set; }

    public byte Last1Ffd { get; set; }
}