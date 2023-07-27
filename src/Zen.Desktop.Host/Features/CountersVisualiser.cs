using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Zen.Common;

namespace Zen.Desktop.Host.Features;

public class CountersVisualiser
{
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;
    
    private readonly Color[] _characterSet;

    private readonly ContentManager _contentManager;

    private Texture2D _texture;

    public int ScaleFactor { get; set; }

    public CountersVisualiser(GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager, int scaleFactor)
    {
        _graphicsDeviceManager = graphicsDeviceManager;
        
        _contentManager = contentManager;

        var characterSet = contentManager.Load<Texture2D>("character-set");
        
        _characterSet = new Color[7168];

        characterSet.GetData(_characterSet);

        ScaleFactor = scaleFactor;

        _data = new Color[Constants.WavePanelWidth * ScaleFactor * Constants.CountersPanelHeight * ScaleFactor];
    }

    public Texture2D RenderPanel()
    {
        if (_graphicsDeviceManager.GraphicsDevice == null)
        {
            return null;
        }

        if (_texture == null)
        {
            _texture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.WavePanelWidth * ScaleFactor, Constants.CountersPanelHeight * ScaleFactor);
        }

        return _texture;
    }
}