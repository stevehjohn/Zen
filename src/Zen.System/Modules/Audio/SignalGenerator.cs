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
            _noiseValue = (float) _random.NextDouble();

            _phaseAngle -= (float) (Math.PI * 2);
        }

        var signal = _noise ? _noiseValue : (float) Math.Sin(_phaseAngle);

        if (! EnvelopeOn)
        {
            signal *= Volume / 15f * Constants.Amplitude;
        }
        else
        {
            signal *= _gain;

            CycleEnvelope();
        }

        return signal;
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
}