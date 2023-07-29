namespace Zen.System.Modules.Audio;

public class ToneGenerator
{
    public ushort FinePeriod 
    {
        set
        {
            _period = (_period & 0x0F00) | value;

            RecalculateParameters();
        }
    }

    public ushort CoarsePeriod 
    {
        set
        {
            _period = (_period & 0x00FF) | (value << 8);

            RecalculateParameters();
        }
    }

    private int _period;

    private float _increment;

    private float _position;

    private bool _value;

    public bool GetNextSignal()
    {
        _position += _increment;

        if (_position > 0.5)
        {
            _position = 0;

            _value = ! _value;
        }

        return _value;
    }

    private void RecalculateParameters()
    {
        var frequency = Constants.AyFrequency / (16f * _period);

        _increment = frequency / Constants.SampleRate;
    }
}