namespace Zen.System.Modules.Audio;

public class NoiseGenerator
{
    public byte Period
    {
        set => _period = (byte) (value & 0b0001_1111);
    }

    private byte _period;

    public bool GetNextSignal()
    {
        return false;
    }
}