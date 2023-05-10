namespace Zen.Z80.Interfaces;

public interface IRamConnector
{
    byte ReadRam(ushort address);

    void WriteRam(ushort address, byte data);
}