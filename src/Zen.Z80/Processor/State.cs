﻿using Zen.Z80.Implementation;

namespace Zen.Z80.Processor;

public class State
{
    private const int RegisterCount = 26;

    private readonly byte[] _registers = new byte[RegisterCount];

    private readonly byte[] _lastMCycles = new byte[7];

    public ushort ProgramCounter
    {
        get => this[RegisterPair.PC];
        set => this[RegisterPair.PC] = value;
    }

    public ushort StackPointer 
    {
        get => this[RegisterPair.SP];
        set => this[RegisterPair.SP] = value;
    }

    public InterruptMode InterruptMode { get; set; }

    public bool InterruptFlipFlop1 { get; set; }

    public bool InterruptFlipFlop2 { get; set; }

    public bool IgnoreNextInterrupt { get; set; }

    public bool Halted { get; set; }

    public ushort MemPtr { get; set; }
    
    public byte Q { get; set; }

    public ushort InstructionPrefix { get; set; }

    public ulong ClockCycles { get; set; }

    public Instruction? LastInstruction { get; set; }

    public State()
    {
        this[Register.A] = this[Register.A_] = 0xFF;

        this[Register.F] = this[Register.F_] = 0xFF;

        this[Register.I] = this[Register.R] = 0;

        this[Register.B] = this[Register.C] = this[Register.D] = this[Register.E] = this[Register.H] = this[Register.L] = 0x00;

        this[Register.B_] = this[Register.C_] = this[Register.D_] = this[Register.E_] = this[Register.H_] = this[Register.L_] = 0x00;

        ProgramCounter = 0x0000;

        StackPointer = 0xFFFF;

        InterruptMode = InterruptMode.IM0;

        InterruptFlipFlop1 = false;

        InterruptFlipFlop2 = false;

        Halted = false;

        InstructionPrefix = 0;

        MemPtr = 0;

        Q = 0;
    }

    public bool this[Flag flag]
    {
        get => (this[Register.F] & (byte) flag) > 0;
        set
        {
            if (value)
            {
                this[Register.F] |= (byte) flag;
            }
            else
            {
                this[Register.F] &= (byte) ~(byte) flag;
            }

            Q = this[Register.F];
        }
    }

    public byte this[Register register]
    {
        get => _registers[(byte) register];
        set => _registers[(byte) register] = value;
    }

    public ushort this[RegisterPair registerPair]
    {
        get
        {
            var positions = (int) registerPair;

            return (ushort) ((_registers[(positions & 0xFF00) >> 8] << 8) | _registers[positions & 0x00FF]);
        }
        set
        {
            var positions = (int) registerPair;

            _registers[(positions & 0xFF00) >> 8] = (byte) ((value & 0xFF00) >> 8);
            _registers[positions & 0x00FF] = (byte) (value & 0x00FF);
        }
    }

    public void LoadRegisterPair(RegisterPair registerPair, byte[] data)
    {
        var positions = (int) registerPair;

        _registers[(positions & 0xFF00) >> 8] = data[1];
        _registers[positions & 0x00FF] = data[0];
    }

    public void SetMCycles(byte m1)
    {
        _lastMCycles[0] = LastInstruction?.ExtraCycles ?? 0;
        _lastMCycles[1] = m1;
        _lastMCycles[2] = 0;
        _lastMCycles[3] = 0;
        _lastMCycles[4] = 0;
        _lastMCycles[5] = 0;
        _lastMCycles[6] = 0;

        ClockCycles = (ulong) _lastMCycles[0] + m1;
    }

    public void SetMCycles(byte m1, byte m2)
    {
        _lastMCycles[0] = LastInstruction?.ExtraCycles ?? 0;
        _lastMCycles[1] = m1;
        _lastMCycles[2] = m2;
        _lastMCycles[3] = 0;
        _lastMCycles[4] = 0;
        _lastMCycles[5] = 0;
        _lastMCycles[6] = 0;

        ClockCycles = (ulong) _lastMCycles[0] +  m1 + m2;
    }

    public void SetMCycles(byte m1, byte m2, byte m3)
    {
        _lastMCycles[0] = LastInstruction?.ExtraCycles ?? 0;
        _lastMCycles[1] = m1;
        _lastMCycles[2] = m2;
        _lastMCycles[3] = m3;
        _lastMCycles[4] = 0;
        _lastMCycles[5] = 0;
        _lastMCycles[6] = 0;

        ClockCycles = (ulong) _lastMCycles[0] +  m1 + m2 + m3;
    }

    public void SetMCycles(byte m1, byte m2, byte m3, byte m4)
    {
        _lastMCycles[0] = LastInstruction?.ExtraCycles ?? 0;
        _lastMCycles[1] = m1;
        _lastMCycles[2] = m2;
        _lastMCycles[3] = m3;
        _lastMCycles[4] = m4;
        _lastMCycles[5] = 0;
        _lastMCycles[6] = 0;

        ClockCycles = (ulong) _lastMCycles[0] +  m1 + m2 + m3 + m4;
    }

    public void SetMCycles(byte m1, byte m2, byte m3, byte m4, byte m5)
    {
        _lastMCycles[0] = LastInstruction?.ExtraCycles ?? 0;
        _lastMCycles[1] = m1;
        _lastMCycles[2] = m2;
        _lastMCycles[3] = m3;
        _lastMCycles[4] = m4;
        _lastMCycles[5] = m5;
        _lastMCycles[6] = 0;

        ClockCycles = (ulong) _lastMCycles[0] +  m1 + m2 + m3 + m4 + m5;
    }

    public void SetMCycles(byte m1, byte m2, byte m3, byte m4, byte m5, byte m6)
    {
        _lastMCycles[0] = LastInstruction?.ExtraCycles ?? 0;
        _lastMCycles[1] = m1;
        _lastMCycles[2] = m2;
        _lastMCycles[3] = m3;
        _lastMCycles[4] = m4;
        _lastMCycles[5] = m5;
        _lastMCycles[6] = m6;

        ClockCycles = (ulong) _lastMCycles[0] +  m1 + m2 + m3 + m4 + m5 + m6;
    }

    public void Reset()
    {
        // TODO: The rest...
        InstructionPrefix = 0;

        LastInstruction = null;
    }
}