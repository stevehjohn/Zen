﻿using Zen.Z80.Processor;

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

    [Theory]
    [InlineData(0b0000_0001, Flag.Carry)]
    [InlineData(0b0000_0010, Flag.AddSubtract)]
    [InlineData(0b0000_0100, Flag.ParityOverflow)]
    [InlineData(0b0000_1000, Flag.X1)]
    [InlineData(0b0001_0000, Flag.HalfCarry)]
    [InlineData(0b0010_0000, Flag.X2)]
    [InlineData(0b0100_0000, Flag.Zero)]
    [InlineData(0b1000_0000, Flag.Sign)]
    public void Flags_are_mapped_correctly(byte registerF, Flag flag)
    {
        _state[Register.F] = registerF;

        Assert.True(_state[flag]);
    }

    [Theory]
    [InlineData(Flag.Carry, 0b0000_0001)]
    [InlineData(Flag.AddSubtract, 0b0000_0010)]
    [InlineData(Flag.ParityOverflow, 0b0000_0100)]
    [InlineData(Flag.X1, 0b0000_1000)]
    [InlineData(Flag.HalfCarry, 0b0001_0000)]
    [InlineData(Flag.X2, 0b0010_0000)]
    [InlineData(Flag.Zero, 0b0100_0000)]
    [InlineData(Flag.Sign, 0b1000_0000)]
    public void SetFlag_true_sets_correct_bit(Flag flag, byte expected)
    {
        _state[Register.F] = 0;

        _state[flag] = true;

        Assert.Equal(expected, _state[Register.F]);
    }

    [Theory]
    [InlineData(Flag.Carry, 0b1111_1110)]
    [InlineData(Flag.AddSubtract, 0b1111_1101)]
    [InlineData(Flag.ParityOverflow, 0b1111_1011)]
    [InlineData(Flag.X1, 0b1111_0111)]
    [InlineData(Flag.HalfCarry, 0b1110_1111)]
    [InlineData(Flag.X2, 0b1101_1111)]
    [InlineData(Flag.Zero, 0b1011_1111)]
    [InlineData(Flag.Sign, 0b0111_1111)]
    public void SetFlag_false_resets_correct_bit(Flag flag, byte expected)
    {
        _state[Register.F] = 0xFF;

        _state[flag] = false;

        Assert.Equal(expected, _state[Register.F]);
    }
}