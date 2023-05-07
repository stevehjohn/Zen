using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace Zen.Desktop.Host.Infrastructure;

public static class KeyboardMapper
{
    private static readonly ushort[] Ports = { 0xFEFE, 0xFDFE, 0xFBFE, 0xF7FE, 0xEFFE, 0xDFFE, 0xBFFE, 0x7FFE };

    public static List<(ushort Port, byte data)> MapKeyState(Keys[] keyboardState)
    {
        var portData = new List<(ushort Port, byte data)>();

        foreach (var port in Ports)
        {
            if (port == 0xBFFE)
            {
                portData.Add((port, 0b1111_1110));

                continue;
            }

            if (keyboardState.Length == 0)
            {
                portData.Add((port, 0b11111111));

                continue;
            }

            var data = (byte) 0xFF;

            foreach (var key in keyboardState)
            {
                data &= GetPortData(port, key);
            }

            portData.Add((port, data));
        }

        return portData;
    }

    private static byte GetPortData(int port, Keys keys)
    {
        switch (port)
        {
            case 0xFEFE:
                return ScanForFEFEKeys(keys);

            case 0xFDFE:
                return ScanForFDFEKeys(keys);

            case 0xFBFE:
                return ScanForFBFEKeys(keys);

            case 0xF7FE:
                return ScanForF7FEKeys(keys);

            case 0xEFFE:
                return ScanForEFFEKeys(keys);

            case 0xDFFE:
                return ScanForDFFEKeys(keys);

            case 0xBFFE:
                return ScanForBFFEKeys(keys);

            case 0x7FFE:
                return ScanFor7FFEKeys(keys);

            default:
                // TODO: Should probably throw an exception.
                return 0b11111111;
        }
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