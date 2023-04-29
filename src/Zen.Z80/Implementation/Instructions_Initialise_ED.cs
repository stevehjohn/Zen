﻿// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

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
        _instructions.Add(0xED42, new Instruction(_ => SBC_RR_RR(RegisterPair.HL, RegisterPair.BC), "SBC HL, BC", 0xED42, 0));

        _instructions.Add(0xED43, new Instruction(p => LD_ann_RR(p, RegisterPair.BC), "LD (nn), BC", 0xED43, 2, 4));

        _instructions.Add(0xED44, new Instruction(_ => NEG(), "NEG A", 0xED44, 0));

        _instructions.Add(0xED45, new Instruction(_ => RETN(), "RETN", 0xED45, 0));

        _instructions.Add(0xED46, new Instruction(_ => IM(InterruptMode.IM0), "IM 0", 0xED46, 0));

        _instructions.Add(0xED47, new Instruction(_ => LD_R_R(Register.I, Register.A), "LD I, A", 0xED47, 0));

        _instructions.Add(0xED4A, new Instruction(_ => ADC_RR_RR(RegisterPair.HL, RegisterPair.BC), "ADC HL, BC", 0xED4A, 0));

        _instructions.Add(0xED4B, new Instruction(p => LD_RR_ann(RegisterPair.BC, p), "LD BC, (nn)", 0xED4B, 2, 4));
        
        _instructions.Add(0xED4D, new Instruction(_ => RETI(), "RETI", 0xED4D, 0));

        _instructions.Add(0xED4F, new Instruction(_ => LD_R_R(Register.R, Register.A), "LD R, A", 0xED4F, 0));

        _instructions.Add(0xED52, new Instruction(_ => SBC_RR_RR(RegisterPair.HL, RegisterPair.DE), "SBC HL, DE", 0xED52, 0));

        _instructions.Add(0xED53, new Instruction(p => LD_ann_RR(p, RegisterPair.DE), "LD (nn), DE", 0xED53, 2, 4));

        _instructions.Add(0xED56, new Instruction(_ => IM(InterruptMode.IM1), "IM 1", 0xED56, 0));

        _instructions.Add(0xED57, new Instruction(_ => LD_R_R(Register.A, Register.I), "LD A, I", 0xED57, 0));

        _instructions.Add(0xED5A, new Instruction(_ => ADC_RR_RR(RegisterPair.HL, RegisterPair.DE), "ADC HL, DE", 0xED5A, 0));

        _instructions.Add(0xED5B, new Instruction(p => LD_RR_ann(RegisterPair.DE, p), "LD DE, (nn)", 0xED5B, 2, 4));

        _instructions.Add(0xED5E, new Instruction(_ => IM(InterruptMode.IM2), "IM 1", 0xED5E, 0));

        _instructions.Add(0xED5F, new Instruction(_ => LD_R_R(Register.A, Register.R), "LD A, R", 0xED5F, 0));
        
        _instructions.Add(0xED62, new Instruction(_ => SBC_RR_RR(RegisterPair.HL, RegisterPair.HL), "SBC HL, HL", 0xED62, 0));

        _instructions.Add(0xED63, new Instruction(p => LD_ann_RR(p, RegisterPair.HL), "LD (nn), HL", 0xED63, 2, 4));

        _instructions.Add(0xED67, new Instruction(_ => RRD(), "RRD", 0xED67, 0));
        
        _instructions.Add(0xED6A, new Instruction(_ => ADC_RR_RR(RegisterPair.HL, RegisterPair.HL), "ADC HL, HL", 0xED6A, 0));

        _instructions.Add(0xED6B, new Instruction(p => LD_RR_ann(RegisterPair.HL, p), "LD HL, (nn)", 0xED6B, 2, 4));

        _instructions.Add(0xED6F, new Instruction(_ => RLD(), "RLD", 0xED6F, 0));

        _instructions.Add(0xED72, new Instruction(_ => SBC_RR_RR(RegisterPair.HL, RegisterPair.SP), "SBC HL, SP", 0xED72, 0));

        _instructions.Add(0xED73, new Instruction(p => LD_ann_RR(p, RegisterPair.SP), "LD (nn), SP", 0xED73, 2, 4));
        
        _instructions.Add(0xED7A, new Instruction(_ => ADC_RR_RR(RegisterPair.HL, RegisterPair.SP), "ADC HL, SP", 0xED7A, 0));

        _instructions.Add(0xED7B, new Instruction(p => LD_RR_ann(RegisterPair.SP, p), "LD SP, (nn)", 0xED7B, 2, 4));

        _instructions.Add(0xEDA0, new Instruction(_ => LDI(), "LDI", 0xEDA0, 0));

        _instructions.Add(0xEDA1, new Instruction(_ => CPI(), "CPI", 0xEDA1, 0));

        _instructions.Add(0xEDA8, new Instruction(_ => LDD(), "LDD", 0xEDA8, 0));

        _instructions.Add(0xEDA9, new Instruction(_ => CPD(), "CPD", 0xEDA9, 0));

        _instructions.Add(0xEDB0, new Instruction(_ => LDIR(), "LDIR", 0xEDB0, 0));

        _instructions.Add(0xEDB1, new Instruction(_ => CPIR(), "CPIR", 0xEDB1, 0));

        _instructions.Add(0xEDB8, new Instruction(_ => LDDR(), "LDDR", 0xEDB8, 0));

        _instructions.Add(0xEDB9, new Instruction(_ => CPDR(), "CPDR", 0xEDB9, 0));
    }
}