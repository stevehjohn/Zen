using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Common;
using Zen.System.Modules;

namespace Zen.Desktop.Host.Features;

public class RamVisualiser
{
    const int BytesPerRow = Constants.RamVisualisationPanelWidth / 8;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly Texture2D _visualisation;

    private Ram _ram;

    private int _offset = 0x8200;

    public Ram Ram
    {
        set => _ram = value;
    }

    public RamVisualiser(GraphicsDeviceManager graphicsDeviceManager, Ram ram)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        _ram = ram;

        _data = new Color[Constants.RamVisualisationPanelWidth * Constants.ScreenHeightPixels];

        _visualisation = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.RamVisualisationPanelWidth, Constants.ScreenHeightPixels);
    }

    public Texture2D RenderRam()
    {
        Array.Fill(_data, Color.Black);

        for (var y = 0; y < Constants.ScreenHeightPixels; y++)
        {
            for (var x = 0; x < BytesPerRow; x++)
            {
                var data = _ram[(ushort) (_offset + y * BytesPerRow + x)];

                for (var i = 0; i < 8; i++)
                {
                    _data[y * Constants.RamVisualisationPanelWidth + x * 8 + i] = (data & (1 << (7 - i))) > 0 ? Color.LightGray : Color.Black;
                }
            }
        }

        _visualisation.SetData(_data);
        
        return _visualisation;
    }
    //
    // private void Scroll(int direction)
    // {
    //     _offset += BytesPerRow;
    //
    //     if (_offset < 0)
    //     {
    //         // TODO
    //     }
    //     else if (_offset + Constants.ScreenHeightPixels * BytesPerRow > 0xFFFF)
    //     {
    //         _offset = 0;
    //     }
    // }
}