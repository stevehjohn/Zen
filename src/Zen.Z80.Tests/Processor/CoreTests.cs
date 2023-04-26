using Moq;
using Zen.Z80.Processor;

namespace Zen.Z80.Tests.Processor;

public class CoreTests
{
    private readonly Mock<Interface> _interfaceMock;

    private readonly Interface _interface;

    private readonly State _state;

    public CoreTests()
    {
        _interfaceMock = new();

        _interface = new();

        _state = new();

    }

    [Fact]
    public void Core_can_execute_simple_instruction()
    {
        var core = new Core(_interfaceMock.Object, _state);

        _state[RegisterPair.BC] = 0x0000;

        _interfaceMock.SetupGet(i => i.Data).Returns(0x01);

        core.ExecuteCycle();

        Assert.Equal(0x1234, _state[RegisterPair.BC]);
    }

    [Fact]
    public void Core_puts_program_counter_on_address_bus_to_get_instruction()
    {
        var core = new Core(_interface, _state);

        _state.ProgramCounter = 0x0100;

        _interface.Data = 0x01;

        core.ExecuteCycle();

        Assert.Equal(0x0100, _interface.Address);
    }

    [Fact]
    public void Core_increments_program_counter_after_fetching_instruction()
    {
        var core = new Core(_interface, _state);

        _state.ProgramCounter = 0x0100;

        _interface.Data = 0x01;

        core.ExecuteCycle();

        Assert.Equal(0x0101, _state.ProgramCounter);
    }
}