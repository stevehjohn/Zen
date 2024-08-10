using System;
using System.Numerics;
using MathNet.Numerics.IntegralTransforms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Common;

namespace Zen.Desktop.Host.Features;

public class SpectrumAnalyser
{
    private const int BufferSize = System.Modules.Audio.Constants.SampleRate / Constants.SpectrumFramesPerSecond;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly Complex[][] _buffers;
    
    private readonly Texture2D _spectrum;

    private int _bufferPosition;

    private bool _rendering;

    private readonly FrequencyRange[] _frequencyRanges =
    [
        new FrequencyRange(20, 40),       // Sub-bass
        new FrequencyRange(40, 80),       // Bass
        new FrequencyRange(80, 160),      // Lower midrange
        new FrequencyRange(160, 320),     // Midrange
        new FrequencyRange(320, 640),     // Upper midrange
        new FrequencyRange(640, 1280),    // Presence
        new FrequencyRange(1280, 2560),   // Brilliance
        new FrequencyRange(2560, 5120),   // Air
        new FrequencyRange(5120, 10000),  // High Treble
        new FrequencyRange(10000, 20000)  // Ultra-High Treble
    ];

    public Texture2D Spectrum => _spectrum;

    public SpectrumAnalyser(GraphicsDeviceManager graphicsDeviceManager)
    {
        _graphicsDeviceManager = graphicsDeviceManager;
        
        _data = new Color[Constants.WaveVisualisationPanelWidth * Constants.ScreenHeightPixels];

        _spectrum = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.WaveVisualisationPanelWidth, Constants.ScreenHeightPixels);

        _buffers = new Complex[4][];

        for (var i = 0; i < 4; i++)
        {
            _buffers[i] = new Complex[BufferSize];
        }
    }

    public void ReceiveSignals(float[] signals)
    {
        if (_rendering)
        {
            return;
        }

        _buffers[0][_bufferPosition] = new Complex(signals[0], 0);
        _buffers[1][_bufferPosition] = new Complex(signals[1], 0);
        _buffers[2][_bufferPosition] = new Complex(signals[2], 0);

        _bufferPosition++;

        if (_bufferPosition >= BufferSize)
        {
            _bufferPosition = 0;

            if (! _rendering)
            {
                RenderSpectrum();
            }
        }
    }

    private void RenderSpectrum()
    {
        _rendering = true;

        Array.Fill(_data, Color.Black);

        RenderSpectrumChannel(0);
        RenderSpectrumChannel(1);
        RenderSpectrumChannel(2);

        _spectrum.SetData(_data);

        _rendering = false;
    }

    private void RenderSpectrumChannel(int channel)
    {
        Fourier.Forward(_buffers[channel], FourierOptions.Matlab);
        
        var height = Constants.ScreenHeightPixels / 4;

        var magnitudes = new float[_buffers[channel].Length];
        
        var axis = height * Constants.SpectrumVisualisationPanelWidth * (channel + 1) - Constants.SpectrumVisualisationPanelWidth;

        for (var i = 0; i < magnitudes.Length; i++)
        {
            magnitudes[i] = (float) Math.Sqrt(_buffers[channel][i].Real * _buffers[channel][i].Real + _buffers[channel][i].Imaginary * _buffers[channel][i].Imaginary);
        }
        
        var groupedMagnitudes = new float[10];
        
        for (var i = 0; i < 10; i++)
        {
            var lowBin = (int)Math.Round(_frequencyRanges[i].Low * BufferSize / 48_000);
            var highBin = (int)Math.Round(_frequencyRanges[i].High * BufferSize / 48_000);

            float sum = 0;
            
            for (var j = lowBin; j <= highBin; j++)
            {
                sum += magnitudes[j];
            }
            
            groupedMagnitudes[i] = sum / (highBin - lowBin + 1);
        }

        for (var i = 0; i < 10; i++)
        {
            var dataPoint = groupedMagnitudes[i] / 500;

            var offset = -(int) (dataPoint * height * (channel == 3 ? 1 : 4));

            for (var x = 0; x < 10; x++)
            {
                _data[axis + (i * 10) + x + offset * Constants.SpectrumVisualisationPanelWidth] = Color.LightGreen;
            }
        }
    }
    
    private class FrequencyRange
    {
        public float Low { get; }
        public float High { get; }

        public FrequencyRange(float low, float high)
        {
            Low = low;
            High = high;
        }
    }
}