namespace Zen.System.Modules;

public class Ports
{
    private byte _data;

    public byte this[ushort port]
    {
        get => _data;
        set => _data = value;
    }
}