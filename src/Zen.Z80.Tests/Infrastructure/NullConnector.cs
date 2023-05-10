using Zen.Z80.Interfaces;

namespace Zen.Z80.Tests.Infrastructure;

public class NullConnector : IPortConnector
{
    public byte CpuRead(ushort port)
    {
        return 0xFF;
    }

    public void CpuWrite(ushort port, byte data)
    {
    }
}