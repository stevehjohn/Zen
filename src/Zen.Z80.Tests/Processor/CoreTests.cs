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

        _interface.Data = 0x01;

        _core.ExecuteCycle();

        Assert.Equal(0x1234, _state[RegisterPair.BC]);
    }
}