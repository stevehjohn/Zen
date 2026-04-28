using Microsoft.Xna.Framework.Input;
using Zen.System.Interfaces;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace Zen.Desktop.Host.Peripherals;

public class Keyboard : IPeripheral
{
    public byte? GetPortState(ushort port)
    {
        if ((port & 0x01) == 0)
        {
            var high = (port & 0xFF00) >> 8;

            var result = 0xFF;

            if ((high & 0b0000_0001) == 0) result &= GetKeyState(0xFE);
            if ((high & 0b0000_0010) == 0) result &= GetKeyState(0xFD);
            if ((high & 0b0000_0100) == 0) result &= GetKeyState(0xFB);
            if ((high & 0b0000_1000) == 0) result &= GetKeyState(0xF7);
            if ((high & 0b0001_0000) == 0) result &= GetKeyState(0xEF);
            if ((high & 0b0010_0000) == 0) result &= GetKeyState(0xDF);
            if ((high & 0b0100_0000) == 0) result &= GetKeyState(0xBF);
            if ((high & 0b1000_0000) == 0) result &= GetKeyState(0x7F);

            return (byte) result;
        }

        return null;
    }

    private static byte GetKeyState(byte high)
    {
        var state = (byte) 0b1011_1111;

        var keys = Microsoft.Xna.Framework.Input.Keyboard.GetState().GetPressedKeys();

        foreach (var key in keys)
        {
            state &= high switch
            {
                0xFE => ScanForFEFEKeys(key),
                0xFD => ScanForFDFEKeys(key),
                0xFB => ScanForFBFEKeys(key),
                0xF7 => ScanForF7FEKeys(key),
                0xEF => ScanForEFFEKeys(key),
                0xDF => ScanForDFFEKeys(key),
                0xBF => ScanForBFFEKeys(key),
                0x7F => ScanFor7FFEKeys(key),
                _ => 0xFF
            };
        }

        return state;
    }

    private static byte ScanForFEFEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        // Caps shift.
        if (keys is Keys.LeftShift or Keys.RightShift)
        {
            data = (byte) (data & 0b1111_1110);
        }

        // Account for PC arrow keys (caps + 5, 6, 7 or 8 on the speccy).
        if (keys is Keys.Up or Keys.Down or Keys.Left or Keys.Right or Keys.Back)
        {
            data = (byte) (data & 0b1111_1110);
        }

        if (keys == Keys.Z)
        {
            data = (byte) (data & 0b1111_1101);
        }

        if (keys == Keys.X)
        {
            data = (byte) (data & 0b1111_1011);
        }

        if (keys == Keys.C)
        {
            data = (byte) (data & 0b1111_0111);
        }

        if (keys == Keys.V)
        {
            data = (byte) (data & 0b1110_1111);
        }

        return data;
    }

    private static byte ScanForFDFEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        data = keys switch
        {
            Keys.A => (byte) (data & 0b1111_1110),
            Keys.S => (byte) (data & 0b1111_1101),
            Keys.D => (byte) (data & 0b1111_1011),
            Keys.F => (byte) (data & 0b1111_0111),
            Keys.G => (byte) (data & 0b1110_1111),
            _ => data
        };

        return data;
    }

    private static byte ScanForFBFEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        data = keys switch
        {
            Keys.Q => (byte) (data & 0b1111_1110),
            Keys.W => (byte) (data & 0b1111_1101),
            Keys.E => (byte) (data & 0b1111_1011),
            Keys.R => (byte) (data & 0b1111_0111),
            Keys.T => (byte) (data & 0b1110_1111),
            _ => data
        };

        return data;
    }

    private static byte ScanForF7FEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        // Numeric 1 - 5.
        if (keys == Keys.D1)
        {
            data = (byte) (data & 0b1111_1110);
        }

        if (keys == Keys.D2)
        {
            data = (byte) (data & 0b1111_1101);
        }

        if (keys == Keys.D3)
        {
            data = (byte) (data & 0b1111_1011);
        }

        if (keys == Keys.D4)
        {
            data = (byte) (data & 0b1111_0111);
        }

        if (keys == Keys.D5)
        {
            data = (byte) (data & 0b1110_1111);
        }

        // Windows left arrow.
        if (keys == Keys.Left)
        {
            data = (byte) (data & 0b1110_1111);
        }

        return data;
    }

    private static byte ScanForEFFEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        // Numeric 0, 6 - 9.
        if (keys == Keys.D0)
        {
            data = (byte) (data & 0b1111_1110);
        }

        // Windows backspace.
        if (keys == Keys.Back)
        {
            data = (byte) (data & 0b1111_1110);
        }

        if (keys == Keys.D9)
        {
            data = (byte) (data & 0b1111_1101);
        }

        if (keys == Keys.D8)
        {
            data = (byte) (data & 0b1111_1011);
        }

        // Windows right arrow.
        if (keys == Keys.Right)
        {
            data = (byte) (data & 0b1111_1011);
        }

        if (keys == Keys.D7)
        {
            data = (byte) (data & 0b1111_0111);
        }

        // Windows up arrow.
        if (keys == Keys.Up)
        {
            data = (byte) (data & 0b1111_0111);
        }

        if (keys == Keys.D6)
        {
            data = (byte) (data & 0b1110_1111);
        }

        // Windows down arrow.
        if (keys == Keys.Down)
        {
            data = (byte) (data & 0b1110_1111);
        }

        return data;
    }

    private static byte ScanForDFFEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        data = keys switch
        {
            Keys.P => (byte) (data & 0b1111_1110),
            Keys.O => (byte) (data & 0b1111_1101),
            Keys.I => (byte) (data & 0b1111_1011),
            Keys.U => (byte) (data & 0b1111_0111),
            Keys.Y => (byte) (data & 0b1110_1111),
            _ => data
        };

        return data;
    }

    private static byte ScanForBFFEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        data = keys switch
        {
            Keys.Enter => (byte) (data & 0b1111_1110),
            Keys.L => (byte) (data & 0b1111_1101),
            Keys.K => (byte) (data & 0b1111_1011),
            Keys.J => (byte) (data & 0b1111_0111),
            Keys.H => (byte) (data & 0b1110_1111),
            _ => data
        };

        return data;
    }

    private static byte ScanFor7FFEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        data = keys switch
        {
            Keys.Space => (byte) (data & 0b1111_1110),
            // Symbol shift.
            Keys.LeftAlt => (byte) (data & 0b1111_1101),
            Keys.M => (byte) (data & 0b1111_1011),
            Keys.N => (byte) (data & 0b1111_0111),
            Keys.B => (byte) (data & 0b1110_1111),
            _ => data
        };

        return data;
    }
}