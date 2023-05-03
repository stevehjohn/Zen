// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace Zen.Z80.Processor;

public class Interface
{
    public bool INT { get; set; }

    public Func<ushort, byte>? ReadRam { get; set; }

    public Action<ushort, byte>? WriteRam { get; set; }

    public Func<ushort, byte>? ReadPort { get; set; }

    public Action<ushort, byte>? WritePort { get; set; }

    public byte ReadFromMemory(ushort address)
    {
        return ReadRam!(address);
    }

    public void WriteToMemory(ushort address, byte data)
    {
        WriteRam!(address, data);
    }

    public byte ReadFromPort(ushort port)
    {
        return ReadPort!(port);
    }

    public void WriteToPort(ushort port, byte data)
    {
        WritePort!(port, data);
    }
}