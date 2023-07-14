namespace Zen.System.Modules.Audio;

public class Channel
{
    public ushort TonePeriod
    {
        set => _toneGenerator.Period = value;
        get => _toneGenerator.Period;
    }

    public byte Volume
    {
        set
        {
            _toneGenerator.Volume = value;
            _noiseGenerator.Volume = value;
        }
    }

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

        return signal;
    }
}