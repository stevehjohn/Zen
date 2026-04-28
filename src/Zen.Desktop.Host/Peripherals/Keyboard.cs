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

        data = keys switch
        {
            // Caps shift.
            // Account for PC arrow keys (caps + 5, 6, 7 or 8 on the speccy).
            Keys.LeftShift or Keys.RightShift or Keys.Up or Keys.Down or Keys.Left or Keys.Right or Keys.Back => (byte) (data & 0b1111_1110),
            Keys.Z => (byte) (data & 0b1111_1101),
            Keys.X => (byte) (data & 0b1111_1011),
            Keys.C => (byte) (data & 0b1111_0111),
            Keys.V => (byte) (data & 0b1110_1111),
            _ => data
        };

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

        data = keys switch
        {
            // Numeric 1 - 5.
            Keys.D1 => (byte) (data & 0b1111_1110),
            Keys.D2 => (byte) (data & 0b1111_1101),
            Keys.D3 => (byte) (data & 0b1111_1011),
            Keys.D4 => (byte) (data & 0b1111_0111),
            // Windows left arrow.
            Keys.D5 or Keys.Left => (byte) (data & 0b1110_1111),
            _ => data
        };

        return data;
    }

    private static byte ScanForEFFEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        data = keys switch
        {
            // Numeric 0, 6 - 9.
            // Windows backspace.
            Keys.D0 or Keys.Back => (byte) (data & 0b1111_1110),
            Keys.D9 => (byte) (data & 0b1111_1101),
            // Windows right arrow.
            Keys.D8 or Keys.Right => (byte) (data & 0b1111_1011),
            // Windows up arrow.
            Keys.D7 or Keys.Up => (byte) (data & 0b1111_0111),
            // Windows down arrow.
            Keys.D6 or Keys.Down => (byte) (data & 0b1110_1111),
            _ => data
        };

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