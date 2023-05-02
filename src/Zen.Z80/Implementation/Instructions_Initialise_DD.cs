// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDInstructions()
    {
        _instructions.Add(0xDD00, new Instruction(_ => NOP(), "NOP", 0xDD00, 0, 4));
        
        InitialiseLDByteInstructions(0xDD00);
        
        InitialiseArithmeticLogicInstructions(0xDD00);
        
        InitialiseDDIncDecInstructions();

        InitialiseMiscDDInstructions();

        _instructions.Add(0xDDCB, new Instruction(_ => PREFIX(0xDDCB), "PREFIX 0xDDCB", 0xDDCB, 2));
    }

    private void InitialiseMiscDDInstructions()
    {
        _instructions.Add(0xDD06, new Instruction(p => LD_R_n(Register.B, p), "LD B, n", 0xDD06, 1, 4));

        _instructions.Add(0xDD09, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.BC), "ADD IX, BC", 0xDD09, 0, 4));

        _instructions.Add(0xDD0E, new Instruction(p => LD_R_n(Register.C, p), "LD C, n", 0xDD0E, 1, 4));

        _instructions.Add(0xDD16, new Instruction(p => LD_R_n(Register.D, p), "LD D, n", 0xDD16, 1, 4));

        _instructions.Add(0xDD19, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.DE), "ADD IX, DE", 0xDD19, 0, 4));

        _instructions.Add(0xDD1E, new Instruction(p => LD_R_n(Register.E, p), "LD E, n", 0xDD1E, 1, 4));

        _instructions.Add(0xDD21, new Instruction(p => LD_RR_nn(RegisterPair.IX, p), "LD IX, nn", 0xDD21, 2, 4));

        _instructions.Add(0xDD22, new Instruction(p => LD_ann_RR(p, RegisterPair.IX), "LD (nn), IX", 0xDD22, 2, 4));

        _instructions.Add(0xDD26, new Instruction(p => LD_R_n(Register.IXh, p), "LD IXh, n", 0xDD26, 1, 4));

        _instructions.Add(0xDD29, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.IX), "ADD IX, IX", 0xDD29, 0, 4));

        _instructions.Add(0xDD2A, new Instruction(p => LD_RR_ann(RegisterPair.IX, p), "LD IX, (nn)", 0xDD2A, 2, 4));

        _instructions.Add(0xDD2E, new Instruction(p => LD_R_n(Register.IXl, p), "LD IXl, n", 0xDD2E, 1, 4));

        _instructions.Add(0xDD36, new Instruction(p => LD_aRRd_n(RegisterPair.IX, p), "LD (IX + d), n", 0xDD36, 2));

        _instructions.Add(0xDD39, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.SP), "ADD IX, SP", 0xDD39, 0, 4));

        _instructions.Add(0xDDCD, new Instruction(CALL_nn, "CALL nn", 0xDDCD, 2, 4));

        _instructions.Add(0xDD3E, new Instruction(p => LD_R_n(Register.A, p), "LD A, n", 0xDD3E, 1, 4));

        _instructions.Add(0xDDE1, new Instruction(_ => POP(RegisterPair.IX), "POP IX", 0xDDE1, 0, 4));

        _instructions.Add(0xDDE3, new Instruction(_ => EX_aSP_RR(RegisterPair.IX), "EX (SP), IX", 0xDDE3, 0, 4));

        _instructions.Add(0xDDE5, new Instruction(_ => PUSH(RegisterPair.IX), "PUSH IX", 0xDDE5, 0, 4));

        _instructions.Add(0xDDE9, new Instruction(_ => JP_RR(RegisterPair.IX), "JP IX", 0xDDE9, 0, 4));

        _instructions.Add(0xDDF9, new Instruction(_ => LD_RR_RR(RegisterPair.SP, RegisterPair.IX), "LD SP, IX", 0xDDF9, 0, 4));
    }

    private void InitialiseDDIncDecInstructions()
    {
        _instructions.Add(0xDD04, new Instruction(_ => INC_R(Register.B), "INC B", 0xDD04, 0, 4));

        _instructions.Add(0xDD05, new Instruction(_ => DEC_R(Register.B), "DEC B", 0xDD05, 0, 4));

        _instructions.Add(0xDD0C, new Instruction(_ => INC_R(Register.C), "INC C", 0xDD0C, 0, 4));

        _instructions.Add(0xDD0D, new Instruction(_ => DEC_R(Register.C), "DEC C", 0xDD0D, 0, 4));

        _instructions.Add(0xDD14, new Instruction(_ => INC_R(Register.D), "INC D", 0xDD14, 0, 4));

        _instructions.Add(0xDD15, new Instruction(_ => DEC_R(Register.D), "DEC D", 0xDD15, 0, 4));

        _instructions.Add(0xDD1C, new Instruction(_ => INC_R(Register.E), "INC E", 0xDD1C, 0, 4));

        _instructions.Add(0xDD1D, new Instruction(_ => DEC_R(Register.E), "DEC E", 0xDD1D, 0, 4));

        _instructions.Add(0xDD23, new Instruction(_ => INC_RR(RegisterPair.IX), "INC IX", 0xDD23, 0, 4));

        _instructions.Add(0xDD24, new Instruction(_ => INC_R(Register.IXh), "INC IXh", 0xDD24, 0, 4));

        _instructions.Add(0xDD25, new Instruction(_ => DEC_R(Register.IXh), "DEC IXh", 0xDD25, 0, 4));

        _instructions.Add(0xDD2B, new Instruction(_ => DEC_RR(RegisterPair.IX), "DEC IX", 0xDD2B, 0, 4));

        _instructions.Add(0xDD2C, new Instruction(_ => INC_R(Register.IXl), "INC IXl", 0xDD2C, 0, 4));

        _instructions.Add(0xDD2D, new Instruction(_ => DEC_R(Register.IXl), "DEC IXl", 0xDD2D, 0, 4));

        _instructions.Add(0xDD33, new Instruction(_ => INC_RR(RegisterPair.SP), "INC SP", 0xDD33, 0, 4));
        
        _instructions.Add(0xDD34, new Instruction(p => INC_aRRd(RegisterPair.IX, p), "INC (IX + d)", 0xDD34, 1));

        _instructions.Add(0xDD35, new Instruction(p => DEC_aRRd(RegisterPair.IX, p), "DEC (IX + d)", 0xDD35, 1));

        _instructions.Add(0xDD3C, new Instruction(_ => INC_R(Register.A), "INC A", 0xDD3C, 0, 4));

        _instructions.Add(0xDD3D, new Instruction(_ => DEC_R(Register.A), "DEC A", 0xDD3D, 0, 4));
    }
}