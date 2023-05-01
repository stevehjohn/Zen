namespace Zen.System.Modules;

public class Ports
{
    private readonly Dictionary<int, byte> _data = new();

    public byte this[ushort port]
    {
        get 
        {
            if (! _data.ContainsKey(port))
            {
                return 0xFF;
            }

            return _data[port];
        }

        set => _data[port] = value;
    }
}