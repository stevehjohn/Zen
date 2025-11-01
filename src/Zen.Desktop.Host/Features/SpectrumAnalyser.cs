using System;
using System.Numerics;
using MathNet.Numerics.IntegralTransforms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Common;
using Zen.Desktop.Host.Features.SupportingClasses;

namespace Zen.Desktop.Host.Features;

public class SpectrumAnalyser
{
    private const int BufferSize = System.Modules.Audio.Constants.SampleRate / Constants.SpectrumFramesPerSecond;

    private const int MagnitudeDivisor = 250;

    private const int PeakDelayMilliseconds = 250;

    private const int BarWidth = 8;

    private const int BarSpacing = 4;

    private const int SegmentHeight = 5;

    private readonly int _leftPadding;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly Complex[][] _buffers;

    private readonly int[][] _peaks;

    private readonly long[][] _peakTimes;

    private readonly Texture2D _spectrum;

    private int _bufferPosition;

    private int _beeperBufferPosition;

    private bool _rendering;

    private readonly Color[] _palette;

    private readonly float[] _magnitudes;

    private readonly float[] _groupedMagnitudes;

    private readonly FrequencyRange[] _frequencyRanges =
    [
        new(30, 40),
        new(40, 60), // Bass
        new(60, 80),
        new(80, 120), // Lower midrange
        new(120, 160),
        new(160, 240), // Midrange
        new(240, 320),
        new(320, 480), // Upper midrange
        new(480, 640),
        new(640, 960), // Presence
        new(960, 1280),
        new(1280, 1920), // Brilliance
        new(1920, 2560)
    ];

    public Texture2D Spectrum => _spectrum;

    public SpectrumAnalyser(GraphicsDeviceManager graphicsDeviceManager)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        _data = new Color[Constants.SpectrumAnalyserVisualisationPanelWidth * Constants.ScreenHeightPixels];

        _spectrum = new Texture2D(_graphicsDeviceManager.GraphicsDevice, Constants.SpectrumAnalyserVisualisationPanelWidth, Constants.ScreenHeightPixels);

        _buffers = new Complex[4][];

        _peaks = new int[4][];

        _peakTimes = new long[4][];

        _magnitudes = new float[BufferSize];

        _groupedMagnitudes = new float[_frequencyRanges.Length];

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

        _leftPadding = (Constants.SpectrumAnalyserVisualisationPanelWidth - (BarWidth + BarSpacing) * _frequencyRanges.Length) / 2;
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

    public void ReceiveSignal(float signal)
    {
        _buffers[3][_beeperBufferPosition] = new Complex(-signal / 1.5, 0);

        _beeperBufferPosition++;

        if (_beeperBufferPosition >= BufferSize)
        {
            _beeperBufferPosition = 0;
        }
    }

    private void RenderSpectrum()
    {
        _rendering = true;

        Array.Fill(_data, Color.Black);

        RenderSpectrumChannel(0);
        RenderSpectrumChannel(1);
        RenderSpectrumChannel(2);
        RenderSpectrumChannel(3);

        _spectrum.SetData(_data);

        _rendering = false;
    }

    private void RenderSpectrumChannel(int channel)
    {
        Fourier.Forward(_buffers[channel], FourierOptions.Matlab);

        var height = Constants.ScreenHeightPixels / 4;

        var axis = height * Constants.SpectrumAnalyserVisualisationPanelWidth * (channel + 1) - Constants.SpectrumAnalyserVisualisationPanelWidth;

        for (var i = 0; i < _magnitudes.Length; i++)
        {
            _magnitudes[i] = (float) Math.Sqrt(_buffers[channel][i].Real * _buffers[channel][i].Real + _buffers[channel][i].Imaginary * _buffers[channel][i].Imaginary);
        }

        for (var i = 0; i < _frequencyRanges.Length; i++)
        {
            var lowBin = (int) Math.Round(_frequencyRanges[i].Low * BufferSize / System.Modules.Audio.Constants.SampleRate);
            var highBin = (int) Math.Round(_frequencyRanges[i].High * BufferSize / System.Modules.Audio.Constants.SampleRate);

            float sum = 0;

            for (var j = lowBin; j <= highBin; j++)
            {
                sum += _magnitudes[j];
            }

            _groupedMagnitudes[i] = sum / (highBin - lowBin + 1);
        }

        for (var i = 0; i < _frequencyRanges.Length; i++)
        {
            var dataPoint = _groupedMagnitudes[i] / MagnitudeDivisor;

            var peak = (int) (dataPoint * height * (channel == 3 ? 1 : 4));

            if (peak > _peaks[channel][i])
            {
                _peaks[channel][i] = peak;

                _peakTimes[channel][i] = Environment.TickCount64;
            }
            else
            {
                if (Environment.TickCount64 - _peakTimes[channel][i] > PeakDelayMilliseconds && _peaks[channel][i] > 0)
                {
                    _peaks[channel][i]--;
                }
            }

            for (var x = 0; x < BarWidth; x++)
            {
                _data[_leftPadding + axis + i * (BarWidth + BarSpacing) + x - _peaks[channel][i] * Constants.SpectrumAnalyserVisualisationPanelWidth] = Color.FromNonPremultiplied(192, 192, 192, 255);

                var offset = -(int) (dataPoint * height * (channel == 3 ? 1 : 4));

                while (offset <= 0)
                {
                    if (Math.Abs(offset) % SegmentHeight == 4)
                    {
                        offset++;

                        continue;
                    }

                    _data[_leftPadding + axis + i * (BarWidth + BarSpacing) + x + offset * Constants.SpectrumAnalyserVisualisationPanelWidth] = _palette[-offset];

                    offset++;
                }
            }
        }
    }
}