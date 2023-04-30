using System.Globalization;

namespace Zen.Z80.Tests.Fuse.Models;

// ReSharper disable InconsistentNaming

public class ProcessorState
{
    public ushort AF { get; }

    public ushort BC { get; }

    public ushort DE { get; }

    public ushort HL { get; }

    public ushort AF_ { get; }

    public ushort BC_ { get; }

    public ushort DE_ { get; }

    public ushort HL_ { get; }

    public ushort IX { get; }

    public ushort IY { get; }

    public ushort SP { get; }

    public ushort PC { get; }

    public byte I { get; }

    public byte R { get; }

    public bool IFF1 { get; }

    public bool IFF2 { get; }

    public byte InterruptMode { get; }

    public bool Halted { get; }

    public int TStates { get; }

    public ProcessorState(string[] lines)
    {
        var registers = lines[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        AF = ushort.Parse(registers[0], NumberStyles.HexNumber);

        BC = ushort.Parse(registers[1], NumberStyles.HexNumber);

        DE = ushort.Parse(registers[2], NumberStyles.HexNumber);

        HL = ushort.Parse(registers[3], NumberStyles.HexNumber);

        AF_ = ushort.Parse(registers[4], NumberStyles.HexNumber);

        BC_ = ushort.Parse(registers[5], NumberStyles.HexNumber);

        DE_ = ushort.Parse(registers[6], NumberStyles.HexNumber);

        HL_ = ushort.Parse(registers[7], NumberStyles.HexNumber);

        IX = ushort.Parse(registers[8], NumberStyles.HexNumber);

        IY = ushort.Parse(registers[9], NumberStyles.HexNumber);

        SP = ushort.Parse(registers[10], NumberStyles.HexNumber);

        PC = ushort.Parse(registers[11], NumberStyles.HexNumber);

        var interrupts = lines[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        I = byte.Parse(interrupts[0], NumberStyles.HexNumber);

        R = byte.Parse(interrupts[1], NumberStyles.HexNumber);

        IFF1 = interrupts[2] == "1";

        IFF2 = interrupts[3] == "1";

        InterruptMode = byte.Parse(interrupts[4]);

        Halted = interrupts[5] == "1";

        TStates = int.Parse(interrupts[6]);
    }
}