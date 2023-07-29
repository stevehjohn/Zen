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
            _continue = (value & 0b1000) > 0;

            _attack = (value & 0b0100) > 0;

            _alternate = (value & 0b0010) > 0;

            _hold = (value & 0b0001) > 0;

            _gain = _attack ? 0 : MaxGain;

            _direction = _attack ? 1 : -1;
        }
    }

    private int _period;

    private bool _continue;

    private bool _attack;

    private bool _alternate;

    private bool _hold;

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
        if (_hold)
        {
            _direction = 0;
        }

        if (_alternate)
        {
            _direction = -_direction;
        }

        if (_attack)
        {
        }

        if (_continue)
        {
        }
    }
}