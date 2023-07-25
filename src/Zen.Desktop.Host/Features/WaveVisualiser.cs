using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Zen.Common;

namespace Zen.Desktop.Host.Features;

public class WaveVisualiser
{
    private const int BufferSize = 44_100;

    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly float[][] _buffers;

    private int _bufferPosition;

    private Texture2D _waves;

    public int ScaleFactor { get; set; }

    public Texture2D Waves => _waves;

    public WaveVisualiser(GraphicsDeviceManager graphicsDeviceManager, int scaleFactor)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        ScaleFactor = scaleFactor;

        _data = new Color[Constants.WavePanelWidth * ScaleFactor * Constants.ScreenHeightPixels * ScaleFactor];

        _buffers = new float[6][];

        for (var i = 0; i < 6; i++)
        {
            _buffers[i] = new float[BufferSize];
        }
    }

    public void ReceiveSignals(float[] signals)
    {
        _buffers[0][_bufferPosition] = signals[0];
        _buffers[1][_bufferPosition] = signals[1];
        _buffers[2][_bufferPosition] = signals[2];

        _bufferPosition++;

        if (_bufferPosition > BufferSize)
        {
            _bufferPosition = 0;

            RenderWaves();
        }
    }

    private void RenderWaves()
    {
        var texture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.WavePanelWidth * ScaleFactor, Constants.ScreenHeightPixels * ScaleFactor);
        
        Array.Fill(_data, Color.Black);

        for (var i = 0; i < 3; i++)
        {
            RenderChannel(i);
        }

        texture.SetData(_data);

        _waves = texture;
    }

    private void RenderChannel(int channel)
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