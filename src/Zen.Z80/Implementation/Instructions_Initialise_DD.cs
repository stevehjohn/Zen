﻿// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDInstructions()
    {
        _instructions.Add(0xDD00, new Instruction(_ => NOP(), "NOP", 0xDD00, 0, 4));

        _instructions.Add(0xDD01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0xDD01, 2, 4));

        _instructions.Add(0xDD11, new Instruction(d => LD_RR_nn(RegisterPair.DE, d), "LD DE, nn", 0xDD11, 2, 4));

        _instructions.Add(0xDD21, new Instruction(d => LD_RR_nn(RegisterPair.IX, d), "LD IX, nn", 0xDD21, 2, 4));

        _instructions.Add(0xDD40, new Instruction(_ => LD_R_R(Register.B, Register.B), "LD B, B", 0xDD40, 4));

        _instructions.Add(0xDDCB, new Instruction(_ => PREFIX(0xDDCB), "PREFIX 0xDDCB", 0xDDCB, 2));
    }
}