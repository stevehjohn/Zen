using Zen.Z80.Processor;

namespace Zen.Z80.Tests.Processor;

public class CoreTests
{
    private readonly Interface _interface;

    private readonly State _state;

    private readonly Core _core;

    public CoreTests()
    {
        _interface = new();

        _state = new();

        _core = new Core(_interface, _state);
    }

    [Fact]
    public void Core_can_execute_simple_instruction()
    {
        _state[RegisterPair.BC] = 0x0000;

        _state.ProgramCounter = 0x0100;

        _interface.StateChanged = () =>
        {
            if (_interface.MREQ && _interface.RD)
            {
                switch (_interface.Address)
                {
                    case 0x0100:
                        _interface.Data = 0x01;
                        break;
                    case 0x0101:
                        _interface.Data = 0x34;
                        break;
                    case 0x102:
                        _interface.Data = 0x12;
                        break;
                }
            }
        };

        _core.ExecuteCycle();

        Assert.Equal(0x1234, _state[RegisterPair.BC]);
    }

    [Fact]
    public void Core_can_execute_extended_instruction()
    {
        _state[Register.B] = 0x0001;

        _state.ProgramCounter = 0x0100;

        _interface.StateChanged = () =>
        {
            if (_interface.MREQ && _interface.RD)
            {
                switch (_interface.Address)
                {
                    case 0x0100:
                        _interface.Data = 0xCB;
                        break;
                    case 0x0101:
                        _interface.Data = 0x40;
                        break;
                }
            }
        };

        _core.ExecuteCycle();

        _core.ExecuteCycle();

        Assert.False(_state[Flag.Zero]);
    }

    [Fact]
    public void Core_can_execute_double_extended_instruction()
    {
        _state[RegisterPair.IX] = 0x0200;

        _state.ProgramCounter = 0x0100;
        
        _interface.StateChanged = () =>
        {
            if (_interface.MREQ && _interface.RD)
            {
                switch (_interface.Address)
                {
                    case 0x0100:
                        _interface.Data = 0xDD;
                        break;
                    case 0x0101:
                        _interface.Data = 0xCB;
                        break;
                    case 0x0102:
                        _interface.Data = 0x10;
                        break;
                    case 0x0103:
                        _interface.Data = 0x40;
                        break;
                    case 0x0210:
                        _interface.Data = 0x01;
                        break;
                }
            }
        };

        _core.ExecuteCycle();

        _core.ExecuteCycle();

        Assert.False(_state[Flag.Zero]);
    }

    [Fact]
    public void Core_puts_program_counter_on_address_bus_to_get_instruction()
    {
        var core = new Core(_interface, _state);

        _state.ProgramCounter = 0x0100;

        var requestedAddress = 0;

        _interface.StateChanged = () =>
        {
            if (_interface.MREQ && _interface.RD)
            {
                switch (_interface.Address)
                {
                    case 0x0100:
                        requestedAddress = _interface.Address;
                        _interface.Data = 0x00;
                        break;
                }
            }
        };

        core.ExecuteCycle();

        Assert.Equal(0x0100, requestedAddress);
    }

    [Fact]
    public void Core_puts_program_counter_on_address_bus_before_getting_instruction()
    {
        // TODO
        _state[RegisterPair.BC] = 0x0000;

        _state.ProgramCounter = 0x0100;
                
        _interface.StateChanged = () =>
        {
            if (_interface.MREQ && _interface.RD)
            {
                switch (_interface.Address)
                {
                    case 0x0100:
                        _interface.Data = 0x01;
                        break;
                    case 0x0101:
                        _interface.Data = 0x34;
                        break;
                    case 0x0102:
                        _interface.Data = 0x12;
                        break;
                }
            }
        };

        _core.ExecuteCycle();

        Assert.Equal(0x1234, _state[RegisterPair.BC]);
    }

    [Fact]
    public void Core_increments_program_counter_after_fetching_instruction()
    {
        var core = new Core(_interface, _state);

        _state.ProgramCounter = 0x0100;
                        
        _interface.StateChanged = () =>
        {
            if (_interface.MREQ && _interface.RD)
            {
                switch (_interface.Address)
                {
                    case 0x0100:
                        _interface.Data = 0x00;
                        break;
                }
            }
        };

        core.ExecuteCycle();

        Assert.Equal(0x0101, _state.ProgramCounter);
    }
}