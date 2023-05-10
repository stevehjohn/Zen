namespace Zen.System.Interfaces;

public interface IPeripheral
{
    byte? GetPortState(ushort port);
}