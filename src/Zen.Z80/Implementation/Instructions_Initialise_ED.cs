// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseEDInstructions()
    {
        InitialiseEDLoadInstructions();
    }

    private void InitialiseEDLoadInstructions()
    {
        _instructions.Add(0xED43, new Instruction(p => LD_ann_RR(p, RegisterPair.BC), "LD (nn), BC", 0xED43, 2, 4));

        _instructions.Add(0xED47, new Instruction(_ => LD_R_R(Register.I, Register.A), "LD I, A", 0xED47, 0));

        _instructions.Add(0xED4B, new Instruction(p => LD_RR_ann(RegisterPair.BC, p), "LD BC, (nn)", 0xED4B, 2, 4));

        _instructions.Add(0xED4F, new Instruction(_ => LD_R_R(Register.R, Register.A), "LD R, A", 0xED4F, 0));

        _instructions.Add(0xED53, new Instruction(p => LD_ann_RR(p, RegisterPair.DE), "LD (nn), DE", 0xED53, 2, 4));

        _instructions.Add(0xED57, new Instruction(_ => LD_R_R(Register.A, Register.I), "LD A, I", 0xED57, 0));

        _instructions.Add(0xED5B, new Instruction(p => LD_RR_ann(RegisterPair.DE, p), "LD DE, (nn)", 0xED5B, 2, 4));

        _instructions.Add(0xED5F, new Instruction(_ => LD_R_R(Register.A, Register.R), "LD A, R", 0xED5F, 0));

        _instructions.Add(0xED63, new Instruction(p => LD_ann_RR(p, RegisterPair.HL), "LD (nn), HL", 0xED63, 2, 4));

        _instructions.Add(0xED6B, new Instruction(p => LD_RR_ann(RegisterPair.HL, p), "LD HL, (nn)", 0xED6B, 2, 4));

        _instructions.Add(0xED73, new Instruction(p => LD_ann_RR(p, RegisterPair.SP), "LD (nn), SP", 0xED73, 2, 4));

        _instructions.Add(0xED7B, new Instruction(p => LD_RR_ann(RegisterPair.SP, p), "LD SP, (nn)", 0xED7B, 2, 4));
    }
}