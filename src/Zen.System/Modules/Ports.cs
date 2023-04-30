namespace Zen.System.Modules;

public class Ports
{
    private byte _data = 0xFF;

    public byte this[ushort port]
    {
        get => _data;
        set => _data = value;
    }
}