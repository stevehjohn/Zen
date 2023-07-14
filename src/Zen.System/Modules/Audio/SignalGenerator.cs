namespace Zen.System.Modules.Audio;

public class SignalGenerator
{
    public ushort Period { get; set; }

    public byte Volume { get; set; }

    public ushort EnvelopePeriod
    {
        get => _envelopePeriod;
        set
        {
            _envelopePeriod = value;

            var period = 256f * _envelopePeriod / Constants.AyFrequency;

            _gainStep = Constants.Amplitude / (period * Constants.SampleRate);
        }
    }

    public byte Envelope
    {
        get => _envelope;
        set
        {
            _envelope = value;

            switch (value)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 9:
                    _gain = 1;
                    _gainDirection = -1;

                    break;

                case 4:
                case 5:
                case 6:
                case 7:
                case 15:
                    _gain = 0;
                    _gainDirection = 1;

                    break;

                case 8:
                    _gain = 1;
                    _gainDirection = -1;

                    break;

                case 10:
                    _gain = 1;
                    _gainDirection = -1;

                    break;

                case 11:
                    _gain = 1;
                    _gainDirection = -1;

                    break;

                case 12:
                    _gain = 0;
                    _gainDirection = 1;

                    break;

                case 13:
                    _gain = 0;
                    _gainDirection = 1;

                    break;

                case 14:
                    _gain = 0;
                    _gainDirection = 1;

                    break;
            }
        }
    }

    public bool EnvelopeOn { get; set; }

    private readonly bool _noise;

    private float _phaseAngle;

    private readonly Random _random = new();

    private float _noiseValue;

    private byte _envelope;

    private ushort _envelopePeriod;

    private float _gain;

    private float _gainDirection;

    private float _gainStep;

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
            _noiseValue = (float) _random.NextDouble() * 2 - 1;

            _phaseAngle -= (float) (Math.PI * 2);
        }

        var signal = _noise ? SquareWave(_noiseValue) : SquareWave((float) Math.Sin(_phaseAngle));

        if (! EnvelopeOn)
        {
            signal *= NormaliseVolume(Volume) * Constants.Amplitude;
        }
        else
        {
            signal *= _gain;

            CycleEnvelope();
        }

        return signal;
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
    }

    private void CycleEnvelope()
    {
        _gain += _gainDirection * _gainStep;

        switch (_envelope)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 9:
                if (_gain < 0)
                {
                    _gain = 0;
                    _gainDirection = 0;
                }

                break;

            case 4:
            case 5:
            case 6:
            case 7:
            case 15:
                if (_gain > 1)
                {
                    _gain = 0;
                    _gainDirection = 0;
                }

                break;

            case 8:
                if (_gain < 0)
                {
                    _gain = 1;
                    _gainDirection = -1;
                }

                break;

            case 10:
            case 14:
                if (_gain < 0)
                {
                    _gain = 0;
                    _gainDirection = 1;
                }
                else if (_gain > 1)
                {
                    _gain = 1;
                    _gainDirection = -1;
                }

                break;

            case 11:
                if (_gain < 0)
                {
                    _gain = 1;
                    _gainDirection = 0;
                }

                break;

            case 12:
                if (_gain > 1)
                {
                    _gain = 0;
                    _gainDirection = 1;
                }

                break;

            case 13:
                if (_gain > 1)
                {
                    _gain = 1;
                    _gainDirection = 0;
                }

                break;
        }
    }

    private float SquareWave(float sineValue)
    {
        return sineValue > 0 ? 1 : 0;
    }
}