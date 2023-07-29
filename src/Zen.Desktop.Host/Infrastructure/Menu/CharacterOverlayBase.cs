using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Zen.Common;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class CharacterOverlayBase
{
    private const int ColorDecrement = 20;

    private const int ColorFrameDelay = 5;

    private readonly Color[] _characterSet;

    private int _colorOffset;

    private int _colorFrame;

    protected readonly Texture2D Background;

    protected readonly GraphicsDeviceManager GraphicsDeviceManager;

    protected readonly ContentManager ContentManager;
    
    public Texture2D Menu { get; protected set; }

    public CharacterOverlayBase(Texture2D background, GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager)
    {
        Background = background;

        GraphicsDeviceManager = graphicsDeviceManager;

        ContentManager = contentManager;

        var characterSet = contentManager.Load<Texture2D>("character-set");

        _characterSet = new Color[7168];

        characterSet.GetData(_characterSet);
    }
    
    protected static void DrawWindow(Color[] data)
    {
        for (var y = 16; y < 208; y++)
        {
            for (var x = 24; x < 264; x++)
            {
                var i = y * Constants.ScreenWidthPixels + x;

                var color = data[i];

                color = y < 18 || y > 205 || x < 26 || x > 261 ? Color.White : Color.FromNonPremultiplied(color.R / 5, color.G / 5, color.B / 5, color.A);

                data[i] = color;
            }
        }
    }

    protected void DrawString(Color[] data, string text, int x, int y, Color color, bool centered = false, bool invert = false)
    {
        var xOffset = 0;

        if (centered)
        {
            x = 14 - text.Length / 2;

            if (text.Length % 2 == 1)
            {
                xOffset = -4;
            }
        }

        for (var i = 0; i < text.Length; i++)
        {
            DrawMenuCharacter(data, text[i], x + i, y, color, xOffset, invert);
        }

        if (invert)
        {
            var offset = _colorOffset;

            for (var iy = 0; iy < 8; iy++)
            {
                var decrement = ColorDecrement * offset;

                var lineColor = Color.FromNonPremultiplied(color.R - decrement, color.G - decrement, color.B - decrement, color.A);

                offset++;

                if (offset > 7)
                {
                    offset = 0;
                }

                for (var ix = 26; ix < (x + 4) * 8; ix++)
                {
                    data[(3 + y) * 2304 + ix + iy * 288 + xOffset] = lineColor;
                }

                for (var ix = (4 + x + text.Length) * 8; ix < 262; ix++)
                {
                    data[(3 + y) * 2304 + ix + iy * 288 + xOffset] = lineColor;
                }
            }
        }
    }
    
    protected void UpdateTextAnimation()
    {
        _colorFrame++;

        if (_colorFrame < ColorFrameDelay)
        {
            return;
        }

        _colorFrame = 0;

        _colorOffset--;

        if (_colorOffset < 0)
        {
            _colorOffset = 7;
        }
    }

    private void DrawMenuCharacter(Color[] data, char character, int x, int y, Color color, int xOffset, bool invert = false)
    {
        var co = CharacterMap.Instance.GetCharacterStartPixel(character);

        var offset = _colorOffset;

        var a = invert ? 255 : 0;

        for (var iy = 0; iy < 8; iy++)
        {
            var decrement = ColorDecrement * offset;

            var textColor = Color.FromNonPremultiplied(color.R - decrement, color.G - decrement, color.B - decrement, color.A);

            offset++;

            if (offset > 7)
            {
                offset = 0;
            }

            for (var ix = 0; ix < 8; ix++)
            {
                if (_characterSet[iy * 128 + ix + co].A == a)
                {
                    continue;
                }

                data[(3 + y) * 2304 + (x + 4) * 8 + ix + iy * 288 + xOffset] = textColor;
            }
        }
    }
}