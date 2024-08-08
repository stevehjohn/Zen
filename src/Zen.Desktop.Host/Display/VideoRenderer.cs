using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Desktop.Host.Infrastructure.Settings;
using Zen.System.Infrastructure;
using Color = Microsoft.Xna.Framework.Color;
using Constants = Zen.Common.Constants;

namespace Zen.Desktop.Host.Display;

public class VideoRenderer
{
    private const int FlashFrames = 20;

    private ushort[] _screenFrame;

    private readonly Texture2D _display;

    private ulong _frameCount;

    private bool _flash;

    private readonly Color[] _data = new Color[Constants.ScreenWidthPixels * Constants.ScreenHeightPixels];

    private int _y;
    
    public Texture2D Display => _display;

    public ushort[] ScreenFrame
    {
        set => _screenFrame = value;
    }

    public VideoRenderer(ushort[] screenFrame, GraphicsDeviceManager graphicsDeviceManager)
    {
        _screenFrame = screenFrame;

        _display = new Texture2D(graphicsDeviceManager.GraphicsDevice, Constants.ScreenWidthPixels, Constants.ScreenHeightPixels);
    }

    public void RenderDisplay()
    {
        if (AppSettings.Instance.Speed == Speed.Slow)
        {
            for (var x = 0; x < Constants.ScreenWidthPixels; x++)
            {
                var p = _y * Constants.ScreenWidthPixels + x;

                _data[p] = AppSettings.Instance.ColourScheme == ColourScheme.Spectrum ? GetColor(_screenFrame[p]) : GetC64Color(_screenFrame[p]);
            }

            _y++;

            if (_y >= Constants.ScreenHeightPixels)
            {
                _y = 0;
            }
        }
        else
        {
            for (var y = 0; y < Constants.ScreenHeightPixels; y++)
            {
                for (var x = 0; x < Constants.ScreenWidthPixels; x++)
                {
                    var p = y * Constants.ScreenWidthPixels + x;

                    _data[p] = AppSettings.Instance.ColourScheme == ColourScheme.Spectrum ? GetColor(_screenFrame[p]) : GetC64Color(_screenFrame[p]);
                }
            }
        }

        _display.SetData(_data);

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

        var intensity = (pixel & 0b0000_0001_0000_0000) > 0 ? 0xFF : 0xD8;
        
        return color switch
        {
            1 => Color.FromNonPremultiplied(0, 0, intensity, 255),
            2 => Color.FromNonPremultiplied(intensity, 0, 0, 255),
            3 => Color.FromNonPremultiplied(intensity, 0, intensity, 255),
            4 => Color.FromNonPremultiplied(0, intensity, 0, 255),
            5 => Color.FromNonPremultiplied(0, intensity, intensity, 255),
            6 => Color.FromNonPremultiplied(intensity, intensity, 0, 255),
            7 => Color.FromNonPremultiplied(intensity, intensity, intensity, 255),
            _ => Color.FromNonPremultiplied(0, 0, 0, 255)
        };
    }
    
    private Color GetC64Color(ushort pixel)
    {
        var flash = (pixel & 0b0000_0010_0000_0000) > 0;

        var color = (flash && _flash) ^ ((pixel & 01000_0000_0000_0000) > 0)
            ? pixel & 0b0000_0111
            : (pixel & 0b0011_1000) >> 3;

        var intensity = (pixel & 0b0000_0001_0000_0000) > 0;

        return color switch
        {
            1 => Color.FromNonPremultiplied(0, intensity ? 136 : 0, intensity ? 255 : 170, 255),
            2 => Color.FromNonPremultiplied(intensity ? 255 : 136, intensity ? 119 : 0,  intensity ? 119 : 0, 255),
            3 => Color.FromNonPremultiplied(204, 68, 204, 255),
            4 => Color.FromNonPremultiplied(intensity ? 170 : 0, intensity ? 255 : 204, intensity ? 102 : 85, 255),
            5 => Color.FromNonPremultiplied(intensity ? 170 : 0, intensity ? 255 : 139, intensity ? 238 : 139, 255),
            6 => Color.FromNonPremultiplied(intensity ? 221 : 102, intensity ? 136 : 68, intensity ? 85 : 0, 255),
            7 => Color.FromNonPremultiplied(intensity ? 255 : 187, intensity ? 255 : 187, intensity ? 255 : 187, 255),
            _ => Color.FromNonPremultiplied(0, 0, 0, 255)
        };
    }
}