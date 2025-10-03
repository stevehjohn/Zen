// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

using Zen.Z80.Interfaces;

namespace Zen.Z80.Processor;

public class Interface
{
    private readonly IPortConnector _portConnector;

    private readonly IRamConnector _ramConnector;

    public Interface(IPortConnector portConnector, IRamConnector ramConnector)
    {
        _portConnector = portConnector;

        _ramConnector = ramConnector;
    }

    public bool Int { get; set; }

    public byte ReadFromMemory(ushort address)
    {
        return _ramConnector.ReadRam(address);
    }

    public void WriteToMemory(ushort address, byte data)
    {
        _ramConnector.WriteRam(address, data);
    }

    public byte ReadFromPort(ushort port)
    {
        return _portConnector.CpuPortRead(port);
    }

    public void WriteToPort(ushort port, byte data)
    {
        _portConnector.CpuPortWrite(port, data);
    }
}