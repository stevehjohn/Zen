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

    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly Complex[][] _buffers;
    
    private readonly Texture2D _spectrum;

    private int _bufferPosition;

    private bool _rendering;

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

        for (var x = 0; x < Constants.SpectrumVisualisationPanelWidth; x++)
        {
            var dataPoint = magnitudes[(int) (x * ((float) BufferSize / Constants.SpectrumVisualisationPanelWidth))] / 1000;

            var offset = -(int) (dataPoint * height * (channel == 3 ? 1 : 4));

            _data[axis + x + offset * Constants.SpectrumVisualisationPanelWidth] = Color.LightGreen;
        }
    }
}