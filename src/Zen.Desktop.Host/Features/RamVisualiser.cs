using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Common;
using Zen.System.Modules;

namespace Zen.Desktop.Host.Features;

public class RamVisualiser
{
    private const int SideSize = 256;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly Texture2D _visualisation;

    private Ram _ram;

    public Ram Ram
    {
        set => _ram = value;
    }

    public RamVisualiser(GraphicsDeviceManager graphicsDeviceManager, Ram ram)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        _ram = ram;

        _data = new Color[SideSize * SideSize];

        _visualisation = new Texture2D(_graphicsDeviceManager.GraphicsDevice, SideSize, SideSize);
    }

    public Texture2D RenderRam()
    {
        Array.Fill(_data, Color.Black);

        const int bytesPerRow = SideSize / 8;

        for (var y = 0; y < Constants.ScreenHeightPixels; y++)
        {
            for (var x = 0; x < bytesPerRow; x++)
            {
                var data = _ram[(ushort) (y * bytesPerRow + x)];

                for (var i = 0; i < 8; i++)
                {
                    _data[y * SideSize + x * 8 + i] = (data & (1 << (7 - i))) > 0 ? Color.LightGray : Color.Black;
                }
            }
        }

        _visualisation.SetData(_data);

        return _visualisation;
    }
}