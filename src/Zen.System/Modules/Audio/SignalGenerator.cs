namespace Zen.System.Modules.Audio;

public class SignalGenerator
{
    public ushort Period { get; set; }

    public byte Volume { get; set; }

    public ushort EnvelopePeriod { get; set; }

    public byte Envelope { get; set; }

    public bool EnvelopeOn { get; set; }

    private readonly bool _noise;

    private float _phaseAngle;

    private readonly Random _random = new();

    private float _noiseValue;

    public SignalGenerator(bool noise)
    {
        _noise = noise;

        _noiseValue = (float) _random.NextDouble();
    }

    public float GetNextSignal()
    {
        var frequency = Constants.AyFrequency / (16f * Period);

        var increment = (float) (2 * Math.PI * frequency / Constants.SampleRate);

        if (float.IsNaN(increment) || float.IsInfinity(increment))
        {
            increment = 0;
        }

        _phaseAngle += increment;

        if (_phaseAngle > Math.PI * 2)
        {
            _noiseValue = (float) _random.NextDouble();

            _phaseAngle -= (float) (Math.PI * 2);
        }

        var signal = Volume / 15f * Constants.Amplitude * (_noise ? _noiseValue : (float) Math.Sin(_phaseAngle));

        return signal;
    }
}