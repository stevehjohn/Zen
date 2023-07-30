namespace Zen.System.Modules.Audio;

public class NoiseGenerator
{
    public byte Period
    {
        set 
        {
            if (value == 0)
            {
                _increment = 0;

                return;
            }

            var frequency = Constants.AyFrequency / (16f * (value & 0b0001_1111));

            _increment = frequency / Constants.SampleRate;
        }
    }

    private float _increment;

    private float _position;

    private bool _value;

    public bool GetNextSignal()
    {
         _position += _increment;

        if (_position > 1)
        {
            _position -= 1f;

            _value = Lfsr.GetNextValue();
        }
        else
        {
            Lfsr.GetNextValue();
        }

        return _value;
   }
}