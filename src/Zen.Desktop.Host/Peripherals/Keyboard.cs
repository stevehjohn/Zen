using Microsoft.Xna.Framework.Input;
using Zen.System.Interfaces;

// ReSharper disable IdentifierTypo

namespace Zen.Desktop.Host.Peripherals;

public class Keyboard : IPeripheral
{
    public byte? GetPortState(ushort port)
    {
        if ((port & 0x01) == 0)
        {
            var high = (port & 0xFF00) >> 8;

            return high switch
            {
                _ when (high & 0b0000_0001) == 0 => GetKeyState(0xFE),
                _ when (high & 0b0000_0010) == 0 => GetKeyState(0xFD),
                _ when (high & 0b0000_0100) == 0 => GetKeyState(0xFB),
                _ when (high & 0b0000_1000) == 0 => GetKeyState(0xF7),
                _ when (high & 0b0001_0000) == 0 => GetKeyState(0xEF),
                _ when (high & 0b0010_0000) == 0 => GetKeyState(0xDF),
                _ when (high & 0b0100_0000) == 0 => GetKeyState(0xBF),
                _ when (high & 0b1000_0000) == 0 => GetKeyState(0x7F),
                _ => null
            };
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
        if (keys == Keys.LeftShift || keys == Keys.RightShift)
        {
            data = (byte) (data & 0b1111_1110);
        }

        // Account for PC arrow keys (caps + 5, 6, 7 or 8 on the speccy).
        if (keys == Keys.Up || keys == Keys.Down || keys == Keys.Left || keys == Keys.Right || keys == Keys.Back)
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

        if (keys == Keys.A)
        {
            data = (byte) (data & 0b1111_1110);
        }

        if (keys == Keys.S)
        {
            data = (byte) (data & 0b1111_1101);
        }

        if (keys == Keys.D)
        {
            data = (byte) (data & 0b1111_1011);
        }

        if (keys == Keys.F)
        {
            data = (byte) (data & 0b1111_0111);
        }

        if (keys == Keys.G)
        {
            data = (byte) (data & 0b1110_1111);
        }

        return data;
    }

    private static byte ScanForFBFEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        if (keys == Keys.Q)
        {
            data = (byte) (data & 0b1111_1110);
        }

        if (keys == Keys.W)
        {
            data = (byte) (data & 0b1111_1101);
        }

        if (keys == Keys.E)
        {
            data = (byte) (data & 0b1111_1011);
        }

        if (keys == Keys.R)
        {
            data = (byte) (data & 0b1111_0111);
        }

        if (keys == Keys.T)
        {
            data = (byte) (data & 0b1110_1111);
        }

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

        if (keys == Keys.P)
        {
            data = (byte) (data & 0b1111_1110);
        }

        if (keys == Keys.O)
        {
            data = (byte) (data & 0b1111_1101);
        }

        if (keys == Keys.I)
        {
            data = (byte) (data & 0b1111_1011);
        }

        if (keys == Keys.U)
        {
            data = (byte) (data & 0b1111_0111);
        }

        if (keys == Keys.Y)
        {
            data = (byte) (data & 0b1110_1111);
        }

        return data;
    }

    private static byte ScanForBFFEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        if (keys == Keys.Enter)
        {
            data = (byte) (data & 0b1111_1110);
        }

        if (keys == Keys.L)
        {
            data = (byte) (data & 0b1111_1101);
        }

        if (keys == Keys.K)
        {
            data = (byte) (data & 0b1111_1011);
        }

        if (keys == Keys.J)
        {
            data = (byte) (data & 0b1111_0111);
        }

        if (keys == Keys.H)
        {
            data = (byte) (data & 0b1110_1111);
        }

        return data;
    }

    private static byte ScanFor7FFEKeys(Keys keys)
    {
        var data = (byte) 0b1111_1111;

        if (keys == Keys.Space)
        {
            data = (byte) (data & 0b1111_1110);
        }

        // Symbol shift.
        if (keys == Keys.LeftAlt)
        {
            data = (byte) (data & 0b1111_1101);
        }

        if (keys == Keys.M)
        {
            data = (byte) (data & 0b1111_1011);
        }

        if (keys == Keys.N)
        {
            data = (byte) (data & 0b1111_0111);
        }

        if (keys == Keys.B)
        {
            data = (byte) (data & 0b1110_1111);
        }

        return data;
    }
}
