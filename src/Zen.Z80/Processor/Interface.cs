// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

using Zen.Z80.Interfaces;

namespace Zen.Z80.Processor;

public class Interface
{
    private readonly IPortConnector _portConnector;

    public Interface(IPortConnector portConnector)
    {
        _portConnector = portConnector;
    }

    public bool INT { get; set; }

    public Func<ushort, byte>? ReadRam { get; set; }

    public Action<ushort, byte>? WriteRam { get; set; }

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
        return _portConnector.CpuRead(port);
    }

    public void WriteToPort(ushort port, byte data)
    {
        _portConnector.CpuWrite(port, data);
    }
}