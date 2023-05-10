using Zen.System.Interfaces;

namespace Zen.Desktop.Host.Peripherals;

public class DiskDrive : IPeripheral
{
    public byte? GetPortState(ushort port)
    {
        if (port == 0x2FFD)
        {
            return 0b10000000;
        }

        return 0xFF;
    }
}