using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Zen.Common;
using Zen.Common.Infrastructure;
using Zen.Desktop.Host.Infrastructure;

namespace Zen.Desktop.Host.Features;

public class CountersVisualiser
{
    private readonly Color[] _data;
    
    private readonly Color[] _characterSet;

    private readonly Texture2D _texture;

    public CountersVisualiser(GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager)
    {
        var characterSet = contentManager.Load<Texture2D>("character-set");
        
        _characterSet = new Color[7168];

        characterSet.GetData(_characterSet);

        _data = new Color[Constants.ScreenWidthPixels * Constants.CountersPanelHeight];

        _texture = new Texture2D(graphicsDeviceManager.GraphicsDevice, Constants.ScreenWidthPixels, Constants.CountersPanelHeight);
    }

    public Texture2D RenderPanel()
    {
        Array.Fill(_data, Color.Black);

        DrawString("PCFPS", Color.Magenta, 2, 0);
        DrawString(":", Color.White, 7, 0);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.RenderedFrames).ToString("N0"), Color.Cyan, 9, 0);

        DrawString("Z80Ops/s", Color.Magenta, 14, 0);
        DrawString(":", Color.White, 22, 0);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.Instructions).ToString("N0"), Color.Cyan, 24, 0);

        DrawString("ZXFPS", Color.Magenta, 2, 1);
        DrawString(":", Color.White, 7, 1);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.SpectrumFrames).ToString("N0"), Color.Cyan, 9, 1);

        DrawString("AYFPS", Color.Magenta, 14, 1);
        DrawString(":", Color.White, 22, 1);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.AyFrames).ToString("N0"), Color.Cyan, 24, 1);

        DrawString("IRQ/s", Color.Magenta, 2, 2);
        DrawString(":", Color.White, 7, 2);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.IRQs).ToString("N0"), Color.Cyan, 9, 2);

        DrawString("Hz", Color.Magenta, 14, 2);
        DrawString(":", Color.White, 22, 2);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.Hertz).ToString("N0"), Color.Cyan, 24, 2);
        
        _texture.SetData(_data);

        return _texture;
    }

    private void DrawString(string text, Color color, int x, int y)
    {
        for (var i = 0; i < text.Length; i++)
        {
            DrawCharacter(text[i], color, x + i, y);
        }
    }

    private void DrawCharacter(char c, Color color, int x, int y)
    {
        var co = CharacterMap.Instance.GetCharacterStartPixel(c);

        for (var bY = 0; bY < 8; bY++)
        {
            for (var bX = 0; bX < 8; bX++)
            {
                if (_characterSet[bY * 128 + bX + co].A == 0)
                {
                    continue;
                }

                _data[x * 8 + y * 8 * Constants.ScreenWidthPixels + bX + bY * Constants.ScreenWidthPixels] = color;
            }
        }
    }
}