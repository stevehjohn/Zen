using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Color = Microsoft.Xna.Framework.Color;
using Constants = Zen.Common.Constants;

namespace Zen.Desktop.Host.Display;

public class VRamAdapter
{
    private readonly byte[] _screenFrame;

    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Stopwatch _stopwatch;

    private bool _alternate;

    private Texture2D _display;

    public Texture2D Display => _display;

    public VRamAdapter(byte[] screenFrame, GraphicsDeviceManager graphicsDeviceManager)
    {
        _screenFrame = screenFrame;

        _graphicsDeviceManager = graphicsDeviceManager;

        _stopwatch = Stopwatch.StartNew();
    }

    public void RenderDisplay()
    {
        var texture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.ScreenWidthPixels, Constants.ScreenHeightPixels);
       
        var data = new Color[Constants.ScreenWidthPixels * Constants.ScreenHeightPixels];

        for (var p = 0; p < 0xC000; p++)
        {
            data[p] = _screenFrame[p] == 0xFF ? Color.White : Color.Black;
        }

        _alternate = _stopwatch.ElapsedMilliseconds > 500;

        //for (var y = 0; y < Constants.ScreenHeightPixels; y++)
        //{
        //    for (var x = 0; x < Constants.ScreenWidthBytes; x++)
        //    {
        //        var address = 0;

        //        address |= (y & 0b0000_0111) << 8;

        //        address |= (y & 0b1100_0000) << 5;

        //        address |= (y & 0b0011_1000) << 2;

        //        address |= x;

        //        var segment = screenRam[address];

        //        var colours = GetColours(x, y, screenRam);

        //        for (var b = 7; b >= 0; b--)
        //        {
        //            if ((segment & (0x01 << b)) > 0)
        //            {
        //                data[i] = colours.Foreground;
        //            }
        //            else
        //            {
        //                data[i] = colours.Background;
        //            }

        //            i++;
        //        }
        //    }
        //}

        texture.SetData(data);

        if (_stopwatch.ElapsedMilliseconds > 1_000)
        {
            _stopwatch.Restart();
        }

        _display = texture;
    }

    private (Color Foreground, Color Background) GetColours(int x, int y, byte[] screenRam)
    {
        var colourAddress = 0x1800;

        var offset = x + y / 8 * 32;

        colourAddress += offset;

        var data = screenRam[colourAddress];

        Color background;

        if ((data & 0b0100_0000) > 0)
        {
            background = ((data & 0b00111000) >> 3) switch
            {
                0 => Color.Black,
                1 => Color.Blue,
                2 => Color.Red,
                3 => Color.Magenta,
                4 => Color.FromNonPremultiplied(0, 200, 0, 255),
                5 => Color.Cyan,
                6 => Color.Yellow,
                7 => Color.White,
                _ => Color.Black
            };
        }
        else
        {
            background = ((data & 0b00111000) >> 3) switch
            {
                0 => Color.Black,
                1 => Color.DarkBlue,
                2 => Color.FromNonPremultiplied(192, 0, 0, 255),
                3 => Color.DarkMagenta,
                4 => Color.Green,
                5 => Color.DarkCyan,
                6 => Color.FromNonPremultiplied(204, 204, 0, 255),
                7 => Color.LightGray,
                _ => Color.Black
            };
        }

        Color foreground;

        if ((data & 0b0100_0000) > 0)
        {
            foreground = (data & 0b00000111) switch
            {
                0 => Color.Black,
                1 => Color.Blue,
                2 => Color.Red,
                3 => Color.Magenta,
                4 => Color.FromNonPremultiplied(0, 200, 0, 255),
                5 => Color.Cyan,
                6 => Color.Yellow,
                7 => Color.White,
                _ => Color.Black
            };
        }
        else
        {
            foreground = (data & 0b00000111) switch
            {
                0 => Color.Black,
                1 => Color.DarkBlue,
                2 => Color.FromNonPremultiplied(192, 0, 0, 255),
                3 => Color.DarkMagenta,
                4 => Color.Green,
                5 => Color.DarkCyan,
                6 => Color.FromNonPremultiplied(204, 204, 0, 255),
                7 => Color.LightGray,
                _ => Color.Black
            };
        }

        if ((data & 0x80) > 0 && _alternate)
        {
            return (background, foreground);
        }

        return (foreground, background);
    }
}