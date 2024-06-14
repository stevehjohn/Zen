using Zen.Z80.Processor;

// ReSharper disable ConvertClosureToMethodGroup
// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void Initialise0xFD()
    {
        _instructions.Add(0x00FD00, new Instruction(_ => NOP(), "NOP", 0x00FD00, 0, 4));

        _instructions.Add(0x00FD01, new Instruction(p => LD_RR_nn(RegisterPair.BC, p), "LD BC, nn", 0x00FD01, 2, 4));

        _instructions.Add(0x00FD02, new Instruction(_ => LD_aRR_R(RegisterPair.BC, Register.A), "LD (BC), A", 0x00FD02, 0, 4));

        _instructions.Add(0x00FD03, new Instruction(_ => INC_RR(RegisterPair.BC), "INC BC", 0x00FD03, 0, 4));

        _instructions.Add(0x00FD04, new Instruction(_ => INC_R(Register.B), "INC B", 0x00FD04, 0, 4));

        _instructions.Add(0x00FD05, new Instruction(_ => DEC_R(Register.B), "DEC B", 0x00FD05, 0, 4));

        _instructions.Add(0x00FD06, new Instruction(p => LD_R_n(Register.B, p), "LD B, n", 0x00FD06, 1, 4));

        _instructions.Add(0x00FD07, new Instruction(_ => RLCA(), "RLCA", 0x00FD07, 0, 4));

        _instructions.Add(0x00FD08, new Instruction(_ => EX_RR_RR(RegisterPair.AF, RegisterPair.AF_), "EX AF, AF'", 0x00FD08, 0, 4));

        _instructions.Add(0x00FD09, new Instruction(_ => ADD_RR_RR(RegisterPair.IY, RegisterPair.BC), "ADD IY, BC", 0x00FD09, 0, 4));

        _instructions.Add(0x00FD0A, new Instruction(_ => LD_R_aRR(Register.A, RegisterPair.BC), "LD A, (BC)", 0x00FD0A, 0, 4));

        _instructions.Add(0x00FD0B, new Instruction(_ => DEC_RR(RegisterPair.BC), "DEC BC", 0x00FD0B, 0, 4));

        _instructions.Add(0x00FD0C, new Instruction(_ => INC_R(Register.C), "INC C", 0x00FD0C, 0, 4));

        _instructions.Add(0x00FD0D, new Instruction(_ => DEC_R(Register.C), "DEC C", 0x00FD0D, 0, 4));

        _instructions.Add(0x00FD0E, new Instruction(p => LD_R_n(Register.C, p), "LD C, n", 0x00FD0E, 1, 4));

        _instructions.Add(0x00FD0F, new Instruction(_ => RRCA(), "RRCA", 0x00FD0F, 0, 4));

        _instructions.Add(0x00FD10, new Instruction(p => DJNZ_e(p), "DJNZ e", 0x00FD10, 1, 4));

        _instructions.Add(0x00FD11, new Instruction(p => LD_RR_nn(RegisterPair.DE, p), "LD DE, nn", 0x00FD11, 2, 4));

        _instructions.Add(0x00FD12, new Instruction(_ => LD_aRR_R(RegisterPair.DE, Register.A), "LD (DE), A", 0x00FD12, 0, 4));

        _instructions.Add(0x00FD13, new Instruction(_ => INC_RR(RegisterPair.DE), "INC DE", 0x00FD13, 0, 4));

        _instructions.Add(0x00FD14, new Instruction(_ => INC_R(Register.D), "INC D", 0x00FD14, 0, 4));

        _instructions.Add(0x00FD15, new Instruction(_ => DEC_R(Register.D), "DEC D", 0x00FD15, 0, 4));

        _instructions.Add(0x00FD16, new Instruction(p => LD_R_n(Register.D, p), "LD D, n", 0x00FD16, 1, 4));

        _instructions.Add(0x00FD17, new Instruction(_ => RLA(), "RLA", 0x00FD17, 0, 4));

        _instructions.Add(0x00FD18, new Instruction(p => JR_e(p), "JR e", 0x00FD18, 1, 4));

        _instructions.Add(0x00FD19, new Instruction(_ => ADD_RR_RR(RegisterPair.IY, RegisterPair.DE), "ADD IY, DE", 0x00FD19, 0, 4));

        _instructions.Add(0x00FD1A, new Instruction(_ => LD_R_aRR(Register.A, RegisterPair.DE), "LD A, (DE)", 0x00FD1A, 0, 4));

        _instructions.Add(0x00FD1B, new Instruction(_ => DEC_RR(RegisterPair.DE), "DEC DE", 0x00FD1B, 0, 4));

        _instructions.Add(0x00FD1C, new Instruction(_ => INC_R(Register.E), "INC E", 0x00FD1C, 0, 4));

        _instructions.Add(0x00FD1D, new Instruction(_ => DEC_R(Register.E), "DEC E", 0x00FD1D, 0, 4));

        _instructions.Add(0x00FD1E, new Instruction(p => LD_R_n(Register.E, p), "LD E, n", 0x00FD1E, 1, 4));

        _instructions.Add(0x00FD1F, new Instruction(_ => RRA(), "RRA", 0x00FD1F, 0, 4));

        _instructions.Add(0x00FD20, new Instruction(p => JR_F_e(Flag.Zero, p, true), "JR NZ, e", 0x00FD20, 1, 4));

        _instructions.Add(0x00FD21, new Instruction(p => LD_RR_nn(RegisterPair.IY, p), "LD IY, nn", 0x00FD21, 2, 4));

        _instructions.Add(0x00FD22, new Instruction(p => LD_ann_RR(p, RegisterPair.IY), "LD (nn), IY", 0x00FD22, 2, 4));

        _instructions.Add(0x00FD23, new Instruction(_ => INC_RR(RegisterPair.IY), "INC IY", 0x00FD23, 0, 4));

        _instructions.Add(0x00FD24, new Instruction(_ => INC_R(Register.IYh), "INC IYh", 0x00FD24, 0, 4));

        _instructions.Add(0x00FD25, new Instruction(_ => DEC_R(Register.IYh), "DEC IYh", 0x00FD25, 0, 4));

        _instructions.Add(0x00FD26, new Instruction(p => LD_R_n(Register.IYh, p), "LD IYh, n", 0x00FD26, 1, 4));

        _instructions.Add(0x00FD27, new Instruction(_ => DAA(), "DAA", 0x00FD27, 0, 4));

        _instructions.Add(0x00FD28, new Instruction(p => JR_F_e(Flag.Zero, p), "JR Z, e", 0x00FD28, 1, 4));

        _instructions.Add(0x00FD29, new Instruction(_ => ADD_RR_RR(RegisterPair.IY, RegisterPair.IY), "ADD IY, IY", 0x00FD29, 0, 4));

        _instructions.Add(0x00FD2A, new Instruction(p => LD_RR_ann(RegisterPair.IY, p), "LD IY, (nn)", 0x00FD2A, 2, 4));

        _instructions.Add(0x00FD2B, new Instruction(_ => DEC_RR(RegisterPair.IY), "DEC IY", 0x00FD2B, 0, 4));

        _instructions.Add(0x00FD2C, new Instruction(_ => INC_R(Register.IYl), "INC IYl", 0x00FD2C, 0, 4));

        _instructions.Add(0x00FD2D, new Instruction(_ => DEC_R(Register.IYl), "DEC IYl", 0x00FD2D, 0, 4));

        _instructions.Add(0x00FD2E, new Instruction(p => LD_R_n(Register.IYl, p), "LD IYl, n", 0x00FD2E, 1, 4));

        _instructions.Add(0x00FD2F, new Instruction(_ => CPL(), "CPL", 0x00FD2F, 0, 4));

        _instructions.Add(0x00FD30, new Instruction(p => JR_F_e(Flag.Carry, p, true), "JR NC, e", 0x00FD30, 1, 4));

        _instructions.Add(0x00FD31, new Instruction(p => LD_RR_nn(RegisterPair.SP, p), "LD SP, nn", 0x00FD31, 2, 4));

        _instructions.Add(0x00FD32, new Instruction(p => LD_ann_R(p, Register.A), "LD (nn), A", 0x00FD32, 2, 4));

        _instructions.Add(0x00FD33, new Instruction(_ => INC_RR(RegisterPair.SP), "INC SP", 0x00FD33, 0, 4));

        _instructions.Add(0x00FD34, new Instruction(p => INC_aRRd(RegisterPair.IY, p), "INC (IY + d)", 0x00FD34, 1));

        _instructions.Add(0x00FD35, new Instruction(p => DEC_aRRd(RegisterPair.IY, p), "DEC (IY + d)", 0x00FD35, 1));

        _instructions.Add(0x00FD36, new Instruction(p => LD_aRRd_n(RegisterPair.IY, p), "LD (IY + d), n", 0x00FD36, 2));

        _instructions.Add(0x00FD37, new Instruction(_ => SCF(), "SCF", 0x00FD37, 0, 4));

        _instructions.Add(0x00FD38, new Instruction(p => JR_F_e(Flag.Carry, p), "JR C, e", 0x00FD38, 1, 4));

        _instructions.Add(0x00FD39, new Instruction(_ => ADD_RR_RR(RegisterPair.IY, RegisterPair.SP), "ADD IY, SP", 0x00FD39, 0, 4));

        _instructions.Add(0x00FD3A, new Instruction(p => LD_R_ann(Register.A, p), "LD A, (nn)", 0x00FD3A, 2, 4));

        _instructions.Add(0x00FD3B, new Instruction(_ => DEC_RR(RegisterPair.SP), "DEC SP", 0x00FD3B, 0, 4));

        _instructions.Add(0x00FD3C, new Instruction(_ => INC_R(Register.A), "INC A", 0x00FD3C, 0, 4));

        _instructions.Add(0x00FD3D, new Instruction(_ => DEC_R(Register.A), "DEC A", 0x00FD3D, 0, 4));

        _instructions.Add(0x00FD3E, new Instruction(p => LD_R_n(Register.A, p), "LD A, n", 0x00FD3E, 1, 4));

        _instructions.Add(0x00FD3F, new Instruction(_ => CCF(), "CCF", 0x00FD3F, 0, 4));

        _instructions.Add(0x00FD40, new Instruction(_ => LD_R_R(Register.B, Register.B), "LD B, B", 0x00FD40, 0, 4));

        _instructions.Add(0x00FD41, new Instruction(_ => LD_R_R(Register.B, Register.C), "LD B, C", 0x00FD41, 0, 4));

        _instructions.Add(0x00FD42, new Instruction(_ => LD_R_R(Register.B, Register.D), "LD B, D", 0x00FD42, 0, 4));

        _instructions.Add(0x00FD43, new Instruction(_ => LD_R_R(Register.B, Register.E), "LD B, E", 0x00FD43, 0, 4));

        _instructions.Add(0x00FD44, new Instruction(_ => LD_R_R(Register.B, Register.IYh), "LD B, IYh", 0x00FD44, 0, 4));

        _instructions.Add(0x00FD45, new Instruction(_ => LD_R_R(Register.B, Register.IYl), "LD B, IYl", 0x00FD45, 0, 4));

        _instructions.Add(0x00FD46, new Instruction(p => LD_R_aRRd(Register.B, RegisterPair.IY, p), "LD B, (IY + d)", 0x00FD46, 1));

        _instructions.Add(0x00FD47, new Instruction(_ => LD_R_R(Register.B, Register.A), "LD B, A", 0x00FD47, 0, 4));

        _instructions.Add(0x00FD48, new Instruction(_ => LD_R_R(Register.C, Register.B), "LD C, B", 0x00FD48, 0, 4));

        _instructions.Add(0x00FD49, new Instruction(_ => LD_R_R(Register.C, Register.C), "LD C, C", 0x00FD49, 0, 4));

        _instructions.Add(0x00FD4A, new Instruction(_ => LD_R_R(Register.C, Register.D), "LD C, D", 0x00FD4A, 0, 4));

        _instructions.Add(0x00FD4B, new Instruction(_ => LD_R_R(Register.C, Register.E), "LD C, E", 0x00FD4B, 0, 4));

        _instructions.Add(0x00FD4C, new Instruction(_ => LD_R_R(Register.C, Register.IYh), "LD C, IYh", 0x00FD4C, 0, 4));

        _instructions.Add(0x00FD4D, new Instruction(_ => LD_R_R(Register.C, Register.IYl), "LD C, IYl", 0x00FD4D, 0, 4));

        _instructions.Add(0x00FD4E, new Instruction(p => LD_R_aRRd(Register.C, RegisterPair.IY, p), "LD C, (IY + d)", 0x00FD4E, 1));

        _instructions.Add(0x00FD4F, new Instruction(_ => LD_R_R(Register.C, Register.A), "LD C, A", 0x00FD4F, 0, 4));

        _instructions.Add(0x00FD50, new Instruction(_ => LD_R_R(Register.D, Register.B), "LD D, B", 0x00FD50, 0, 4));

        _instructions.Add(0x00FD51, new Instruction(_ => LD_R_R(Register.D, Register.C), "LD D, C", 0x00FD51, 0, 4));

        _instructions.Add(0x00FD52, new Instruction(_ => LD_R_R(Register.D, Register.D), "LD D, D", 0x00FD52, 0, 4));

        _instructions.Add(0x00FD53, new Instruction(_ => LD_R_R(Register.D, Register.E), "LD D, E", 0x00FD53, 0, 4));

        _instructions.Add(0x00FD54, new Instruction(_ => LD_R_R(Register.D, Register.IYh), "LD D, IYh", 0x00FD54, 0, 4));

        _instructions.Add(0x00FD55, new Instruction(_ => LD_R_R(Register.D, Register.IYl), "LD D, IYl", 0x00FD55, 0, 4));

        _instructions.Add(0x00FD56, new Instruction(p => LD_R_aRRd(Register.D, RegisterPair.IY, p), "LD D, (IY + d)", 0x00FD56, 1));

        _instructions.Add(0x00FD57, new Instruction(_ => LD_R_R(Register.D, Register.A), "LD D, A", 0x00FD57, 0, 4));

        _instructions.Add(0x00FD58, new Instruction(_ => LD_R_R(Register.E, Register.B), "LD E, B", 0x00FD58, 0, 4));

        _instructions.Add(0x00FD59, new Instruction(_ => LD_R_R(Register.E, Register.C), "LD E, C", 0x00FD59, 0, 4));

        _instructions.Add(0x00FD5A, new Instruction(_ => LD_R_R(Register.E, Register.D), "LD E, D", 0x00FD5A, 0, 4));

        _instructions.Add(0x00FD5B, new Instruction(_ => LD_R_R(Register.E, Register.E), "LD E, E", 0x00FD5B, 0, 4));

        _instructions.Add(0x00FD5C, new Instruction(_ => LD_R_R(Register.E, Register.IYh), "LD E, IYh", 0x00FD5C, 0, 4));

        _instructions.Add(0x00FD5D, new Instruction(_ => LD_R_R(Register.E, Register.IYl), "LD E, IYl", 0x00FD5D, 0, 4));

        _instructions.Add(0x00FD5E, new Instruction(p => LD_R_aRRd(Register.E, RegisterPair.IY, p), "LD E, (IY + d)", 0x00FD5E, 1));

        _instructions.Add(0x00FD5F, new Instruction(_ => LD_R_R(Register.E, Register.A), "LD E, A", 0x00FD5F, 0, 4));

        _instructions.Add(0x00FD60, new Instruction(_ => LD_R_R(Register.IYh, Register.B), "LD IYh, B", 0x00FD60, 0, 4));

        _instructions.Add(0x00FD61, new Instruction(_ => LD_R_R(Register.IYh, Register.C), "LD IYh, C", 0x00FD61, 0, 4));

        _instructions.Add(0x00FD62, new Instruction(_ => LD_R_R(Register.IYh, Register.D), "LD IYh, D", 0x00FD62, 0, 4));

        _instructions.Add(0x00FD63, new Instruction(_ => LD_R_R(Register.IYh, Register.E), "LD IYh, E", 0x00FD63, 0, 4));

        _instructions.Add(0x00FD64, new Instruction(_ => LD_R_R(Register.IYh, Register.IYh), "LD IYh, IYh", 0x00FD64, 0, 4));

        _instructions.Add(0x00FD65, new Instruction(_ => LD_R_R(Register.IYh, Register.IYl), "LD IYh, IYl", 0x00FD65, 0, 4));

        _instructions.Add(0x00FD66, new Instruction(p => LD_R_aRRd(Register.H, RegisterPair.IY, p), "LD H, (IY + d)", 0x00FD66, 1));

        _instructions.Add(0x00FD67, new Instruction(_ => LD_R_R(Register.IYh, Register.A), "LD IYh, A", 0x00FD67, 0, 4));

        _instructions.Add(0x00FD68, new Instruction(_ => LD_R_R(Register.IYl, Register.B), "LD IYl, B", 0x00FD68, 0, 4));

        _instructions.Add(0x00FD69, new Instruction(_ => LD_R_R(Register.IYl, Register.C), "LD IYl, C", 0x00FD69, 0, 4));

        _instructions.Add(0x00FD6A, new Instruction(_ => LD_R_R(Register.IYl, Register.D), "LD IYl, D", 0x00FD6A, 0, 4));

        _instructions.Add(0x00FD6B, new Instruction(_ => LD_R_R(Register.IYl, Register.E), "LD IYl, E", 0x00FD6B, 0, 4));

        _instructions.Add(0x00FD6C, new Instruction(_ => LD_R_R(Register.IYl, Register.IYh), "LD IYl, IYh", 0x00FD6C, 0, 4));

        _instructions.Add(0x00FD6D, new Instruction(_ => LD_R_R(Register.IYl, Register.IYl), "LD IYl, IYl", 0x00FD6D, 0, 4));

        _instructions.Add(0x00FD6E, new Instruction(p => LD_R_aRRd(Register.L, RegisterPair.IY, p), "LD L, (IY + d)", 0x00FD6E, 1));

        _instructions.Add(0x00FD6F, new Instruction(_ => LD_R_R(Register.IYl, Register.A), "LD IYl, A", 0x00FD6F, 0, 4));

        _instructions.Add(0x00FD70, new Instruction(p => LD_aRRd_R(RegisterPair.IY, p, Register.B), "LD (IY + d), B", 0x00FD70, 1));

        _instructions.Add(0x00FD71, new Instruction(p => LD_aRRd_R(RegisterPair.IY, p, Register.C), "LD (IY + d), C", 0x00FD71, 1));

        _instructions.Add(0x00FD72, new Instruction(p => LD_aRRd_R(RegisterPair.IY, p, Register.D), "LD (IY + d), D", 0x00FD72, 1));

        _instructions.Add(0x00FD73, new Instruction(p => LD_aRRd_R(RegisterPair.IY, p, Register.E), "LD (IY + d), E", 0x00FD73, 1));

        _instructions.Add(0x00FD74, new Instruction(p => LD_aRRd_R(RegisterPair.IY, p, Register.H), "LD (IY + d), H", 0x00FD74, 1));

        _instructions.Add(0x00FD75, new Instruction(p => LD_aRRd_R(RegisterPair.IY, p, Register.L), "LD (IY + d), L", 0x00FD75, 1));

        _instructions.Add(0x00FD76, new Instruction(_ => HALT(), "HALT", 0x00FD76, 0, 4));

        _instructions.Add(0x00FD77, new Instruction(p => LD_aRRd_R(RegisterPair.IY, p, Register.A), "LD (IY + d), A", 0x00FD77, 1));

        _instructions.Add(0x00FD78, new Instruction(_ => LD_R_R(Register.A, Register.B), "LD A, B", 0x00FD78, 0, 4));

        _instructions.Add(0x00FD79, new Instruction(_ => LD_R_R(Register.A, Register.C), "LD A, C", 0x00FD79, 0, 4));

        _instructions.Add(0x00FD7A, new Instruction(_ => LD_R_R(Register.A, Register.D), "LD A, D", 0x00FD7A, 0, 4));

        _instructions.Add(0x00FD7B, new Instruction(_ => LD_R_R(Register.A, Register.E), "LD A, E", 0x00FD7B, 0, 4));

        _instructions.Add(0x00FD7C, new Instruction(_ => LD_R_R(Register.A, Register.IYh), "LD A, IYh", 0x00FD7C, 0, 4));

        _instructions.Add(0x00FD7D, new Instruction(_ => LD_R_R(Register.A, Register.IYl), "LD A, IYl", 0x00FD7D, 0, 4));

        _instructions.Add(0x00FD7E, new Instruction(p => LD_R_aRRd(Register.A, RegisterPair.IY, p), "LD A, (IY + d)", 0x00FD7E, 1));

        _instructions.Add(0x00FD7F, new Instruction(_ => LD_R_R(Register.A, Register.A), "LD A, A", 0x00FD7F, 0, 4));

        _instructions.Add(0x00FD80, new Instruction(_ => ADD_R_R(Register.A, Register.B), "ADD A, B", 0x00FD80, 0, 4));

        _instructions.Add(0x00FD81, new Instruction(_ => ADD_R_R(Register.A, Register.C), "ADD A, C", 0x00FD81, 0, 4));

        _instructions.Add(0x00FD82, new Instruction(_ => ADD_R_R(Register.A, Register.D), "ADD A, D", 0x00FD82, 0, 4));

        _instructions.Add(0x00FD83, new Instruction(_ => ADD_R_R(Register.A, Register.E), "ADD A, E", 0x00FD83, 0, 4));

        _instructions.Add(0x00FD84, new Instruction(_ => ADD_R_R(Register.A, Register.IYh), "ADD A, IYh", 0x00FD84, 0, 4));

        _instructions.Add(0x00FD85, new Instruction(_ => ADD_R_R(Register.A, Register.IYl), "ADD A, IYl", 0x00FD85, 0, 4));

        _instructions.Add(0x00FD86, new Instruction(p => ADD_R_aRRd(Register.A, RegisterPair.IY, p), "ADD A, (IY + d)", 0x00FD86, 1));

        _instructions.Add(0x00FD87, new Instruction(_ => ADD_R_R(Register.A, Register.A), "ADD A, A", 0x00FD87, 0, 4));

        _instructions.Add(0x00FD88, new Instruction(_ => ADC_R_R(Register.A, Register.B), "ADC A, B", 0x00FD88, 0, 4));

        _instructions.Add(0x00FD89, new Instruction(_ => ADC_R_R(Register.A, Register.C), "ADC A, C", 0x00FD89, 0, 4));

        _instructions.Add(0x00FD8A, new Instruction(_ => ADC_R_R(Register.A, Register.D), "ADC A, D", 0x00FD8A, 0, 4));

        _instructions.Add(0x00FD8B, new Instruction(_ => ADC_R_R(Register.A, Register.E), "ADC A, E", 0x00FD8B, 0, 4));

        _instructions.Add(0x00FD8C, new Instruction(_ => ADC_R_R(Register.A, Register.IYh), "ADC A, IYh", 0x00FD8C, 0, 4));

        _instructions.Add(0x00FD8D, new Instruction(_ => ADC_R_R(Register.A, Register.IYl), "ADC A, IYl", 0x00FD8D, 0, 4));

        _instructions.Add(0x00FD8E, new Instruction(p => ADC_R_aRRd(Register.A, RegisterPair.IY, p), "ADC A, (IY + d)", 0x00FD8E, 1));

        _instructions.Add(0x00FD8F, new Instruction(_ => ADC_R_R(Register.A, Register.A), "ADC A, A", 0x00FD8F, 0, 4));

        _instructions.Add(0x00FD90, new Instruction(_ => SUB_R_R(Register.A, Register.B), "SUB A, B", 0x00FD90, 0, 4));

        _instructions.Add(0x00FD91, new Instruction(_ => SUB_R_R(Register.A, Register.C), "SUB A, C", 0x00FD91, 0, 4));

        _instructions.Add(0x00FD92, new Instruction(_ => SUB_R_R(Register.A, Register.D), "SUB A, D", 0x00FD92, 0, 4));

        _instructions.Add(0x00FD93, new Instruction(_ => SUB_R_R(Register.A, Register.E), "SUB A, E", 0x00FD93, 0, 4));

        _instructions.Add(0x00FD94, new Instruction(_ => SUB_R_R(Register.A, Register.IYh), "SUB A, IYh", 0x00FD94, 0, 4));

        _instructions.Add(0x00FD95, new Instruction(_ => SUB_R_R(Register.A, Register.IYl), "SUB A, IYl", 0x00FD95, 0, 4));

        _instructions.Add(0x00FD96, new Instruction(p => SUB_R_aRRd(Register.A, RegisterPair.IY, p), "SUB A, (IY + d)", 0x00FD96, 1));

        _instructions.Add(0x00FD97, new Instruction(_ => SUB_R_R(Register.A, Register.A), "SUB A, A", 0x00FD97, 0, 4));

        _instructions.Add(0x00FD98, new Instruction(_ => SBC_R_R(Register.A, Register.B), "SBC A, B", 0x00FD98, 0, 4));

        _instructions.Add(0x00FD99, new Instruction(_ => SBC_R_R(Register.A, Register.C), "SBC A, C", 0x00FD99, 0, 4));

        _instructions.Add(0x00FD9A, new Instruction(_ => SBC_R_R(Register.A, Register.D), "SBC A, D", 0x00FD9A, 0, 4));

        _instructions.Add(0x00FD9B, new Instruction(_ => SBC_R_R(Register.A, Register.E), "SBC A, E", 0x00FD9B, 0, 4));

        _instructions.Add(0x00FD9C, new Instruction(_ => SBC_R_R(Register.A, Register.IYh), "SBC A, IYh", 0x00FD9C, 0, 4));

        _instructions.Add(0x00FD9D, new Instruction(_ => SBC_R_R(Register.A, Register.IYl), "SBC A, IYl", 0x00FD9D, 0, 4));

        _instructions.Add(0x00FD9E, new Instruction(p => SBC_R_aRRd(Register.A, RegisterPair.IY, p), "SBC A, (IY + d)", 0x00FD9E, 1));

        _instructions.Add(0x00FD9F, new Instruction(_ => SBC_R_R(Register.A, Register.A), "SBC A, A", 0x00FD9F, 0, 4));

        _instructions.Add(0x00FDA0, new Instruction(_ => AND_R_R(Register.A, Register.B), "AND A, B", 0x00FDA0, 0, 4));

        _instructions.Add(0x00FDA1, new Instruction(_ => AND_R_R(Register.A, Register.C), "AND A, C", 0x00FDA1, 0, 4));

        _instructions.Add(0x00FDA2, new Instruction(_ => AND_R_R(Register.A, Register.D), "AND A, D", 0x00FDA2, 0, 4));

        _instructions.Add(0x00FDA3, new Instruction(_ => AND_R_R(Register.A, Register.E), "AND A, E", 0x00FDA3, 0, 4));

        _instructions.Add(0x00FDA4, new Instruction(_ => AND_R_R(Register.A, Register.IYh), "AND A, IYh", 0x00FDA4, 0, 4));

        _instructions.Add(0x00FDA5, new Instruction(_ => AND_R_R(Register.A, Register.IYl), "AND A, IYl", 0x00FDA5, 0, 4));

        _instructions.Add(0x00FDA6, new Instruction(p => AND_R_aRRd(Register.A, RegisterPair.IY, p), "AND A, (IY + d)", 0x00FDA6, 1));

        _instructions.Add(0x00FDA7, new Instruction(_ => AND_R_R(Register.A, Register.A), "AND A, A", 0x00FDA7, 0, 4));

        _instructions.Add(0x00FDA8, new Instruction(_ => XOR_R_R(Register.A, Register.B), "XOR A, B", 0x00FDA8, 0, 4));

        _instructions.Add(0x00FDA9, new Instruction(_ => XOR_R_R(Register.A, Register.C), "XOR A, C", 0x00FDA9, 0, 4));

        _instructions.Add(0x00FDAA, new Instruction(_ => XOR_R_R(Register.A, Register.D), "XOR A, D", 0x00FDAA, 0, 4));

        _instructions.Add(0x00FDAB, new Instruction(_ => XOR_R_R(Register.A, Register.E), "XOR A, E", 0x00FDAB, 0, 4));

        _instructions.Add(0x00FDAC, new Instruction(_ => XOR_R_R(Register.A, Register.IYh), "XOR A, IYh", 0x00FDAC, 0, 4));

        _instructions.Add(0x00FDAD, new Instruction(_ => XOR_R_R(Register.A, Register.IYl), "XOR A, IYl", 0x00FDAD, 0, 4));

        _instructions.Add(0x00FDAE, new Instruction(p => XOR_R_aRRd(Register.A, RegisterPair.IY, p), "XOR A, (IY + d)", 0x00FDAE, 1));

        _instructions.Add(0x00FDAF, new Instruction(_ => XOR_R_R(Register.A, Register.A), "XOR A, A", 0x00FDAF, 0, 4));

        _instructions.Add(0x00FDB0, new Instruction(_ => OR_R_R(Register.A, Register.B), "OR A, B", 0x00FDB0, 0, 4));

        _instructions.Add(0x00FDB1, new Instruction(_ => OR_R_R(Register.A, Register.C), "OR A, C", 0x00FDB1, 0, 4));

        _instructions.Add(0x00FDB2, new Instruction(_ => OR_R_R(Register.A, Register.D), "OR A, D", 0x00FDB2, 0, 4));

        _instructions.Add(0x00FDB3, new Instruction(_ => OR_R_R(Register.A, Register.E), "OR A, E", 0x00FDB3, 0, 4));

        _instructions.Add(0x00FDB4, new Instruction(_ => OR_R_R(Register.A, Register.IYh), "OR A, IYh", 0x00FDB4, 0, 4));

        _instructions.Add(0x00FDB5, new Instruction(_ => OR_R_R(Register.A, Register.IYl), "OR A, IYl", 0x00FDB5, 0, 4));

        _instructions.Add(0x00FDB6, new Instruction(p => OR_R_aRRd(Register.A, RegisterPair.IY, p), "OR A, (IY + d)", 0x00FDB6, 1));

        _instructions.Add(0x00FDB7, new Instruction(_ => OR_R_R(Register.A, Register.A), "OR A, A", 0x00FDB7, 0, 4));

        _instructions.Add(0x00FDB8, new Instruction(_ => CP_R_R(Register.A, Register.B), "CP A, B", 0x00FDB8, 0, 4));

        _instructions.Add(0x00FDB9, new Instruction(_ => CP_R_R(Register.A, Register.C), "CP A, C", 0x00FDB9, 0, 4));

        _instructions.Add(0x00FDBA, new Instruction(_ => CP_R_R(Register.A, Register.D), "CP A, D", 0x00FDBA, 0, 4));

        _instructions.Add(0x00FDBB, new Instruction(_ => CP_R_R(Register.A, Register.E), "CP A, E", 0x00FDBB, 0, 4));

        _instructions.Add(0x00FDBC, new Instruction(_ => CP_R_R(Register.A, Register.IYh), "CP A, IYh", 0x00FDBC, 0, 4));

        _instructions.Add(0x00FDBD, new Instruction(_ => CP_R_R(Register.A, Register.IYl), "CP A, IYl", 0x00FDBD, 0, 4));

        _instructions.Add(0x00FDBE, new Instruction(p => CP_R_aRRd(Register.A, RegisterPair.IY, p), "CP A, (IY + d)", 0x00FDBE, 1));

        _instructions.Add(0x00FDBF, new Instruction(_ => CP_R_R(Register.A, Register.A), "CP A, A", 0x00FDBF, 0, 4));

        _instructions.Add(0x00FDC0, new Instruction(_ => RET_F(Flag.Zero, true), "RET NZ", 0x00FDC0, 0, 4));

        _instructions.Add(0x00FDC1, new Instruction(_ => POP_RR(RegisterPair.BC), "POP BC", 0x00FDC1, 0, 4));

        _instructions.Add(0x00FDC2, new Instruction(p => JP_F_nn(Flag.Zero, p, true), "JP NZ, nn", 0x00FDC2, 2, 4));

        _instructions.Add(0x00FDC3, new Instruction(p => JP_nn(p), "JP nn", 0x00FDC3, 2, 4));

        _instructions.Add(0x00FDC4, new Instruction(p => CALL_F_nn(Flag.Zero, p, true), "CALL NZ, nn", 0x00FDC4, 2, 4));

        _instructions.Add(0x00FDC5, new Instruction(_ => PUSH_RR(RegisterPair.BC), "PUSH BC", 0x00FDC5, 0, 4));

        _instructions.Add(0x00FDC6, new Instruction(p => ADD_R_n(Register.A, p), "ADD A, n", 0x00FDC6, 1, 4));

        _instructions.Add(0x00FDC7, new Instruction(_ => RST(0x00), "RST 0x00", 0x00FDC7, 0, 4));

        _instructions.Add(0x00FDC8, new Instruction(_ => RET_F(Flag.Zero), "RET Z", 0x00FDC8, 0, 4));

        _instructions.Add(0x00FDC9, new Instruction(_ => RET(), "RET", 0x00FDC9, 0, 4));

        _instructions.Add(0x00FDCA, new Instruction(p => JP_F_nn(Flag.Zero, p), "JP Z, nn", 0x00FDCA, 2, 4));

        _instructions.Add(0x00FDCB, new Instruction(_ => PREFIX(0xFDCB), "PREFIX 0xFDCB", 0x00FDCB, 2, 0, true));

        _instructions.Add(0x00FDCC, new Instruction(p => CALL_F_nn(Flag.Zero, p), "CALL Z, nn", 0x00FDCC, 2, 4));

        _instructions.Add(0x00FDCD, new Instruction(p => CALL_nn(p), "CALL nn", 0x00FDCD, 2, 4));

        _instructions.Add(0x00FDCE, new Instruction(p => ADC_R_n(Register.A, p), "ADC A, n", 0x00FDCE, 1, 4));

        _instructions.Add(0x00FDCF, new Instruction(_ => RST(0x08), "RST 0x08", 0x00FDCF, 0, 4));

        _instructions.Add(0x00FDD0, new Instruction(_ => RET_F(Flag.Carry, true), "RET NC", 0x00FDD0, 0, 4));

        _instructions.Add(0x00FDD1, new Instruction(_ => POP_RR(RegisterPair.DE), "POP DE", 0x00FDD1, 0, 4));

        _instructions.Add(0x00FDD2, new Instruction(p => JP_F_nn(Flag.Carry, p, true), "JP NC, nn", 0x00FDD2, 2, 4));

        _instructions.Add(0x00FDD3, new Instruction(p => OUT_an_R(p, Register.A), "OUT (n), A", 0x00FDD3, 1, 4));

        _instructions.Add(0x00FDD4, new Instruction(p => CALL_F_nn(Flag.Carry, p, true), "CALL NC, nn", 0x00FDD4, 2, 4));

        _instructions.Add(0x00FDD5, new Instruction(_ => PUSH_RR(RegisterPair.DE), "PUSH DE", 0x00FDD5, 0, 4));

        _instructions.Add(0x00FDD6, new Instruction(p => SUB_R_n(Register.A, p), "SUB A, n", 0x00FDD6, 1, 4));

        _instructions.Add(0x00FDD7, new Instruction(_ => RST(0x10), "RST 0x10", 0x00FDD7, 0, 4));

        _instructions.Add(0x00FDD8, new Instruction(_ => RET_F(Flag.Carry), "RET C", 0x00FDD8, 0, 4));

        _instructions.Add(0x00FDD9, new Instruction(_ => EXX(), "EXX", 0x00FDD9, 0, 4));

        _instructions.Add(0x00FDDA, new Instruction(p => JP_F_nn(Flag.Carry, p), "JP C, nn", 0x00FDDA, 2, 4));

        _instructions.Add(0x00FDDB, new Instruction(p => IN_R_n(Register.A, p), "IN A, n", 0x00FDDB, 1, 4));

        _instructions.Add(0x00FDDC, new Instruction(p => CALL_F_nn(Flag.Carry, p), "CALL C, nn", 0x00FDDC, 2, 4));

        _instructions.Add(0x00FDDD, new Instruction(_ => PREFIX(0xDD), "PREFIX 0xDD", 0x00FDDD, 0, 4, true));

        _instructions.Add(0x00FDDE, new Instruction(p => SBC_R_n(Register.A, p), "SBC A, n", 0x00FDDE, 1, 4));

        _instructions.Add(0x00FDDF, new Instruction(_ => RST(0x18), "RST 0x18", 0x00FDDF, 0, 4));

        _instructions.Add(0x00FDE0, new Instruction(_ => RET_F(Flag.ParityOverflow, true), "RET PO", 0x00FDE0, 0, 4));

        _instructions.Add(0x00FDE1, new Instruction(_ => POP_RR(RegisterPair.IY), "POP IY", 0x00FDE1, 0, 4));

        _instructions.Add(0x00FDE2, new Instruction(p => JP_F_nn(Flag.ParityOverflow, p, true), "JP PO, nn", 0x00FDE2, 2, 4));

        _instructions.Add(0x00FDE3, new Instruction(_ => EX_aRR_RR(RegisterPair.SP, RegisterPair.IY), "EX (SP), IY", 0x00FDE3, 0, 4));

        _instructions.Add(0x00FDE4, new Instruction(p => CALL_F_nn(Flag.ParityOverflow, p, true), "CALL PO, nn", 0x00FDE4, 2, 4));

        _instructions.Add(0x00FDE5, new Instruction(_ => PUSH_RR(RegisterPair.IY), "PUSH IY", 0x00FDE5, 0, 4));

        _instructions.Add(0x00FDE6, new Instruction(p => AND_R_n(Register.A, p), "AND A, n", 0x00FDE6, 1, 4));

        _instructions.Add(0x00FDE7, new Instruction(_ => RST(0x20), "RST 0x20", 0x00FDE7, 0, 4));

        _instructions.Add(0x00FDE8, new Instruction(_ => RET_F(Flag.ParityOverflow), "RET PE", 0x00FDE8, 0, 4));

        _instructions.Add(0x00FDE9, new Instruction(_ => JP_aRR(RegisterPair.IY), "JP (IY)", 0x00FDE9, 0, 4));

        _instructions.Add(0x00FDEA, new Instruction(p => JP_F_nn(Flag.ParityOverflow, p), "JP PE, nn", 0x00FDEA, 2, 4));

        _instructions.Add(0x00FDEB, new Instruction(_ => EX_RR_RR(RegisterPair.DE, RegisterPair.HL), "EX DE, HL", 0x00FDEB, 0, 4));

        _instructions.Add(0x00FDEC, new Instruction(p => CALL_F_nn(Flag.ParityOverflow, p), "CALL PE, nn", 0x00FDEC, 2, 4));

        _instructions.Add(0x00FDED, new Instruction(_ => PREFIX(0xED), "PREFIX 0xED", 0x00FDED, 0, 4, true));

        _instructions.Add(0x00FDEE, new Instruction(p => XOR_R_n(Register.A, p), "XOR A, n", 0x00FDEE, 1, 4));

        _instructions.Add(0x00FDEF, new Instruction(_ => RST(0x28), "RST 0x28", 0x00FDEF, 0, 4));

        _instructions.Add(0x00FDF0, new Instruction(_ => RET_F(Flag.Sign, true), "RET NS", 0x00FDF0, 0, 4));

        _instructions.Add(0x00FDF1, new Instruction(_ => POP_RR(RegisterPair.AF), "POP AF", 0x00FDF1, 0, 4));

        _instructions.Add(0x00FDF2, new Instruction(p => JP_F_nn(Flag.Sign, p, true), "JP NS, nn", 0x00FDF2, 2, 4));

        _instructions.Add(0x00FDF3, new Instruction(_ => DI(), "DI", 0x00FDF3, 0, 4));

        _instructions.Add(0x00FDF4, new Instruction(p => CALL_F_nn(Flag.Sign, p, true), "CALL NS, nn", 0x00FDF4, 2, 4));

        _instructions.Add(0x00FDF5, new Instruction(_ => PUSH_RR(RegisterPair.AF), "PUSH AF", 0x00FDF5, 0, 4));

        _instructions.Add(0x00FDF6, new Instruction(p => OR_R_n(Register.A, p), "OR A, n", 0x00FDF6, 1, 4));

        _instructions.Add(0x00FDF7, new Instruction(_ => RST(0x30), "RST 0x30", 0x00FDF7, 0, 4));

        _instructions.Add(0x00FDF8, new Instruction(_ => RET_F(Flag.Sign), "RET S", 0x00FDF8, 0, 4));

        _instructions.Add(0x00FDF9, new Instruction(_ => LD_RR_RR(RegisterPair.SP, RegisterPair.IY), "LD SP, IY", 0x00FDF9, 0, 4));

        _instructions.Add(0x00FDFA, new Instruction(p => JP_F_nn(Flag.Sign, p), "JP S, nn", 0x00FDFA, 2, 4));

        _instructions.Add(0x00FDFB, new Instruction(_ => EI(), "EI", 0x00FDFB, 0, 4));

        _instructions.Add(0x00FDFC, new Instruction(p => CALL_F_nn(Flag.Sign, p), "CALL S, nn", 0x00FDFC, 2, 4));

        _instructions.Add(0x00FDFD, new Instruction(_ => PREFIX(0xFD), "PREFIX 0xFD", 0x00FDFD, 0, 4, true));

        _instructions.Add(0x00FDFE, new Instruction(p => CP_R_n(Register.A, p), "CP A, n", 0x00FDFE, 1, 4));

        _instructions.Add(0x00FDFF, new Instruction(_ => RST(0x38), "RST 0x38", 0x00FDFF, 0, 4));
    }
}