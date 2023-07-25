using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading.Tasks;
using Zen.Common;

namespace Zen.Desktop.Host.Features;

public class WaveVisualiser
{
    private const int BufferSize = 44_100 / 50;

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

        if (_bufferPosition >= BufferSize)
        {
            _bufferPosition = 0;

            RenderWaves();
        }
    }

    private void RenderWaves()
    {
        if (_graphicsDeviceManager.GraphicsDevice == null)
        {
            return;
        }

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

        var buffer = _buffers[channel];

        var length = buffer.Length;

        var lastOffset = 0;

        for (var x = 0; x < width; x++)
        {
            var dataPoint = buffer[x * (int) ((float) length / width)];

            var offset = (int) (dataPoint * height * 2.5f);

            _data[mid + x + offset * width] = Color.Green;

            if (offset != lastOffset)
            {
                var direction = offset > 0 ? 1 : -1;

                for (var y = 0; y != offset; y += direction)
                {
                    _data[mid + x + y * width] = Color.Green;
                }
            }

            lastOffset = offset;
        }
    }
}