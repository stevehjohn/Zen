using Microsoft.Xna.Framework.Input;
using Zen.System.Interfaces;

namespace Zen.Desktop.Host.Peripherals;

public class Kempston : IPeripheral
{
    public byte? GetPortState(ushort port)
    {
        if ((port & 0xFF) is 0x1F or 0xDF)
        {
            return MapKeysToJoystick();
        }

        return null;
    }

    private static byte MapKeysToJoystick()
    {
        var keys = Microsoft.Xna.Framework.Input.Keyboard.GetState().GetPressedKeys();

        // 000FUDLR
        var data = (byte) 0;

        foreach (var key in keys)
        {
            if (key == Keys.O)
            {
                data |= 0b0000_0010;
            }

            if (key == Keys.P)
            {
                data |= 0b0000_0001;
            }

            if (key == Keys.Q)
            {
                data |= 0b0000_1000;
            }

            if (key == Keys.A)
            {
                data |= 0b0000_0100;
            }

            if (key == Keys.M)
            {
                data |= 0b0001_0000;
            }
        }

        return data;
    }
}