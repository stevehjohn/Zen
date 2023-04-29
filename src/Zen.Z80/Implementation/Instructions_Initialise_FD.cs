// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseFDInstructions()
    {
        _instructions.Add(0xFD00, new Instruction(_ => NOP(), "NOP", 0xFD00, 0, 4));
                
        InitialiseLDByteInstructions(0xFD00);
                
        InitialiseArithmeticLogicInstructions(0xFD00);

        InitialiseFDIncDecInstructions();

        InitialiseMiscFDInstructions();

        _instructions.Add(0xFDCB, new Instruction(_ => PREFIX(0xFDCB), "PREFIX 0xFDCB", 0xFDCB, 2));
    }
        
    private void InitialiseMiscFDInstructions()
    {
        _instructions.Add(0xFD06, new Instruction(p => LD_R_n(Register.B, p), "LD B, n", 0xFD06, 1, 4));

        _instructions.Add(0xFD09, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.BC), "ADD IX, BC", 0xFD09, 0, 4));

        _instructions.Add(0xFD0E, new Instruction(p => LD_R_n(Register.C, p), "LD C, n", 0xFD0E, 1, 4));

        _instructions.Add(0xFD16, new Instruction(p => LD_R_n(Register.D, p), "LD D, n", 0xFD16, 1, 4));

        _instructions.Add(0xFD19, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.DE), "ADD IX, DE", 0xFD19, 0, 4));

        _instructions.Add(0xFD1E, new Instruction(p => LD_R_n(Register.E, p), "LD E, n", 0xFD1E, 1, 4));

        _instructions.Add(0xFD21, new Instruction(p => LD_RR_nn(RegisterPair.IX, p), "LD IX, nn", 0xFD21, 2, 4));

        _instructions.Add(0xFD22, new Instruction(p => LD_ann_RR(p, RegisterPair.IX), "LD (nn), IX", 0xFD22, 2, 4));

        _instructions.Add(0xFD26, new Instruction(p => LD_R_n(Register.IXh, p), "LD IXh, n", 0xFD26, 1, 4));

        _instructions.Add(0xFD29, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.IX), "ADD IX, IX", 0xFD29, 0, 4));

        _instructions.Add(0xFD2A, new Instruction(p => LD_RR_ann(RegisterPair.IX, p), "LD IX, (nn)", 0xFD2A, 2, 4));

        _instructions.Add(0xFD2E, new Instruction(p => LD_R_n(Register.IXl, p), "LD IXl, n", 0xFD2E, 1, 4));

        _instructions.Add(0xFD36, new Instruction(p => LD_aRRd_n(RegisterPair.IX, p), "LD (IX + d), n", 0xFD36, 2, 4));

        _instructions.Add(0xFD39, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.SP), "ADD IX, SP", 0xFD39, 0, 4));

        _instructions.Add(0xFD3E, new Instruction(p => LD_R_n(Register.A, p), "LD A, n", 0xFD3E, 1, 4));

        _instructions.Add(0xFDE1, new Instruction(_ => POP(RegisterPair.IX), "POP IX", 0xFDE1, 0, 4));

        _instructions.Add(0xFDE3, new Instruction(_ => EX_aSP_RR(RegisterPair.IX), "EX (SP), IX", 0xFDE3, 0, 4));

        _instructions.Add(0xFDE5, new Instruction(_ => PUSH(RegisterPair.IX), "PUSH IX", 0xFDE5, 0, 4));

        _instructions.Add(0xFDE9, new Instruction(_ => JP_RR(RegisterPair.IX), "JP IX", 0xFDE9, 0, 4));

        _instructions.Add(0xFDF9, new Instruction(_ => LD_RR_RR(RegisterPair.SP, RegisterPair.IX), "LD SP, IX", 0xFDF9, 0, 4));
    }

    private void InitialiseFDIncDecInstructions()
    {
        _instructions.Add(0xFD04, new Instruction(_ => INC_R(Register.B), "INC B", 0xFD04, 0, 4));

        _instructions.Add(0xFD05, new Instruction(_ => DEC_R(Register.B), "DEC B", 0xFD05, 0, 4));

        _instructions.Add(0xFD0C, new Instruction(_ => INC_R(Register.C), "INC C", 0xFD0C, 0, 4));

        _instructions.Add(0xFD0D, new Instruction(_ => DEC_R(Register.C), "DEC C", 0xFD0D, 0, 4));

        _instructions.Add(0xFD14, new Instruction(_ => INC_R(Register.D), "INC D", 0xFD14, 0, 4));

        _instructions.Add(0xFD15, new Instruction(_ => DEC_R(Register.D), "DEC D", 0xFD15, 0, 4));

        _instructions.Add(0xFD1C, new Instruction(_ => INC_R(Register.E), "INC E", 0xFD1C, 0, 4));

        _instructions.Add(0xFD1D, new Instruction(_ => DEC_R(Register.E), "DEC E", 0xFD1D, 0, 4));

        _instructions.Add(0xFD23, new Instruction(_ => INC_RR(RegisterPair.IY), "INC IY", 0xFD23, 0, 4));

        _instructions.Add(0xFD24, new Instruction(_ => INC_R(Register.IYh), "INC IYh", 0xFD24, 0, 4));

        _instructions.Add(0xFD25, new Instruction(_ => DEC_R(Register.IYh), "DEC IYh", 0xFD25, 0, 4));

        _instructions.Add(0xFD2B, new Instruction(_ => DEC_RR(RegisterPair.IY), "DEC IY", 0xFD2B, 0, 4));

        _instructions.Add(0xFD2C, new Instruction(_ => INC_R(Register.IYl), "INC IYl", 0xFD2C, 0, 4));

        _instructions.Add(0xFD2D, new Instruction(_ => DEC_R(Register.IYl), "DEC IYl", 0xFD2D, 0, 4));

        _instructions.Add(0xFD33, new Instruction(_ => INC_RR(RegisterPair.SP), "INC SP", 0xFD33, 0, 4));
        
        _instructions.Add(0xFD34, new Instruction(p => INC_aRRd(RegisterPair.IY, p), "INC (IY + d)", 0xFD34, 1));

        _instructions.Add(0xFD35, new Instruction(p => DEC_aRRd(RegisterPair.IY, p), "DEC (IY + d)", 0xFD35, 1));

        _instructions.Add(0xFD3C, new Instruction(_ => INC_R(Register.A), "INC A", 0xFD3C, 0, 4));

        _instructions.Add(0xFD3D, new Instruction(_ => DEC_R(Register.A), "DEC A", 0xFD3D, 0, 4));
    }}