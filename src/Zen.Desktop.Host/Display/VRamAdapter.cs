using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Constants = Zen.Common.Constants;

namespace Zen.Desktop.Host.Display;

public class VRamAdapter
{
    private readonly byte[] _screenFrame;

    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private Texture2D _display;

    public Texture2D Display => _display;

    public VRamAdapter(byte[] screenFrame, GraphicsDeviceManager graphicsDeviceManager)
    {
        _screenFrame = screenFrame;

        _graphicsDeviceManager = graphicsDeviceManager;
    }

    public void RenderDisplay()
    {
        var texture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.ScreenWidthPixels, Constants.ScreenHeightPixels);
       
        var data = new Color[Constants.ScreenWidthPixels * Constants.ScreenHeightPixels];

        for (var p = 0; p < 0xC000; p++)
        {
            data[p] = _screenFrame[p] == 0xFF ? Color.White : Color.Black;
           // data[p] = GetColor(_screenFrame[p]);
        }

        texture.SetData(data);

        _display = texture;
    }

    private static Color GetColor(byte pixel)
    {
        Color color;

        if ((pixel & 0b1000_0000) > 0)
        {
            color = ((pixel & 0b0000_0111) >> 3) switch
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
            color = ((pixel & 0b0000_0111) >> 3) switch
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

        return color;
    }
}