using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Common;
using Zen.System.Modules;

namespace Zen.Desktop.Host.Features;

public class MemoryVisualiser
{
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Ram _ram;

    private Color[] _data;

    private int _scaleFactor;

    private Texture2D _view;

    private int _top;

    public Texture2D View => _view;

    public int ScaleFactor
    {
        get => _scaleFactor;
        set
        {
            _scaleFactor = value;

            _data = new Color[Constants.VisualisationPanelWidth * Constants.ScreenHeightPixels];

            _view = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.VisualisationPanelWidth, Constants.ScreenHeightPixels);
        }
    }

    public MemoryVisualiser(GraphicsDeviceManager graphicsDeviceManager, int scaleFactor, Ram ram)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        ScaleFactor = scaleFactor;

        _ram = ram;
    }

    public void Render()
    {
        var count = 0;

        _top += 25;

        for (var y = 0; y < Constants.VisualisationPanelWidth; y++)
        {
            byte content = 0;

            for (var x = 0; x < Constants.ScreenHeightPixels; x++)
            {
                if (x % 8 == 0)
                {
                    content = _ram[(ushort) (_top + count)];

                    count++;
                }

                _data[y * Constants.VisualisationPanelWidth + x] = (content & (1 << (8 - x % 8))) > 0 ? Color.White : Color.Black;
            }
        }

        _view.SetData(_data);
    }
}