// ReSharper disable IdentifierTypo
namespace Zen.Z80.Processor;

public class Interface
{
    private ushort _address;

    public ushort Address
    {
        get => _address;
        set
        {
            _address = value;

            AddressChanged?.Invoke(this);
        }
    }

    public byte Data { get; set; }

    public bool Mreq { get; set; }

    public Action<Interface>? AddressChanged { private get; set; }
}