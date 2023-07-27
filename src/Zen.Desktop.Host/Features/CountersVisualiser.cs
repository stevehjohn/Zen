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
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;
    
    private readonly Color[] _characterSet;

    private readonly ContentManager _contentManager;

    private readonly Texture2D _texture;

    public CountersVisualiser(GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager)
    {
        _graphicsDeviceManager = graphicsDeviceManager;
        
        _contentManager = contentManager;

        var characterSet = contentManager.Load<Texture2D>("character-set");
        
        _characterSet = new Color[7168];

        characterSet.GetData(_characterSet);

        _data = new Color[Constants.ScreenWidthPixels * Constants.CountersPanelHeight];

        _texture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.ScreenWidthPixels, Constants.CountersPanelHeight);
    }

    public Texture2D RenderPanel()
    {
        Array.Fill(_data, Color.Black);

        DrawString("FPS", Color.Magenta, 2, 0);
        DrawString(":", Color.White, 5, 0);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.RenderedFrames).ToString(), Color.Cyan, 7, 0);

        DrawString("Z80Ops/s", Color.Magenta, 15, 0);
        DrawString(":", Color.White, 23, 0);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.Instructions).ToString(), Color.Cyan, 25, 0);

        DrawString("Hz", Color.Magenta, 2, 1);
        DrawString(":", Color.White, 5, 1);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.SpectrumFrames).ToString(), Color.Cyan, 7, 1);

        DrawString("AYFPS", Color.Magenta, 15, 1);
        DrawString(":", Color.White, 23, 1);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.AyFrames).ToString(), Color.Cyan, 25, 1);

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