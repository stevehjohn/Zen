using Zen.System.Infrastructure;
using Zen.System.Modules;
using Zen.Z80.Processor;
using Timer = Zen.System.Modules.Timer;

namespace Zen.System;

public class Motherboard
{
    private const int FramesPerSecond = 60;

    private readonly Model _model;

    private readonly Core _core;

    private readonly Interface _interface;

    private readonly State _state;

    private readonly Ram _ram;

    private readonly Ports _ports;

    private readonly Timer _timer;

    public Ram Ram => _ram;

    public Interface Interface => _interface;

    public Model Model => _model;

    public State State => _state;

    public bool Fast
    {
        get => _timer.Fast;
        set => _timer.Fast = value;
    }

    public Motherboard(Model model)
    {
        _model = model;

        _interface = new()
                     {
                         ReadRam = ReadRam,
                         WriteRam = WriteRam,
                         ReadPort = ReadPort,
                         WritePort = WritePort
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

            case Model.Spectrum128:
                data = File.ReadAllBytes("../../../../../ROM Images/ZX Spectrum 128/image-0.rom");

                _ram.LoadRom(data, 0);

                break;

            default:
                // TODO: Proper exception?
                throw new Exception($"ROM not found for {model}");
        }

        _ports = new();

        _timer = new(FramesPerSecond)
                 {
                     HandleRefreshInterrupt = HandleRefreshInterrupt,
                     OnTick = OnTick,
                     FrameFinished = FrameFinished
                 };
    }

    private byte ReadRam(ushort address)
    {
        return _ram[address];
    }

    private void WriteRam(ushort address, byte data)
    {
        _ram[address] = data;
    }

    private byte ReadPort(ushort port)
    {
        return _ports[port];
    }

    private void WritePort(ushort port, byte data)
    {
        _ports[port] = data;
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

    private int OnTick()
    {
        _core.ExecuteCycle();

        return (int) _state.ClockCycles;
    }

    private void HandleRefreshInterrupt()
    {
        _interface.Interrupt = true;
    }

    private void FrameFinished()
    {
        _ram.FrameReady();
    }
}