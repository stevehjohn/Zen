using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Common;
using Zen.System.Modules;

namespace Zen.Desktop.Host.Features;

public class VideoRamVisualiser
{
    private const int YStart = Constants.ScreenWidthPixels * Constants.BorderPixels;
    
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly Texture2D _visualisation;

    private readonly Ram _ram;

    public VideoRamVisualiser(GraphicsDeviceManager graphicsDeviceManager, Ram ram)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        _ram = ram;

        _data = new Color[Constants.VideoRamVisualisationPanelWidth * Constants.ScreenHeightPixels];

        _visualisation = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.VideoRamVisualisationPanelWidth, Constants.ScreenHeightPixels);
    }

    public Texture2D RenderRam()
    {
        Array.Fill(_data, Color.Black);

        for (var y = 0; y < Constants.PaperHeightPixels; y++)
        {
            for (var x = 0; x < Constants.PaperWidthPixels; x++)
            {
                var xB = x / 8;

                var xO = 1 << (7 - x % 8);

                var address = 0x4000;

                address |= (y & 0b0000_0111) << 8;

                address |= (y & 0b1100_0000) << 5;

                address |= (y & 0b0011_1000) << 2;

                address |= xB;

                if ((_ram[(ushort) address] & xO) > 0)
                {
                    _data[YStart + y * Constants.ScreenWidthPixels + Constants.BorderPixels + x] = Color.FromNonPremultiplied(192, 192, 192, 255);
                }
            }
        }

        _visualisation.SetData(_data);

        return _visualisation;
    }
}