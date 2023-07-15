using System.Diagnostics;

namespace Zen.System.Modules.Audio;

public class Channel
{
    public ushort TonePeriod
    {
        set => _toneGenerator.Period = value;
        get => _toneGenerator.Period;
    }

    public byte Volume { get; set; }

    public ushort EnvelopePeriod
    {
        set
        {
            _toneGenerator.EnvelopePeriod = value;
            _noiseGenerator.EnvelopePeriod = value;
        }
        get => _toneGenerator.EnvelopePeriod;
    }

    public byte Envelope
    {
        set
        {
            _toneGenerator.Envelope = value;
            _noiseGenerator.Envelope = value;
        }
    }

    public bool EnvelopeOn
    {
        set
        {
            _toneGenerator.EnvelopeOn = value;
            _noiseGenerator.EnvelopeOn = value;
        }
    }

    public byte NoisePeriod
    {
        set => _noiseGenerator.Period = value;
    }

    public bool ToneOn { get; set; }

    public bool NoiseOn { get; set; }

    private readonly SignalGenerator _toneGenerator;

    private readonly SignalGenerator _noiseGenerator;

    public Channel()
    {
        _toneGenerator = new SignalGenerator(false);

        _noiseGenerator = new SignalGenerator(true);
    }

    public float GetNextSignal()
    {
        var signal = 0f;
        
        if (ToneOn)
        {
            signal = _toneGenerator.GetNextSignal();
        }

        if (NoiseOn)
        {
            signal += _noiseGenerator.GetNextSignal();
        }

        return signal * NormaliseVolume(Volume) * Constants.Amplitude;
    }

    private static float NormaliseVolume(byte volume)
    {
        var result = volume switch
        {
            1 => 0.0105f,
            2 => 0.0154f,
            3 => 0.0216f,
            4 => 0.0314f,
            5 => 0.0461f,
            6 => 0.0635f,
            7 => 0.1061f,
            8 => 0.1319f,
            9 => 0.2163f,
            10 => 0.2973f,
            11 => 0.3908f,
            12 => 0.5129f,
            13 => 0.6371f,
            14 => 0.8186f,
            15 => 1.0000f,
            _ => 0f
        };

        return result;
    }}