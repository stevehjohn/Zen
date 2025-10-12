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

    private void RenderBank(int panel, Span<byte> bank)
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

                var colourAddress = 0x1800 + xB + y / 8 * 32;

                var attribute = bank[colourAddress];

                var bright = (attribute & 0x40) > 0;

                var ink = AppSettings.Instance.ColourScheme == ColourScheme.Spectrum
                    ? GetColor((byte) (attribute & 0x07), bright)
                    : GetC64Color((byte) (attribute & 0x07), bright);

                var paper = AppSettings.Instance.ColourScheme == ColourScheme.Spectrum
                    ? GetColor((byte) ((attribute >> 3) & 0x07), bright)
                    : GetC64Color((byte) ((attribute >> 3) & 0x07), bright);

                _data[(y + Constants.BorderPixels) * width + x + Constants.BorderPixels + offset] = (bank[(ushort) address] & xO) > 0 ? ink : paper;
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

    private static Color GetColor(byte index, bool bright)
    {
        var intensity = bright ? 0xFF : 0xD8;

        return index switch
        {
            1 => Color.FromNonPremultiplied(0, 0, intensity, 255),
            2 => Color.FromNonPremultiplied(intensity, 0, 0, 255),
            3 => Color.FromNonPremultiplied(intensity, 0, intensity, 255),
            4 => Color.FromNonPremultiplied(0, intensity, 0, 255),
            5 => Color.FromNonPremultiplied(0, intensity, intensity, 255),
            6 => Color.FromNonPremultiplied(intensity, intensity, 0, 255),
            7 => Color.FromNonPremultiplied(intensity, intensity, intensity, 255),
            _ => Color.FromNonPremultiplied(0, 0, 0, 255)
        };
    }

    private static Color GetC64Color(byte index, bool bright)
    {
        return index switch
        {
            1 => Color.FromNonPremultiplied(0, bright ? 136 : 0, bright ? 255 : 170, 255),
            2 => Color.FromNonPremultiplied(bright ? 255 : 136, bright ? 119 : 0, bright ? 119 : 0, 255),
            3 => Color.FromNonPremultiplied(204, 68, 204, 255),
            4 => Color.FromNonPremultiplied(bright ? 170 : 0, bright ? 255 : 204, bright ? 102 : 85, 255),
            5 => Color.FromNonPremultiplied(bright ? 170 : 0, bright ? 255 : 139, bright ? 238 : 139, 255),
            6 => Color.FromNonPremultiplied(bright ? 221 : 102, bright ? 136 : 68, bright ? 85 : 0, 255),
            7 => Color.FromNonPremultiplied(bright ? 255 : 187, bright ? 255 : 187, bright ? 255 : 187, 255),
            _ => Color.FromNonPremultiplied(0, 0, 0, 255)
        };
    }
}