using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Common;

namespace Zen.Desktop.Host.Features;

public class VideoRamVisualiser
{
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly Texture2D _ram;

    public VideoRamVisualiser(GraphicsDeviceManager graphicsDeviceManager)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        _data = new Color[Constants.VideoRamVisualisationPanelWidth * Constants.ScreenHeightPixels];

        _ram = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.VideoRamVisualisationPanelWidth, Constants.ScreenHeightPixels);
    }

    public Texture2D RenderRam()
    {
        Array.Fill(_data, Color.Black);

        _ram.SetData(_data);

        return _ram;
    }
}