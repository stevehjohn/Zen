using Zen.Common;

namespace Zen.System.Modules;

public class Beeper
{
    private bool _bit3State;

    private bool _bit4State;

    private ulong? _lastCycle;

    public void UlaAddressed(byte value, ulong cycle)
    {
        var amplitude = 0;

        var bit3State = (value & 0b0000_1000) > 0;

        if (_bit3State != bit3State)
        {
            amplitude = 1;

            _bit3State = bit3State;
        }

        var bit4State = (value & 0b0000_1000) > 0;

        if (_bit4State != bit4State)
        {
            amplitude = 2;

            _bit4State = bit4State;
        }

        var frequency = 0f;

        if (_lastCycle != null)
        {
            // So, frequency seems to be 2x the expected value. Why?
            frequency = Constants.TStatesPerSecond / (float) (cycle - _lastCycle);
        }

        _lastCycle = cycle;
    }
}