using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Constants = Zen.Common.Constants;

namespace Zen.Desktop.Host.Features;

public class WaveVisualiser
{
    private const int BufferSize = System.Modules.Audio.Constants.SampleRate / Constants.SpectrumFramesPerSecond;

    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private Color[] _data;

    private readonly float[][] _buffers;

    private int _bufferPosition;

    private int _beeperBufferPosition;

    private Texture2D _waves;

    private int _scaleFactor;

    public int ScaleFactor
    {
        get => _scaleFactor;
        set
        {
            _scaleFactor = value;

            _data = new Color[Constants.WavePanelWidth * _scaleFactor * Constants.ScreenHeightPixels * _scaleFactor];

            _waves = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.WavePanelWidth * _scaleFactor, Constants.ScreenHeightPixels * _scaleFactor);
        }
    }

    private bool _rendering;

    public Texture2D Waves => _waves;

    public WaveVisualiser(GraphicsDeviceManager graphicsDeviceManager, int scaleFactor)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        _scaleFactor = scaleFactor;

        _data = new Color[Constants.WavePanelWidth * _scaleFactor * Constants.ScreenHeightPixels * _scaleFactor];

        _waves = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.WavePanelWidth * _scaleFactor, Constants.ScreenHeightPixels * _scaleFactor);

        _buffers = new float[4][];

        for (var i = 0; i < 4; i++)
        {
            _buffers[i] = new float[BufferSize];
        }
    }

    public void ReceiveSignals(float[] signals)
    {
        if (_rendering)
        {
            return;
        }

        _buffers[0][_bufferPosition] = signals[0];
        _buffers[1][_bufferPosition] = signals[1];
        _buffers[2][_bufferPosition] = signals[2];

        _bufferPosition++;

        if (_bufferPosition >= BufferSize)
        {
            _bufferPosition = 0;

            if (! _rendering)
            {
                RenderWaves();
            }
        }
    }

    public void ReceiveSignal(float signal)
    {
        _buffers[3][_beeperBufferPosition] = -signal / 3;

        _beeperBufferPosition++;

        if (_beeperBufferPosition >= BufferSize)
        {
            _beeperBufferPosition = 0;
        }
    }

    private void RenderWaves()
    {
        _rendering = true;

        Array.Fill(_data, Color.Black);

        for (var i = 0; i < 4; i++)
        { 
            RenderChannel(i);
        }

        _waves.SetData(_data);

        _rendering = false;
    }

    private void RenderChannel(int channel)
    {
        var width = Constants.WavePanelWidth * _scaleFactor;

        var height = Constants.ScreenHeightPixels * _scaleFactor / 4;

        var axis = channel == 3 ? height * width * channel + height * width / 2 : height * width * (channel + 1) - width;

        var buffer = _buffers[channel];

        var length = buffer.Length;

        var lastOffset = 0;

        var color = channel == 3 ? Color.Blue : Color.Green;

        for (var x = 0; x < width; x++)
        {
            var dataPoint = buffer[(int) (x * ((float) length / width))];

            var offset = -(int) (dataPoint * height * (channel == 3 ? 1 : 4));

            _data[axis + x + offset * width - width] = color;
            _data[axis + x + offset * width] = color;
            _data[axis + x + offset * width + width] = color;

            if (x > 0 && offset != lastOffset)
            {
                var direction = offset > lastOffset ? 1 : -1;

                for (var y = lastOffset; y != offset; y += direction)
                {
                    _data[axis + x + y * width - width] = color;
                    _data[axis + x + y * width - 1] = color;
                    _data[axis + x + y * width] = color;
                    _data[axis + x + y * width + 1] = color;
                    _data[axis + x + y * width + width] = color;
                }
            }

            lastOffset = offset;
        }
    }
}