using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Common;
using Zen.System.Modules;

namespace Zen.Desktop.Host.Features;

public class WaveVisualiser
{
    private const int BufferMultiplier = 3;

    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly float[][] _buffers;

    public int ScaleFactor { get; set; }

    public WaveVisualiser(GraphicsDeviceManager graphicsDeviceManager, int scaleFactor)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        ScaleFactor = scaleFactor;

        _data = new Color[Constants.WavePanelWidth * ScaleFactor * Constants.ScreenHeightPixels * ScaleFactor];

        _buffers = new float[6][];

        for (var i = 0; i < 6; i++)
        {
            _buffers[i] = new float[System.Modules.Audio.Constants.BufferSize * BufferMultiplier];
        }
    }

    public void Receive(float[] signals)
    {
    }

    public Texture2D RenderWaves(AyAudio ayAudio)
    {
        var texture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.WavePanelWidth * ScaleFactor, Constants.ScreenHeightPixels * ScaleFactor);
        
        Array.Fill(_data, Color.Black);

        if (ayAudio != null)
        {
            for (var i = 0; i < 3; i++)
            {
                RenderChannel(i, ayAudio);
            }
        }

        texture.SetData(_data);

        return texture;
    }

    private void RenderChannel(int channel, AyAudio ayAudio)
    {
        var width = Constants.WavePanelWidth * ScaleFactor;

        var height = Constants.ScreenHeightPixels * ScaleFactor / 3;

        var mid = height * width * channel + height * width / 2;

        var buffer = new float[100]; // ayAudio.Buffers[channel];

        var length = buffer.Length;

        for (var x = 0; x < width; x++)
        {
            var dataPoint = buffer[x * (length / width)];

            var offset = width * dataPoint * (height / 2f);

            _data[(int) (mid + x + offset - Constants.WavePanelWidth * ScaleFactor)] = Color.Green;
            _data[(int) (mid + x + offset)] = Color.Green;
            _data[(int) (mid + x + offset + Constants.WavePanelWidth * ScaleFactor)] = Color.Green;
        }
    }
}