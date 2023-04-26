namespace Zen.Z80.Processor;

public enum Flag
{
    Carry          = 0b0000_0001,
    AddSubtract    = 0b0000_0010,
    ParityOverflow = 0b0000_0100,
    X1             = 0b0000_1000,
    HalfCarry      = 0b0001_0000,
    X2             = 0b0010_0000,
    Zero           = 0b0100_0000,
    Sign           = 0b1000_0000
}