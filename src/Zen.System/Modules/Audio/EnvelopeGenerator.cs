namespace Zen.System.Modules.Audio;

public class EnvelopeGenerator
{
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

    private int _period;

    private void RecalculateParameters()
    {
    }
}