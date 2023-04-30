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

            AddressChanged?.Invoke();
        }
    }

    public byte Data { get; set; }

    public bool Mreq { get; set; }

    public bool Iorq { get; set; }

    public TransferType TransferType { get; set; }

    public Action? AddressChanged { private get; set; }

    public void WriteToMemory(ushort address, byte data)
    {
        Mreq = true;

        Iorq = false;

        TransferType = TransferType.Write;

        Data = data;

        Address = address;
    }

    public byte ReadFromMemory(ushort address)
    {
        Mreq = true;

        Iorq = false;

        TransferType = TransferType.Read;

        Address = address;

        return Data;
    }

    public byte ReadFromPort(ushort port)
    {
        Mreq = false;

        Iorq = true;

        TransferType = TransferType.Read;

        Address = port;

        return Data;
    }

    public void WriteToPort(ushort port, byte data)
    {
        Mreq = false;

        Iorq = true;

        TransferType = TransferType.Write;

        Data = data;

        Address = port;
    }
}