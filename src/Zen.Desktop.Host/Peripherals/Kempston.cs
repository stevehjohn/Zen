using Zen.System.Interfaces;

namespace Zen.Desktop.Host.Peripherals;

public class Kempston : IPeripheral
{
    public byte? GetPortState(ushort port)
    {
        if ((port & 0xFF) is 0x1F or 0xDF)
        {
            return 0x00;
        }

        return 0xFF;
    }
}