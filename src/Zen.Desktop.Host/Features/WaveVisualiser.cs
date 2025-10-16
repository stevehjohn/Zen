using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Constants = Zen.Common.Constants;

namespace Zen.Desktop.Host.Features;

public class WaveVisualiser
{
    private const int StepReset = 16;
    
    private const int BufferSize = System.Modules.Audio.Constants.SampleRate / Constants.SpectrumFramesPerSecond;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly float[][] _buffers;
    
    private readonly Texture2D _waves;

    private int _beeperBufferPosition;

    public Texture2D Waves => _waves;

    private int _step;

    public WaveVisualiser(GraphicsDeviceManager graphicsDeviceManager)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        _data = new Color[Constants.WaveVisualisationPanelWidth * Constants.ScreenHeightPixels];

        _waves = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.WaveVisualisationPanelWidth, Constants.ScreenHeightPixels);

        _buffers = new float[4][];

        for (var i = 0; i < 4; i++)
        {
            _buffers[i] = new float[BufferSize];
        }
    }
    
    public void ReceiveSignals(float[] signals)
    {
        _step++;

        if (_step < StepReset)
        {
            return;
        }

        _step = 0;

        for (var i = 0; i < BufferSize - 1; i++)
        {
            _buffers[0][i] = _buffers[0][i + 1];
            _buffers[1][i] = _buffers[1][i + 1];
            _buffers[2][i] = _buffers[2][i + 1];
        }

        _buffers[0][BufferSize - 1] = signals[0];
        _buffers[1][BufferSize - 1] = signals[1];
        _buffers[2][BufferSize - 1] = signals[2];
    }

    public void Draw()
    {
        RenderWaves();
        
        _waves.SetData(_data);
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
        Array.Fill(_data, Color.Black);

        for (var i = 0; i < 4; i++)
        { 
            RenderChannel(i);
        }
    }

    private void RenderChannel(int channel)
    {
        var width = Constants.WaveVisualisationPanelWidth;

        var height = Constants.ScreenHeightPixels / 4;

        var axis = channel == 3 ? height * width * channel + height * width / 2 : height * width * (channel + 1) - width;

        var lastOffset = 0;

        var color = channel == 3 ? Color.Blue : Color.Green;

        for (var x = 0; x < width; x++)
        {
            var dataPoint = _buffers[channel][(int) (x * ((float) BufferSize / width))];

            var offset = -(int) (dataPoint * height * (channel == 3 ? 1 : 4));

            if (channel == 3)
            {
                _data[axis + x + offset * width] = color;
            }

            if (channel != 3 || (x > 0 && offset != lastOffset))
            {
                var direction = offset > lastOffset ? 1 : -1;

                for (var y = lastOffset; y != offset; y += direction)
                {
                    _data[axis + x + y * width] = color;
                }
            }

            if (channel == 3)
            {
                lastOffset = offset;
            }
        }
    }
}