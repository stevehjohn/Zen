namespace Zen.Z80.Interfaces;

public interface IPortConnector
{
    byte CpuPortRead(ushort port);

    void CpuPortWrite(ushort port, byte data);
}