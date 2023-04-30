using Zen.System.Modules;
using Zen.Z80.Processor;

namespace Zen.System;

public class Motherboard
{
    private readonly Core _core;

    private readonly Interface _interface;

    private readonly State _state;

    private readonly Ram _ram;

    public Motherboard()
    {
        _ram = new();

        _interface = new();

        _state = new();

        _core = new Core(_interface, _state);
    }
}