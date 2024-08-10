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

    private const int MagnitudeDivisor = 250;

    private const int BarWidth = 8;

    private const int BarSpacing = 4;

    private const int SegmentHeight = 5;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly Complex[][] _buffers;

    private readonly int[][] _peaks;

    private readonly long[][] _peakTimes;
    
    private readonly Texture2D _spectrum;

    private int _bufferPosition;

    private bool _rendering;

    private readonly Color[] _palette;

    private readonly FrequencyRange[] _frequencyRanges =
    [
        new FrequencyRange(30, 40),
        new FrequencyRange(40, 60),       // Bass
        new FrequencyRange(60, 80),
        new FrequencyRange(80, 120),      // Lower midrange
        new FrequencyRange(120, 160),
        new FrequencyRange(160, 240),     // Midrange
        new FrequencyRange(240, 320),
        new FrequencyRange(320, 480),     // Upper midrange
        new FrequencyRange(480, 640),
        new FrequencyRange(640, 960),     // Presence
        new FrequencyRange(960, 1280),
        new FrequencyRange(1280, 1920),   // Brilliance
        new FrequencyRange(1920, 2560)
    ];

    public Texture2D Spectrum => _spectrum;

    public SpectrumAnalyser(GraphicsDeviceManager graphicsDeviceManager)
    {
        _graphicsDeviceManager = graphicsDeviceManager;
        
        _data = new Color[Constants.WaveVisualisationPanelWidth * Constants.ScreenHeightPixels];

        _spectrum = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.WaveVisualisationPanelWidth, Constants.ScreenHeightPixels);

        _buffers = new Complex[4][];

        _peaks = new int[4][];

        _peakTimes = new long[4][];

        for (var i = 0; i < 4; i++)
        {
            _buffers[i] = new Complex[BufferSize];

            _peaks[i] = new int[_frequencyRanges.Length];

            _peakTimes[i] = new long[_frequencyRanges.Length];
        }
    
        _palette = PaletteGenerator.GetPalette(46,
        [
            new Color(119, 35, 172),
            new Color(176, 83, 203),
            new Color(255, 168, 76),
            new Color(254, 211, 56),
            new Color(254, 253, 0)
        ]);
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
        
        var groupedMagnitudes = new float[_frequencyRanges.Length];
        
        for (var i = 0; i < _frequencyRanges.Length; i++)
        {
            var lowBin = (int)Math.Round(_frequencyRanges[i].Low * BufferSize / System.Modules.Audio.Constants.SampleRate);
            var highBin = (int)Math.Round(_frequencyRanges[i].High * BufferSize / System.Modules.Audio.Constants.SampleRate);

            float sum = 0;
            
            for (var j = lowBin; j <= highBin; j++)
            {
                sum += magnitudes[j];
            }
            
            groupedMagnitudes[i] = sum / (highBin - lowBin + 1);
        }

        for (var i = 0; i < _frequencyRanges.Length; i++)
        {
            var dataPoint = groupedMagnitudes[i] / MagnitudeDivisor;

            var peak = (int) (dataPoint * height * (channel == 3 ? 1 : 4));
            
            if (peak > _peaks[channel][i])
            {
                _peaks[channel][i] = peak;

                _peakTimes[channel][i] = Environment.TickCount64;
            }
            else
            {
                if (Environment.TickCount64 - _peakTimes[channel][i] > 100 && _peaks[channel][i] > 0)
                {
                    _peaks[channel][i]--;
                }
            }

            for (var x = 0; x < BarWidth; x++)
            {
                // TODO: Magic number
                _data[22 + axis + i * (BarWidth + BarSpacing) + x - _peaks[channel][i] * Constants.SpectrumVisualisationPanelWidth] = Color.FromNonPremultiplied(192, 192, 192, 255);

                var offset = -(int) (dataPoint * height * (channel == 3 ? 1 : 4));
                
                while (offset <= 0)
                {
                    if (Math.Abs(offset) % SegmentHeight == 4)
                    {
                        offset++;
                        
                        continue;
                    }

                    _data[22 + axis + i * (BarWidth + BarSpacing) + x + offset * Constants.SpectrumVisualisationPanelWidth] = _palette[-offset];

                    offset++;
                }
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

    private static class PaletteGenerator
    {
        public static Color[] GetPalette(int steps, Color[] markers)
        {
            var palette = new Color[steps];

            var markerPeriod = steps / (markers.Length - 2);

            var current = markers[0];

            var next = markers[1];

            var counter = markerPeriod;

            var markerIndex = 1;

            for (var i = 0; i < steps; i++)
            {
                palette[i] = new Color(current.R, current.G, current.B);

                current.R += (byte) ((next.R - current.R) / markerPeriod);
                current.G += (byte) ((next.G - current.G) / markerPeriod);
                current.B += (byte) ((next.B - current.B) / markerPeriod);

                counter--;

                if (counter == 0)
                {
                    counter = markerPeriod;

                    markerIndex++;

                    next = markers[markerIndex];
                }
            }

            return palette;
        }
    }
}