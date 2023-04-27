﻿// ReSharper disable IdentifierTypo
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

    public TransferType TransferType { get; set; }

    public Action<Interface>? AddressChanged { private get; set; }

    public void WriteToMemory(ushort address, byte data)
    {
        Mreq = true;

        TransferType = TransferType.Write;

        Data = data;

        Address = address;
    }

    public byte ReadFromMemory(ushort address)
    {
        Mreq = true;

        TransferType = TransferType.Read;

        Address = address;

        return Data;
    }
}