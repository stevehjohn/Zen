using Zen.System.Infrastructure;
using Zen.System.Modules;
using Zen.Z80.Processor;

namespace Zen.System;

public class Motherboard
{
    private readonly Model _model;

    private readonly Core _core;

    private readonly Interface _interface;

    private readonly State _state;

    private readonly Ram _ram;

    private readonly Ports _ports;

    public Motherboard(Model model)
    {
        _model = model;

        _interface = new()
                     {
                         AddressChanged = AddressChanged
                     };

        _state = new();

        _core = new Core(_interface, _state);

        _ram = new();

        _ports = new();
    }

    private void AddressChanged()
    {
        if (_interface.Mreq)
        {
            MemoryRequest();
        }

        if (_interface.Iorq)
        {
            PortRequest();
        }
    }

    private void MemoryRequest()
    {
        if (_interface.TransferType == TransferType.Read)
        {
            _interface.Data = _ram[_interface.Address];
        }
        else
        {
            _ram[_interface.Address] = _interface.Data;
        }
    }

    private void PortRequest()
    {
        if (_interface.TransferType == TransferType.Read)
        {
            _interface.Data = _ports[_interface.Address];
        }
        else
        {
            _ports[_interface.Address] = _interface.Data;
        }
    }
}