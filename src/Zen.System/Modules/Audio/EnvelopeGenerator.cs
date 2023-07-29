namespace Zen.System.Modules.Audio;

public class EnvelopeGenerator
{
    private const byte MaxGain = 15;

    public byte FinePeriod
    {
        set
        {
            _period = (_period & 0xFF00) | value;

            RecalculateParameters();
        }
    }

    public byte CoarsePeriod
    {
        set
        {
            _period = (_period & 0x00FF) | (value << 8);

            RecalculateParameters();
        }
    }

    public byte Properties
    {
        set
        {
            _properties = value;

            var attack = (value & 0b0100) > 0;

            _gain = attack ? 0 : MaxGain;

            _direction = attack ? 1 : -1;
        }
    }

    private int _period;

    private byte _properties;

    private int _gain;

    private int _direction;

    private int _position;

    private int _stepChange;

    public byte GetNextValue()
    {
        var result = _gain;

        _position++;

        if (_position > _stepChange)
        {
            _gain += _direction;

            _position = 0;

            if (_gain < 0 || _gain > MaxGain)
            {
                CycleComplete();
            }
        }

        return (byte) result;
    }

    private void RecalculateParameters()
    {
        _position = 0;

        var period = 256 * _period / Constants.AyFrequency;

        _stepChange = period * Constants.SampleRate / MaxGain;
    }

    private void CycleComplete()
    {
        switch (_properties)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 9:
                if (_gain < 0)
                {
                    _gain = 0;
                    _direction = 0;
                }

                break;

            case 4:
            case 5:
            case 6:
            case 7:
            case 15:
                if (_gain > MaxGain)
                {
                    _gain = 0;
                    _direction = 0;
                }

                break;

            case 8:
                if (_gain < 0)
                {
                    _gain = MaxGain;
                    _direction = -1;
                }

                break;

            case 10:
            case 14:
                if (_gain < 0)
                {
                    _gain = 0;
                    _direction = 1;
                }
                else if (_gain > MaxGain)
                {
                    _gain = MaxGain;
                    _direction = -1;
                }

                break;

            case 11:
                if (_gain < 0)
                {
                    _gain = MaxGain;
                    _direction = 0;
                }

                break;

            case 12:
                if (_gain > MaxGain)
                {
                    _gain = 0;
                    _direction = 1;
                }

                break;

            case 13:
                if (_gain > MaxGain)
                {
                    _gain = MaxGain;
                    _direction = 0;
                }

                break;
        }
    }
}