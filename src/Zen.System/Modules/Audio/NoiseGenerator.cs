﻿namespace Zen.System.Modules.Audio;

public class NoiseGenerator
{
    public byte Period
    {
        set
        {
            var frequency = Constants.AyFrequency / 16f;

            value = (byte) (value & 0b0001_1111);

            if (value != 0)
            {
                frequency *= value;
            }

            _increment = frequency / Constants.SampleRate;
        }
    }

    private float _increment;

    private float _position;

    private bool _value;

    public bool GetNextSignal()
    {
        if (float.IsInfinity(_increment))
        {
            return Lfsr.GetNextValue();
        }

        _position += _increment;

        if (_position >= 0.5)
        {
            _position -= 0.5f;

            _value = Lfsr.GetNextValue();
        }
        else
        {
            Lfsr.GetNextValue();
        }

        return _value;
    }
}