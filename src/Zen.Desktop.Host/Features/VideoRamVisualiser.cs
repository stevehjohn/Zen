using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Common;
using Zen.Desktop.Host.Display;
using Zen.Desktop.Host.Infrastructure.Settings;
using Zen.System.Modules;

namespace Zen.Desktop.Host.Features;

public class VideoRamVisualiser
{
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly Texture2D _visualisation;

    private Ram _ram;

    private readonly bool _banks;

    private readonly VideoRenderer _videoRenderer;
    
    public bool BanksView => _banks;
    
    public Ram Ram
    {
        set => _ram = value;
    }

    public VideoRamVisualiser(GraphicsDeviceManager graphicsDeviceManager, Ram ram, bool banks, VideoRenderer videoRenderer)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        _ram = ram;

        _banks = banks;

        _videoRenderer = videoRenderer;

        _data = new Color[Constants.VideoRamVisualisationPanelWidth * Constants.ScreenHeightPixels * (banks ? 2 : 1)];

        _visualisation = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.VideoRamVisualisationPanelWidth * (banks ? 2 : 1), Constants.ScreenHeightPixels);
    }

    public Texture2D RenderRam()
    {
        Array.Fill(_data, Color.Black);

        if (_banks)
        {
            RenderBank(0, _ram.GetBank(5));
            
            RenderBank(1, _ram.GetBank(7));
        }
        else
        {
            RenderBank(0, _ram.WorkingScreenRam);
        }
        
        _visualisation.SetData(_data);

        return _visualisation;
    }

    private void RenderBank(int panel, byte[] bank)
    {
        var width = Constants.ScreenWidthPixels;

        var offset = 0;
        
        if (_banks)
        {
            width *= 2;

            if (panel == 1)
            {
                offset = Constants.ScreenWidthPixels;
            }
        }

        for (var y = 0; y < Constants.PaperHeightPixels; y++)
        {
            for (var x = 0; x < Constants.PaperWidthPixels; x++)
            {
                var xB = x / 8;

                var xO = 1 << (7 - x % 8);

                var address = 0;
                
                address |= (y & 0b0000_0111) << 8;

                address |= (y & 0b1100_0000) << 5;

                address |= (y & 0b0011_1000) << 2;

                address |= xB;

                if ((bank[(ushort) address] & xO) > 0)
                {
                    _data[(y + Constants.BorderPixels) * width + x + Constants.BorderPixels + offset] = Color.FromNonPremultiplied(192, 192, 192, 255);
                }
            }
        }

        if (AppSettings.Instance.Speed == Speed.Slow && _videoRenderer.ScanY > -1 && _videoRenderer.ScanY < Constants.ScreenHeightPixels - 4)
        {
            for (var x = 0; x < Constants.ScreenWidthPixels; x++)
            {
                _data[_videoRenderer.ScanY * width + x + offset] = Color.Black;
                _data[(_videoRenderer.ScanY + 1) * width + x + offset] = Color.White;
                _data[(_videoRenderer.ScanY + 2) * width + x + offset] = Color.Black;
            }
        }
    }
}