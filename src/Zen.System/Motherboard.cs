using Zen.System.Infrastructure;
using Zen.System.Modules;
using Zen.Z80.Processor;
using Timer = Zen.System.Modules.Timer;

namespace Zen.System;

public class Motherboard
{
    private readonly Model _model;

    private readonly Core _core;

    private readonly Interface _interface;

    private readonly State _state;

    private readonly Ram _ram;

    private readonly Ports _ports;

    private readonly Timer _timer;

    public Ram Ram => _ram;

    public Interface Interface => _interface;

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

        byte[] data;

        switch (model)
        {
            case Model.Spectrum48K:
                data = File.ReadAllBytes("../../../../../ROM Images/ZX Spectrum 48K/image-0.rom");

                _ram.LoadRom(data, 0);

                break;
        }

        _ports = new();

        _timer = new(4_000_000)
                 {
                     HandleRefreshInterrupt = HandleRefreshInterrupt,
                     OnTick = OnTick
                 };
    }

    public void Start()
    {
        _timer.Start();
    }

    public void Pause()
    {
        _timer.Pause();
    }

    public void Resume()
    {
        _timer.Resume();
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

    private int OnTick()
    {
        _core.ExecuteCycle();

        return (int) _state.ClockCycles;
    }

    private void HandleRefreshInterrupt()
    {
        //throw new NotImplementedException();
    }
}