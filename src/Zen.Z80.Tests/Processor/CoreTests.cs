using Zen.Z80.Processor;

#pragma warning disable CS8509

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

        _interface.AddressChanged = i =>
        {
            i.Data = i.Address switch
            {
                0x0100 => 0x01,
                0x0101 => 0x34,
                0x0102 => 0x12
            };
        };

        _core.ExecuteCycle();

        Assert.Equal(0x1234, _state[RegisterPair.BC]);
    }

    [Fact]
    public void Core_can_execute_extended_instruction()
    {
        _state[Register.B] = 0x0001;

        _interface.Data = 0xCB;

        _core.ExecuteCycle();

        _interface.Data = 0x00;
        
        _core.ExecuteCycle();

        Assert.Equal(0x02, _state[Register.B]);
    }

    [Fact]
    public void Core_can_execute_double_extended_instruction()
    {
        _state[RegisterPair.IX] = 0x0200;

        _state.ProgramCounter = 0x0100;

        _interface.AddressChanged = i =>
        {
            i.Data = i.Address switch
            {
                0x0100 => 0xDD,
                0x0101 => 0xCB,
                0x0102 => 0x10,
                0x0103 => 0x40,
                0x0210 => 0x01
            };
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

        _interface.Data = 0x00;

        core.ExecuteCycle();

        Assert.Equal(0x0100, _interface.Address);
    }

    [Fact]
    public void Core_puts_program_counter_on_address_bus_before_getting_instruction()
    {
        // TODO
        _state[RegisterPair.BC] = 0x0000;

        _state.ProgramCounter = 0x0100;

        _interface.AddressChanged = i =>
        {
            i.Data = i.Address switch
            {
                0x0100 => 0x01,
                0x0101 => 0x34,
                0x0102 => 0x12
            };
        };

        _core.ExecuteCycle();

        Assert.Equal(0x1234, _state[RegisterPair.BC]);
    }

    [Fact]
    public void Core_increments_program_counter_after_fetching_instruction()
    {
        var core = new Core(_interface, _state);

        _state.ProgramCounter = 0x0100;

        _interface.Data = 0x00;

        core.ExecuteCycle();

        Assert.Equal(0x0101, _state.ProgramCounter);
    }
}