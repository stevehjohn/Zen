// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace Zen.Z80.Processor;

public class Interface
{
    public Action? StateChanged { get; set; }

    public bool MREQ { get; set; }

    public bool IORQ { get; set; }

    public bool RD { get; set; }

    public bool WR { get; set; }

    public bool INT { get; set; }

    public ushort Address
    {
        get => _address;
        set 
        {
            _address = value;

            StateChanged?.Invoke();
        }
    }

    public byte Data { get; set; }

    public Func<ushort, byte>? ReadPort { get; set; }

    public Action<ushort, byte>? WritePort { get; set; }

    private ushort _address;

    public byte ReadFromMemory(ushort address)
    {
        MREQ = true;

        IORQ = false;

        RD = true;

        WR = false;

        Address = address;

        return Data;
    }

    public void WriteToMemory(ushort address, byte data)
    {
        MREQ = true;

        IORQ = false;

        RD = false;

        WR = true;

        Data = data;

        Address = address;
    }
    
    public byte ReadFromPort(ushort address)
    {
        MREQ = false;

        IORQ = true;

        RD = true;

        WR = false;

        Address = address;

        return Data;
    }

    public void WriteToPort(ushort port, byte data)
    {
        MREQ = false;

        IORQ = true;

        RD = true;

        WR = false;

        Data = data;

        Address = port;
    }
}