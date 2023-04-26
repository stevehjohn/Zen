using Zen.Z80.Processor;

namespace Zen.Z80.Tests.Processor;

public class StateTests
{
    private readonly State _state;

    public StateTests()
    {
        _state = new State();
    }

    [Fact]
    public void LoadRegisterPair_writes_bytes_in_correct_order()
    {
        _state.LoadRegisterPair(RegisterPair.BC, new byte[] { 0x34, 0x12 });

        Assert.Equal(0x12, _state[Register.B]);
        Assert.Equal(0x34, _state[Register.C]);

        Assert.Equal(0x1234, _state[RegisterPair.BC]);
    }

    [Fact]
    public void IndexerSet_writes_byte()
    {
        _state[Register.B] = 0x12;

        Assert.Equal(0x12, _state[Register.B]);
    }

    [Fact]
    public void IndexerSet_writes_bytes_in_correct_order()
    {
        _state[RegisterPair.BC] = 0x1234;

        Assert.Equal(0x12, _state[Register.B]);
        Assert.Equal(0x34, _state[Register.C]);

        Assert.Equal(0x1234, _state[RegisterPair.BC]);
    }
}