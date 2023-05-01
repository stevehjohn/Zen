// ReSharper disable IdentifierTypo

namespace Zen.Z80.Processor;

public class Interface
{
    private ushort _address;

    private readonly Mutex _mutex = new();

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

    public byte ReadFromMemory(ushort address)
    {
        _mutex.WaitOne();

        Mreq = true;

        Iorq = false;

        TransferType = TransferType.Read;

        Address = address;

        var data = Data;

        _mutex.ReleaseMutex();

        return data;
    }

    public void WriteToMemory(ushort address, byte data)
    {
        _mutex.WaitOne();

        Mreq = true;

        Iorq = false;

        TransferType = TransferType.Write;

        Data = data;

        Address = address;

        _mutex.ReleaseMutex();
    }

    public byte ReadFromPort(ushort port)
    {
        _mutex.WaitOne();

        Mreq = false;

        Iorq = true;

        TransferType = TransferType.Read;

        Address = port;

        var data = Data;

        _mutex.ReleaseMutex();

        return data;
    }

    public void WriteToPort(ushort port, byte data)
    {
        _mutex.WaitOne();

        Mreq = false;

        Iorq = true;

        TransferType = TransferType.Write;

        Data = data;

        _mutex.ReleaseMutex();

        Address = port;
    }
}