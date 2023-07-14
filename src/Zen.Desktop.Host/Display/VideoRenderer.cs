using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Constants = Zen.Common.Constants;

namespace Zen.Desktop.Host.Display;

public class VideoRenderer
{
    private const int FlashFrames = 20;

    private readonly ushort[] _screenFrame;

    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private Texture2D _display;

    private ulong _frameCount;

    private bool _flash;

    private readonly Color[] _data = new Color[Constants.ScreenWidthPixels * Constants.ScreenHeightPixels];

    public Texture2D Display => _display;

    public VideoRenderer(ushort[] screenFrame, GraphicsDeviceManager graphicsDeviceManager)
    {
        _screenFrame = screenFrame;

        _graphicsDeviceManager = graphicsDeviceManager;
    }

    public void RenderDisplay()
    {
        var texture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.ScreenWidthPixels, Constants.ScreenHeightPixels);

        for (var p = 0; p < Constants.ScreenWidthPixels * Constants.ScreenHeightPixels; p++)
        {
            _data[p] = GetColor(_screenFrame[p]);
        }

        texture.SetData(_data);

        _display = texture;

        unchecked
        {
            _frameCount++;
        }

        if (_frameCount % FlashFrames == 0)
        {
            _flash = ! _flash;
        }
    }

    private Color GetColor(ushort pixel)
    {
        var flash = (pixel & 0b0000_0010_0000_0000) > 0;

        var color = (flash && _flash) ^ ((pixel & 01000_0000_0000_0000) > 0)
                        ? pixel & 0b0000_0111
                        : (pixel & 0b0011_1000) >> 3;

        if ((pixel & 0b0000_0001_0000_0000) > 0)
        {
            return color switch
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

        return color switch
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
}