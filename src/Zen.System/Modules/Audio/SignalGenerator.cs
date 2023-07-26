namespace Zen.System.Modules.Audio;

public class SignalGenerator
{
    public ushort Period { get; set; }
    
    private readonly bool _noise;

    private float _phaseAngle;

    private float _noiseValue;

    public SignalGenerator(bool noise)
    {
        _noise = noise;

        _noiseValue = Lsfr.Value;
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
            _noiseValue = Lsfr.Value * 2 - 1;

            _phaseAngle -= (float) (Math.PI * 2);
        }

        var signal = _noise ? _noiseValue : (float) Math.Sin(_phaseAngle);

        signal = SquareWave(signal);

        return signal;
    }

    private static float SquareWave(float sineValue)
    {
        return sineValue > 0 ? 1 : -1;
    }
}