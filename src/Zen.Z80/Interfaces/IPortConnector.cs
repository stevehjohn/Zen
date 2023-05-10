namespace Zen.Z80.Interfaces;

public interface IPortConnector
{
    byte CpuRead(ushort port);

    void CpuWrite(ushort port, byte data);
}