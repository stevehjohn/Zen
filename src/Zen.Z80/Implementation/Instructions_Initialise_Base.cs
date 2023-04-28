﻿// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseBaseInstructions()
    {
        _instructions.Add(0x00, new Instruction(_ => NOP(), "NOP", 0x00, 0));

        InitialiseLDByteInstructions();

        InitialiseArithmeticLogicInstructions();

        InitialiseRETInstructions();

        InitialiseStackInstructions();

        InitialiseIncDecInstructions();

        _instructions.Add(0x01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0x01, 2));

        _instructions.Add(0xCB, new Instruction(_ => PREFIX(0xCB), "PREFIX 0xCB", 0xCB, 0));

        _instructions.Add(0xDD, new Instruction(_ => PREFIX(0xDD), "PREFIX 0xDD", 0xDD, 0));

        _instructions.Add(0xFD, new Instruction(_ => PREFIX(0xFD), "PREFIX 0xFD", 0xFD, 0));
    }

    private void InitialiseRETInstructions()
    {
        _instructions.Add(0xC0, new Instruction(_ => RET(Flag.Zero, true), "RET NZ", 0xC0, 0));

        _instructions.Add(0xD0, new Instruction(_ => RET(Flag.Carry, true), "RET NC", 0xD0, 0));

        _instructions.Add(0xE0, new Instruction(_ => RET(Flag.ParityOverflow, true), "RET PO", 0xE0, 0));

        _instructions.Add(0xF0, new Instruction(_ => RET(Flag.Sign, true), "RET NS", 0xF0, 0));

        _instructions.Add(0xC8, new Instruction(_ => RET(Flag.Zero), "RET Z", 0xC8, 0));

        _instructions.Add(0xD8, new Instruction(_ => RET(Flag.Carry), "RET C", 0xD8, 0));

        _instructions.Add(0xE8, new Instruction(_ => RET(Flag.ParityOverflow), "RET PE", 0xE8, 0));

        _instructions.Add(0xF8, new Instruction(_ => RET(Flag.Sign), "RET S", 0xF8, 0));
    }

    private void InitialiseStackInstructions()
    {
        _instructions.Add(0xC5, new Instruction(_ => PUSH(RegisterPair.BC), "PUSH BC", 0xC5, 0));

        _instructions.Add(0xD5, new Instruction(_ => PUSH(RegisterPair.DE), "PUSH DE", 0xD5, 0));

        _instructions.Add(0xE5, new Instruction(_ => PUSH(RegisterPair.HL), "PUSH HL", 0xE5, 0));

        _instructions.Add(0xF5, new Instruction(_ => PUSH(RegisterPair.AF), "PUSH AF", 0xF5, 0));

        _instructions.Add(0xC1, new Instruction(_ => POP(RegisterPair.BC), "POP BC", 0xC1, 0));

        _instructions.Add(0xD1, new Instruction(_ => POP(RegisterPair.DE), "POP DE", 0xD1, 0));

        _instructions.Add(0xE1, new Instruction(_ => POP(RegisterPair.HL), "POP HL", 0xE1, 0));

        _instructions.Add(0xF1, new Instruction(_ => POP(RegisterPair.AF), "POP AF", 0xF1, 0));
    }

    private void InitialiseIncDecInstructions()
    {
        _instructions.Add(0x03, new Instruction(_ => INC_RR(RegisterPair.BC), "INC BC", 0x03, 0));

        _instructions.Add(0x04, new Instruction(_ => INC_R(Register.B), "INC B", 0x04, 0));

        _instructions.Add(0x05, new Instruction(_ => DEC_R(Register.B), "DEC B", 0x05, 0));

        _instructions.Add(0x0B, new Instruction(_ => DEC_RR(RegisterPair.BC), "DEC BC", 0x0B, 0));

        _instructions.Add(0x0C, new Instruction(_ => INC_R(Register.B), "INC C", 0x0C, 0));

        _instructions.Add(0x0D, new Instruction(_ => DEC_R(Register.B), "DEC C", 0x0D, 0));

        _instructions.Add(0x13, new Instruction(_ => INC_RR(RegisterPair.DE), "INC DE", 0x13, 0));

        _instructions.Add(0x14, new Instruction(_ => INC_R(Register.D), "INC D", 0x14, 0));

        _instructions.Add(0x15, new Instruction(_ => DEC_R(Register.D), "DEC D", 0x15, 0));

        _instructions.Add(0x1B, new Instruction(_ => DEC_RR(RegisterPair.DE), "DEC DE", 0x1B, 0));

        _instructions.Add(0x1C, new Instruction(_ => INC_R(Register.E), "INC E", 0x1C, 0));

        _instructions.Add(0x1D, new Instruction(_ => DEC_R(Register.E), "DEC E", 0x1D, 0));

        _instructions.Add(0x23, new Instruction(_ => INC_RR(RegisterPair.HL), "INC HL", 0x23, 0));

        _instructions.Add(0x24, new Instruction(_ => INC_R(Register.H), "INC H", 0x24, 0));

        _instructions.Add(0x25, new Instruction(_ => DEC_R(Register.H), "DEC H", 0x25, 0));

        _instructions.Add(0x2B, new Instruction(_ => DEC_RR(RegisterPair.HL), "DEC HL", 0x2B, 0));

        _instructions.Add(0x2C, new Instruction(_ => INC_R(Register.L), "INC L", 0x2C, 0));

        _instructions.Add(0x2D, new Instruction(_ => DEC_R(Register.L), "DEC L", 0x2D, 0));
    }
}