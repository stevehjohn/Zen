using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;
using Zen.Common;
using Zen.Common.Infrastructure;
using Zen.Desktop.Host.Infrastructure;
using Zen.System;

namespace Zen.Desktop.Host.Features;

public class CountersVisualiser
{
    private readonly Color[] _data;

    private readonly Color[] _characterSet;

    private readonly Texture2D _texture;

    private Motherboard _motherboard;

    private int _lastRom;

    private readonly byte[] _previousMappings = new byte[4];

    private char _lastUlaBank;
    
    public Motherboard Motherboard
    {
        set => _motherboard = value;
    }

    public CountersVisualiser(GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager, Motherboard motherboard)
    {
        var characterSet = contentManager.Load<Texture2D>("character-set");

        _characterSet = new Color[7168];

        characterSet.GetData(_characterSet);

        _data = new Color[Constants.ScreenWidthPixels * Constants.CountersPanelHeight];

        _texture = new Texture2D(graphicsDeviceManager.GraphicsDevice, Constants.ScreenWidthPixels, Constants.CountersPanelHeight);

        _motherboard = motherboard;
    }

    public Texture2D RenderPanel()
    {
        Array.Fill(_data, Color.Black);

        DrawString("PCFPS", Color.Magenta, 1, 0);
        DrawString(":", Color.White, 6, 0);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.RenderedFrames).ToString("N0"), Color.Cyan, 8, 0);

        DrawString("Z80Ops/s", Color.Magenta, 13, 0);
        DrawString(":", Color.White, 22, 0);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.Instructions).ToString("N0"), Color.Cyan, 24, 0);

        DrawString("ZXFPS", Color.Magenta, 1, 1);
        DrawString(":", Color.White, 6, 1);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.SpectrumFrames).ToString("N0"), Color.Cyan, 8, 1);

        DrawString("AYFPS", Color.Magenta, 13, 1);
        DrawString(":", Color.White, 22, 1);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.AyFrames).ToString("N0"), Color.Cyan, 24, 1);

        DrawString("IRQ/s", Color.Magenta, 1, 2);
        DrawString(":", Color.White, 6, 2);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.IRQs).ToString("N0"), Color.Cyan, 8, 2);

        DrawString("Hz", Color.Magenta, 13, 2);
        DrawString(":", Color.White, 22, 2);
        DrawString(Counters.Instance.GetCountPerSecond(Counter.Hertz).ToString("N0"), Color.Cyan, 24, 2);

        DrawString("ROM", Color.Magenta, 1, 3);
        DrawString(":", Color.White, 6, 3);

        var selectedRom = _motherboard.SelectedRom;

        var text = selectedRom == -1
            ? "RAM"
            : selectedRom.ToString();
        
        DrawString(text, Color.Cyan, 8, 3, selectedRom != _lastRom);

        _lastRom = selectedRom;

        var data = new StringBuilder();

        DrawString("CPU Banks", Color.Magenta, 13, 3);
        DrawString(":", Color.White, 22, 3);

        for (var i = 0; i < 4; i++)
        {
            DrawString(data.ToString().Trim(), Color.Cyan, 24, 3);

            var mapping = _motherboard.Ram.GetBankMapping((byte) i);
            
            var character = mapping == 8
                ? '-'
                : (char) (mapping + '0');

            DrawCharacter(character, Color.Cyan, 24 + i * 2, 3, mapping != _previousMappings[i]);

            _previousMappings[i] = mapping;
        }

        DrawString("ms/F", Color.Magenta, 1, 4);
        DrawString(":", Color.White, 6, 4);
        DrawString($"{_motherboard.LastFrameMs:N0}", Color.Cyan, 8, 4);
        
        DrawString("ULA Bank", Color.Magenta, 13, 4);
        DrawString(":", Color.White, 22, 4);

        var ulaBank = _motherboard.Ram.UseShadowScreenBank ? '7' : '5';
        
        DrawCharacter(ulaBank, Color.Cyan, 24, 4, ulaBank != _lastUlaBank);

        _lastUlaBank = ulaBank;

        _texture.SetData(_data);

        return _texture;
    }

    private void DrawString(string text, Color color, int x, int y, bool invert = false)
    {
        for (var i = 0; i < text.Length; i++)
        {
            DrawCharacter(text[i], color, x + i, y, invert);
        }
    }

    private void DrawCharacter(char c, Color color, int x, int y, bool invert = false)
    {
        var co = CharacterMap.Instance.GetCharacterStartPixel(c);

        for (var bY = 0; bY < 8; bY++)
        {
            for (var bX = 0; bX < 8; bX++)
            {
                var set = _characterSet[bY * 128 + bX + co].A == 0;

                var draw = invert ? set : ! set;

                if (! draw)
                {
                    continue;
                }

                _data[x * 8 + y * 8 * Constants.ScreenWidthPixels + bX + bY * Constants.ScreenWidthPixels] = color;
            }
        }
    }
}