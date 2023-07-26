namespace Zen.System.Modules.Audio;

public class SignalGenerator
{
    public ushort Period { get; set; }

    private readonly bool _noise;

    private float _phaseAngle;

    private float _noiseValue;

    private float _lastValue;

    private float _delta;

    private int _valueCycles;

    public SignalGenerator(bool noise)
    {
        _noise = noise;

        _noiseValue = Lfsr.Value;
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
            _noiseValue = Lfsr.Value * 2 - 1;

            _phaseAngle -= (float) (Math.PI * 2);
        }

        var signal = _noise ? _noiseValue : (float) Math.Sin(_phaseAngle);

        signal = SquareWave(signal);

        return signal;
    }

    private float SquareWave(float sineValue)
    {
        if (sineValue > 0)
        {
            if (_lastValue <= 0)
            {
                _lastValue = 1;

                _delta = 0.05f;
            }
            else
            {
                _lastValue -= _delta;

                _delta /= 1.2f;
            }

            return _lastValue;
        }

        if (sineValue < 0)
        {
            if (_lastValue >= 0)
            {
                _lastValue = -1;

                _delta = 0.05f;
            }
            else
            {
                _lastValue += _delta;

                _delta /= 1.2f;
            }

            return _lastValue;
        }

        return 0;
    }

    private float GetValueForCycle(float sineValue)
    {
        var value = _valueCycles switch
        {
            >= 0 and < 5 => 1f,
            >= 5 and < 10 => 0.95f,
            >= 10 and < 20 => 0.9f,
            >= 20 and < 40 => 0.85f,
            _ => 0.875f
        };

        _valueCycles++;

        return sineValue < 0 ? -value : value;
    }
}