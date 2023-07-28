using Zen.Z80.Processor;

// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    public void Initialise0xED()
    {
        _instructions.Add(0x00ED00, new Instruction(_ => NOP(), "NOP", 0x00ED00, 0, 4));

        _instructions.Add(0x00ED01, new Instruction(p => LD_RR_nn(RegisterPair.BC, p), "LD BC, nn", 0x00ED01, 2, 6));

        _instructions.Add(0x00ED02, new Instruction(_ => LD_aRR_R(RegisterPair.BC, Register.A), "LD (BC), A", 0x00ED02, 0, 4));

        _instructions.Add(0x00ED03, new Instruction(_ => INC_RR(RegisterPair.BC), "INC BC", 0x00ED03, 0, 4));

        _instructions.Add(0x00ED04, new Instruction(_ => INC_R(Register.B), "INC B", 0x00ED04, 0, 4));

        _instructions.Add(0x00ED05, new Instruction(_ => DEC_R(Register.B), "DEC B", 0x00ED05, 0, 4));

        _instructions.Add(0x00ED06, new Instruction(p => LD_R_n(Register.B, p), "LD B, n", 0x00ED06, 1, 5));

        _instructions.Add(0x00ED07, new Instruction(_ => RLCA(), "RLCA", 0x00ED07, 0, 4));

        _instructions.Add(0x00ED08, new Instruction(_ => EX_RR_RR(RegisterPair.AF, RegisterPair.AF_), "EX AF, AF'", 0x00ED08, 0, 4));

        _instructions.Add(0x00ED09, new Instruction(_ => ADD_RR_RR(RegisterPair.HL, RegisterPair.BC), "ADD HL, BC", 0x00ED09, 0, 4));

        _instructions.Add(0x00ED0A, new Instruction(_ => LD_R_aRR(Register.A, RegisterPair.BC), "LD A, (BC)", 0x00ED0A, 0, 4));

        _instructions.Add(0x00ED0B, new Instruction(_ => DEC_RR(RegisterPair.BC), "DEC BC", 0x00ED0B, 0, 4));

        _instructions.Add(0x00ED0C, new Instruction(_ => INC_R(Register.C), "INC C", 0x00ED0C, 0, 4));

        _instructions.Add(0x00ED0D, new Instruction(_ => DEC_R(Register.C), "DEC C", 0x00ED0D, 0, 4));

        _instructions.Add(0x00ED0E, new Instruction(p => LD_R_n(Register.C, p), "LD C, n", 0x00ED0E, 1, 5));

        _instructions.Add(0x00ED0F, new Instruction(_ => RRCA(), "RRCA", 0x00ED0F, 0, 4));

        _instructions.Add(0x00ED10, new Instruction(DJNZ_e, "DJNZ e", 0x00ED10, 1, 5));

        _instructions.Add(0x00ED11, new Instruction(p => LD_RR_nn(RegisterPair.DE, p), "LD DE, nn", 0x00ED11, 2, 6));

        _instructions.Add(0x00ED12, new Instruction(_ => LD_aRR_R(RegisterPair.DE, Register.A), "LD (DE), A", 0x00ED12, 0, 4));

        _instructions.Add(0x00ED13, new Instruction(_ => INC_RR(RegisterPair.DE), "INC DE", 0x00ED13, 0, 4));

        _instructions.Add(0x00ED14, new Instruction(_ => INC_R(Register.D), "INC D", 0x00ED14, 0, 4));

        _instructions.Add(0x00ED15, new Instruction(_ => DEC_R(Register.D), "DEC D", 0x00ED15, 0, 4));

        _instructions.Add(0x00ED16, new Instruction(p => LD_R_n(Register.D, p), "LD D, n", 0x00ED16, 1, 5));

        _instructions.Add(0x00ED17, new Instruction(_ => RLA(), "RLA", 0x00ED17, 0, 4));

        _instructions.Add(0x00ED18, new Instruction(JR_e, "JR e", 0x00ED18, 1, 5));

        _instructions.Add(0x00ED19, new Instruction(_ => ADD_RR_RR(RegisterPair.HL, RegisterPair.DE), "ADD HL, DE", 0x00ED19, 0, 4));

        _instructions.Add(0x00ED1A, new Instruction(_ => LD_R_aRR(Register.A, RegisterPair.DE), "LD A, (DE)", 0x00ED1A, 0, 4));

        _instructions.Add(0x00ED1B, new Instruction(_ => DEC_RR(RegisterPair.DE), "DEC DE", 0x00ED1B, 0, 4));

        _instructions.Add(0x00ED1C, new Instruction(_ => INC_R(Register.E), "INC E", 0x00ED1C, 0, 4));

        _instructions.Add(0x00ED1D, new Instruction(_ => DEC_R(Register.E), "DEC E", 0x00ED1D, 0, 4));

        _instructions.Add(0x00ED1E, new Instruction(p => LD_R_n(Register.E, p), "LD E, n", 0x00ED1E, 1, 5));

        _instructions.Add(0x00ED1F, new Instruction(_ => RRA(), "RRA", 0x00ED1F, 0, 4));

        _instructions.Add(0x00ED20, new Instruction(p => JR_F_e(Flag.Zero, p, true), "JR NZ, e", 0x00ED20, 1, 5));

        _instructions.Add(0x00ED21, new Instruction(p => LD_RR_nn(RegisterPair.HL, p), "LD HL, nn", 0x00ED21, 2, 6));

        _instructions.Add(0x00ED22, new Instruction(p => LD_ann_RR(p, RegisterPair.HL), "LD (nn), HL", 0x00ED22, 2, 6));

        _instructions.Add(0x00ED23, new Instruction(_ => INC_RR(RegisterPair.HL), "INC HL", 0x00ED23, 0, 4));

        _instructions.Add(0x00ED24, new Instruction(_ => INC_R(Register.H), "INC H", 0x00ED24, 0, 4));

        _instructions.Add(0x00ED25, new Instruction(_ => DEC_R(Register.H), "DEC H", 0x00ED25, 0, 4));

        _instructions.Add(0x00ED26, new Instruction(p => LD_R_n(Register.H, p), "LD H, n", 0x00ED26, 1, 5));

        _instructions.Add(0x00ED27, new Instruction(_ => DAA(), "DAA", 0x00ED27, 0, 4));

        _instructions.Add(0x00ED28, new Instruction(p => JR_F_e(Flag.Zero, p), "JR Z, e", 0x00ED28, 1, 5));

        _instructions.Add(0x00ED29, new Instruction(_ => ADD_RR_RR(RegisterPair.HL, RegisterPair.HL), "ADD HL, HL", 0x00ED29, 0, 4));

        _instructions.Add(0x00ED2A, new Instruction(p => LD_RR_ann(RegisterPair.HL, p), "LD HL, (nn)", 0x00ED2A, 2, 6));

        _instructions.Add(0x00ED2B, new Instruction(_ => DEC_RR(RegisterPair.HL), "DEC HL", 0x00ED2B, 0, 4));

        _instructions.Add(0x00ED2C, new Instruction(_ => INC_R(Register.L), "INC L", 0x00ED2C, 0, 4));

        _instructions.Add(0x00ED2D, new Instruction(_ => DEC_R(Register.L), "DEC L", 0x00ED2D, 0, 4));

        _instructions.Add(0x00ED2E, new Instruction(p => LD_R_n(Register.L, p), "LD L, n", 0x00ED2E, 1, 5));

        _instructions.Add(0x00ED2F, new Instruction(_ => CPL(), "CPL", 0x00ED2F, 0, 4));

        _instructions.Add(0x00ED30, new Instruction(p => JR_F_e(Flag.Carry, p, true), "JR NC, e", 0x00ED30, 1, 5));

        _instructions.Add(0x00ED31, new Instruction(p => LD_RR_nn(RegisterPair.SP, p), "LD SP, nn", 0x00ED31, 2, 6));

        _instructions.Add(0x00ED32, new Instruction(p => LD_ann_R(p, Register.A), "LD (nn), A", 0x00ED32, 2, 6));

        _instructions.Add(0x00ED33, new Instruction(_ => INC_RR(RegisterPair.SP), "INC SP", 0x00ED33, 0, 4));

        _instructions.Add(0x00ED34, new Instruction(_ => INC_aRR(RegisterPair.HL), "INC (HL)", 0x00ED34, 0, 4));

        _instructions.Add(0x00ED35, new Instruction(_ => DEC_aRR(RegisterPair.HL), "DEC (HL)", 0x00ED35, 0, 4));

        _instructions.Add(0x00ED36, new Instruction(p => LD_aRR_n(RegisterPair.HL, p), "LD (HL), n", 0x00ED36, 1, 5));

        _instructions.Add(0x00ED37, new Instruction(_ => SCF(), "SCF", 0x00ED37, 0, 4));

        _instructions.Add(0x00ED38, new Instruction(p => JR_F_e(Flag.Carry, p), "JR C, e", 0x00ED38, 1, 5));

        _instructions.Add(0x00ED39, new Instruction(_ => ADD_RR_RR(RegisterPair.HL, RegisterPair.SP), "ADD HL, SP", 0x00ED39, 0, 4));

        _instructions.Add(0x00ED3A, new Instruction(p => LD_R_ann(Register.A, p), "LD A, (nn)", 0x00ED3A, 2, 6));

        _instructions.Add(0x00ED3B, new Instruction(_ => DEC_RR(RegisterPair.SP), "DEC SP", 0x00ED3B, 0, 4));

        _instructions.Add(0x00ED3C, new Instruction(_ => INC_R(Register.A), "INC A", 0x00ED3C, 0, 4));

        _instructions.Add(0x00ED3D, new Instruction(_ => DEC_R(Register.A), "DEC A", 0x00ED3D, 0, 4));

        _instructions.Add(0x00ED3E, new Instruction(p => LD_R_n(Register.A, p), "LD A, n", 0x00ED3E, 1, 5));

        _instructions.Add(0x00ED3F, new Instruction(_ => CCF(), "CCF", 0x00ED3F, 0, 4));

        _instructions.Add(0x00ED40, new Instruction(_ => IN_R_C(Register.B), "IN B, (C)", 0x00ED40, 0));

        _instructions.Add(0x00ED41, new Instruction(_ => OUT_C_R(Register.B), "OUT (C), B", 0x00ED41, 0));

        _instructions.Add(0x00ED42, new Instruction(_ => SBC_RR_RR(RegisterPair.HL, RegisterPair.BC), "SBC HL, BC", 0x00ED42, 0));

        _instructions.Add(0x00ED43, new Instruction(p => LD_ann_RR(p, RegisterPair.BC), "LD (nn), BC", 0x00ED43, 2, 4));

        _instructions.Add(0x00ED44, new Instruction(_ => NEG_R(Register.A), "NEG A", 0x00ED44, 0));

        _instructions.Add(0x00ED45, new Instruction(_ => RETN(), "RETN", 0x00ED45, 0));

        _instructions.Add(0x00ED46, new Instruction(_ => IM(InterruptMode.IM0), "IM 0", 0x00ED46, 0));

        _instructions.Add(0x00ED47, new Instruction(_ => LD_R_R(Register.I, Register.A), "LD I, A", 0x00ED47, 0));

        _instructions.Add(0x00ED48, new Instruction(_ => IN_R_C(Register.C), "IN C, (C)", 0x00ED48, 0));

        _instructions.Add(0x00ED49, new Instruction(_ => OUT_C_R(Register.C), "OUT (C), C", 0x00ED49, 0));

        _instructions.Add(0x00ED4A, new Instruction(_ => ADC_RR_RR(RegisterPair.HL, RegisterPair.BC), "ADC HL, BC", 0x00ED4A, 0));

        _instructions.Add(0x00ED4B, new Instruction(p => LD_RR_ann(RegisterPair.BC, p), "LD BC, (nn)", 0x00ED4B, 2, 4));

        _instructions.Add(0x00ED4C, new Instruction(_ => NEG_R(Register.A), "NEG A", 0x00ED4C, 0));

        _instructions.Add(0x00ED4D, new Instruction(_ => RETI(), "RETI", 0x00ED4D, 0));

        _instructions.Add(0x00ED4E, new Instruction(_ => IM(InterruptMode.IM0), "IM 0", 0x00ED4E, 0));

        _instructions.Add(0x00ED4F, new Instruction(_ => LD_R_R(Register.R, Register.A), "LD R, A", 0x00ED4F, 0));

        _instructions.Add(0x00ED50, new Instruction(_ => IN_R_C(Register.D), "IN D, (C)", 0x00ED50, 0));

        _instructions.Add(0x00ED51, new Instruction(_ => OUT_C_R(Register.D), "OUT (C), D", 0x00ED51, 0));

        _instructions.Add(0x00ED52, new Instruction(_ => SBC_RR_RR(RegisterPair.HL, RegisterPair.DE), "SBC HL, DE", 0x00ED52, 0));

        _instructions.Add(0x00ED53, new Instruction(p => LD_ann_RR(p, RegisterPair.DE), "LD (nn), DE", 0x00ED53, 2, 4));

        _instructions.Add(0x00ED54, new Instruction(_ => NEG_R(Register.A), "NEG A", 0x00ED54, 0));

        _instructions.Add(0x00ED55, new Instruction(_ => RETN(), "RETN", 0x00ED55, 0));

        _instructions.Add(0x00ED56, new Instruction(_ => IM(InterruptMode.IM1), "IM 1", 0x00ED56, 0));

        _instructions.Add(0x00ED57, new Instruction(_ => LD_R_R(Register.A, Register.I), "LD A, I", 0x00ED57, 0));

        _instructions.Add(0x00ED58, new Instruction(_ => IN_R_C(Register.E), "IN E, (C)", 0x00ED58, 0));

        _instructions.Add(0x00ED59, new Instruction(_ => OUT_C_R(Register.E), "OUT (C), E", 0x00ED59, 0));

        _instructions.Add(0x00ED5A, new Instruction(_ => ADC_RR_RR(RegisterPair.HL, RegisterPair.DE), "ADC HL, DE", 0x00ED5A, 0));

        _instructions.Add(0x00ED5B, new Instruction(p => LD_RR_ann(RegisterPair.DE, p), "LD DE, (nn)", 0x00ED5B, 2, 4));

        _instructions.Add(0x00ED5C, new Instruction(_ => NEG_R(Register.A), "NEG A", 0x00ED5C, 0));

        _instructions.Add(0x00ED5D, new Instruction(_ => RETN(), "RETN", 0x00ED5D, 0));

        _instructions.Add(0x00ED5E, new Instruction(_ => IM(InterruptMode.IM2), "IM 2", 0x00ED5E, 0));

        _instructions.Add(0x00ED5F, new Instruction(_ => LD_R_R(Register.A, Register.R), "LD A, R", 0x00ED5F, 0));

        _instructions.Add(0x00ED60, new Instruction(_ => IN_R_C(Register.H), "IN H, (C)", 0x00ED60, 0));

        _instructions.Add(0x00ED61, new Instruction(_ => OUT_C_R(Register.H), "OUT (C), H", 0x00ED61, 0));

        _instructions.Add(0x00ED62, new Instruction(_ => SBC_RR_RR(RegisterPair.HL, RegisterPair.HL), "SBC HL, HL", 0x00ED62, 0));

        _instructions.Add(0x00ED63, new Instruction(p => LD_ann_RR(p, RegisterPair.HL), "LD (nn), HL", 0x00ED63, 2, 4));

        _instructions.Add(0x00ED64, new Instruction(_ => NEG_R(Register.A), "NEG A", 0x00ED64, 0));

        _instructions.Add(0x00ED65, new Instruction(_ => RETN(), "RETN", 0x00ED65, 0));

        _instructions.Add(0x00ED66, new Instruction(_ => IM(InterruptMode.IM0), "IM 0", 0x00ED66, 0));

        _instructions.Add(0x00ED67, new Instruction(_ => RRD(), "RRD", 0x00ED67, 0));

        _instructions.Add(0x00ED68, new Instruction(_ => IN_R_C(Register.L), "IN L, (C)", 0x00ED68, 0));

        _instructions.Add(0x00ED69, new Instruction(_ => OUT_C_R(Register.L), "OUT (C), L", 0x00ED69, 0));

        _instructions.Add(0x00ED6A, new Instruction(_ => ADC_RR_RR(RegisterPair.HL, RegisterPair.HL), "ADC HL, HL", 0x00ED6A, 0));

        _instructions.Add(0x00ED6B, new Instruction(p => LD_RR_ann(RegisterPair.HL, p), "LD HL, (nn)", 0x00ED6B, 2, 4));

        _instructions.Add(0x00ED6C, new Instruction(_ => NEG_R(Register.A), "NEG A", 0x00ED6C, 0));

        _instructions.Add(0x00ED6D, new Instruction(_ => RETN(), "RETN", 0x00ED6D, 0));

        _instructions.Add(0x00ED6E, new Instruction(_ => IM(InterruptMode.IM0), "IM 0", 0x00ED6E, 0));

        _instructions.Add(0x00ED6F, new Instruction(_ => RLD(), "RLD", 0x00ED6F, 0));

        _instructions.Add(0x00ED70, new Instruction(_ => IN_C(), "IN (C)", 0x00ED70, 0));

        _instructions.Add(0x00ED71, new Instruction(_ => OUT_C_b(0x00), "OUT (C), 0", 0x00ED71, 0));

        _instructions.Add(0x00ED72, new Instruction(_ => SBC_RR_RR(RegisterPair.HL, RegisterPair.SP), "SBC HL, SP", 0x00ED72, 0));

        _instructions.Add(0x00ED73, new Instruction(p => LD_ann_RR(p, RegisterPair.SP), "LD (nn), SP", 0x00ED73, 2, 4));

        _instructions.Add(0x00ED74, new Instruction(_ => NEG_R(Register.A), "NEG A", 0x00ED74, 0));

        _instructions.Add(0x00ED75, new Instruction(_ => RETN(), "RETN", 0x00ED75, 0));

        _instructions.Add(0x00ED76, new Instruction(_ => HALT(), "HALT", 0x00ED76, 0, 4));

        _instructions.Add(0x00ED77, new Instruction(_ => NOP(), "NOP", 0x00ED77, 0, 4));

        _instructions.Add(0x00ED78, new Instruction(_ => IN_R_C(Register.A), "IN A, (C)", 0x00ED78, 0));

        _instructions.Add(0x00ED79, new Instruction(_ => OUT_C_R(Register.A), "OUT (C), A", 0x00ED79, 0));

        _instructions.Add(0x00ED7A, new Instruction(_ => ADC_RR_RR(RegisterPair.HL, RegisterPair.SP), "ADC HL, SP", 0x00ED7A, 0));

        _instructions.Add(0x00ED7B, new Instruction(p => LD_RR_ann(RegisterPair.SP, p), "LD SP, (nn)", 0x00ED7B, 2, 4));

        _instructions.Add(0x00ED7C, new Instruction(_ => NEG_R(Register.A), "NEG A", 0x00ED7C, 0));

        _instructions.Add(0x00ED7D, new Instruction(_ => RETN(), "RETN", 0x00ED7D, 0));

        _instructions.Add(0x00ED7E, new Instruction(_ => IM(InterruptMode.IM2), "IM 2", 0x00ED7E, 0));

        _instructions.Add(0x00ED7F, new Instruction(_ => LD_R_R(Register.A, Register.A), "LD A, A", 0x00ED7F, 0, 4));

        _instructions.Add(0x00ED80, new Instruction(_ => ADD_R_R(Register.A, Register.B), "ADD A, B", 0x00ED80, 0, 4));

        _instructions.Add(0x00ED81, new Instruction(_ => ADD_R_R(Register.A, Register.C), "ADD A, C", 0x00ED81, 0, 4));

        _instructions.Add(0x00ED82, new Instruction(_ => ADD_R_R(Register.A, Register.D), "ADD A, D", 0x00ED82, 0, 4));

        _instructions.Add(0x00ED83, new Instruction(_ => ADD_R_R(Register.A, Register.E), "ADD A, E", 0x00ED83, 0, 4));

        _instructions.Add(0x00ED84, new Instruction(_ => ADD_R_R(Register.A, Register.H), "ADD A, H", 0x00ED84, 0, 4));

        _instructions.Add(0x00ED85, new Instruction(_ => ADD_R_R(Register.A, Register.L), "ADD A, L", 0x00ED85, 0, 4));

        _instructions.Add(0x00ED86, new Instruction(_ => ADD_R_aRR(Register.A, RegisterPair.HL), "ADD A, (HL)", 0x00ED86, 0, 4));

        _instructions.Add(0x00ED87, new Instruction(_ => ADD_R_R(Register.A, Register.A), "ADD A, A", 0x00ED87, 0, 4));

        _instructions.Add(0x00ED88, new Instruction(_ => ADC_R_R(Register.A, Register.B), "ADC A, B", 0x00ED88, 0, 4));

        _instructions.Add(0x00ED89, new Instruction(_ => ADC_R_R(Register.A, Register.C), "ADC A, C", 0x00ED89, 0, 4));

        _instructions.Add(0x00ED8A, new Instruction(_ => ADC_R_R(Register.A, Register.D), "ADC A, D", 0x00ED8A, 0, 4));

        _instructions.Add(0x00ED8B, new Instruction(_ => ADC_R_R(Register.A, Register.E), "ADC A, E", 0x00ED8B, 0, 4));

        _instructions.Add(0x00ED8C, new Instruction(_ => ADC_R_R(Register.A, Register.H), "ADC A, H", 0x00ED8C, 0, 4));

        _instructions.Add(0x00ED8D, new Instruction(_ => ADC_R_R(Register.A, Register.L), "ADC A, L", 0x00ED8D, 0, 4));

        _instructions.Add(0x00ED8E, new Instruction(_ => ADC_R_aRR(Register.A, RegisterPair.HL), "ADC A, (HL)", 0x00ED8E, 0, 4));

        _instructions.Add(0x00ED8F, new Instruction(_ => ADC_R_R(Register.A, Register.A), "ADC A, A", 0x00ED8F, 0, 4));

        _instructions.Add(0x00ED90, new Instruction(_ => SUB_R_R(Register.A, Register.B), "SUB A, B", 0x00ED90, 0, 4));

        _instructions.Add(0x00ED91, new Instruction(_ => SUB_R_R(Register.A, Register.C), "SUB A, C", 0x00ED91, 0, 4));

        _instructions.Add(0x00ED92, new Instruction(_ => SUB_R_R(Register.A, Register.D), "SUB A, D", 0x00ED92, 0, 4));

        _instructions.Add(0x00ED93, new Instruction(_ => SUB_R_R(Register.A, Register.E), "SUB A, E", 0x00ED93, 0, 4));

        _instructions.Add(0x00ED94, new Instruction(_ => SUB_R_R(Register.A, Register.H), "SUB A, H", 0x00ED94, 0, 4));

        _instructions.Add(0x00ED95, new Instruction(_ => SUB_R_R(Register.A, Register.L), "SUB A, L", 0x00ED95, 0, 4));

        _instructions.Add(0x00ED96, new Instruction(_ => SUB_R_aRR(Register.A, RegisterPair.HL), "SUB A, (HL)", 0x00ED96, 0, 4));

        _instructions.Add(0x00ED97, new Instruction(_ => SUB_R_R(Register.A, Register.A), "SUB A, A", 0x00ED97, 0, 4));

        _instructions.Add(0x00ED98, new Instruction(_ => SBC_R_R(Register.A, Register.B), "SBC A, B", 0x00ED98, 0, 4));

        _instructions.Add(0x00ED99, new Instruction(_ => SBC_R_R(Register.A, Register.C), "SBC A, C", 0x00ED99, 0, 4));

        _instructions.Add(0x00ED9A, new Instruction(_ => SBC_R_R(Register.A, Register.D), "SBC A, D", 0x00ED9A, 0, 4));

        _instructions.Add(0x00ED9B, new Instruction(_ => SBC_R_R(Register.A, Register.E), "SBC A, E", 0x00ED9B, 0, 4));

        _instructions.Add(0x00ED9C, new Instruction(_ => SBC_R_R(Register.A, Register.H), "SBC A, H", 0x00ED9C, 0, 4));

        _instructions.Add(0x00ED9D, new Instruction(_ => SBC_R_R(Register.A, Register.L), "SBC A, L", 0x00ED9D, 0, 4));

        _instructions.Add(0x00ED9E, new Instruction(_ => SBC_R_aRR(Register.A, RegisterPair.HL), "SBC A, (HL)", 0x00ED9E, 0, 4));

        _instructions.Add(0x00ED9F, new Instruction(_ => SBC_R_R(Register.A, Register.A), "SBC A, A", 0x00ED9F, 0, 4));

        _instructions.Add(0x00EDA0, new Instruction(_ => LDI(), "LDI", 0x00EDA0, 0));

        _instructions.Add(0x00EDA1, new Instruction(_ => CPI(), "CPI", 0x00EDA1, 0));

        _instructions.Add(0x00EDA2, new Instruction(_ => INI(), "INI", 0x00EDA2, 0));

        _instructions.Add(0x00EDA3, new Instruction(_ => OUTI(), "OUTI", 0x00EDA3, 0));

        _instructions.Add(0x00EDA4, new Instruction(_ => AND_R_R(Register.A, Register.H), "AND A, H", 0x00EDA4, 0, 4));

        _instructions.Add(0x00EDA5, new Instruction(_ => AND_R_R(Register.A, Register.L), "AND A, L", 0x00EDA5, 0, 4));

        _instructions.Add(0x00EDA6, new Instruction(_ => AND_R_aRR(Register.A, RegisterPair.HL), "AND A, (HL)", 0x00EDA6, 0, 4));

        _instructions.Add(0x00EDA7, new Instruction(_ => AND_R_R(Register.A, Register.A), "AND A, A", 0x00EDA7, 0, 4));

        _instructions.Add(0x00EDA8, new Instruction(_ => LDD(), "LDD", 0x00EDA8, 0));

        _instructions.Add(0x00EDA9, new Instruction(_ => CPD(), "CPD", 0x00EDA9, 0));

        _instructions.Add(0x00EDAA, new Instruction(_ => IND(), "IND", 0x00EDAA, 0));

        _instructions.Add(0x00EDAB, new Instruction(_ => OUTD(), "OUTD", 0x00EDAB, 0));

        _instructions.Add(0x00EDAC, new Instruction(_ => XOR_R_R(Register.A, Register.H), "XOR A, H", 0x00EDAC, 0, 4));

        _instructions.Add(0x00EDAD, new Instruction(_ => XOR_R_R(Register.A, Register.L), "XOR A, L", 0x00EDAD, 0, 4));

        _instructions.Add(0x00EDAE, new Instruction(_ => XOR_R_aRR(Register.A, RegisterPair.HL), "XOR A, (HL)", 0x00EDAE, 0, 4));

        _instructions.Add(0x00EDAF, new Instruction(_ => XOR_R_R(Register.A, Register.A), "XOR A, A", 0x00EDAF, 0, 4));

        _instructions.Add(0x00EDB0, new Instruction(_ => LDIR(), "LDIR", 0x00EDB0, 0));

        _instructions.Add(0x00EDB1, new Instruction(_ => CPIR(), "CPIR", 0x00EDB1, 0));

        _instructions.Add(0x00EDB2, new Instruction(_ => INIR(), "INIR", 0x00EDB2, 0));

        _instructions.Add(0x00EDB3, new Instruction(_ => OTIR(), "OTIR", 0x00EDB3, 0));

        _instructions.Add(0x00EDB4, new Instruction(_ => OR_R_R(Register.A, Register.H), "OR A, H", 0x00EDB4, 0, 4));

        _instructions.Add(0x00EDB5, new Instruction(_ => OR_R_R(Register.A, Register.L), "OR A, L", 0x00EDB5, 0, 4));

        _instructions.Add(0x00EDB6, new Instruction(_ => OR_R_aRR(Register.A, RegisterPair.HL), "OR A, (HL)", 0x00EDB6, 0, 4));

        _instructions.Add(0x00EDB7, new Instruction(_ => OR_R_R(Register.A, Register.A), "OR A, A", 0x00EDB7, 0, 4));

        _instructions.Add(0x00EDB8, new Instruction(_ => LDDR(), "LDDR", 0x00EDB8, 0));

        _instructions.Add(0x00EDB9, new Instruction(_ => CPDR(), "CPDR", 0x00EDB9, 0));

        _instructions.Add(0x00EDBA, new Instruction(_ => INDR(), "INDR", 0x00EDBA, 0));

        _instructions.Add(0x00EDBB, new Instruction(_ => OTDR(), "OTDR", 0x00EDBB, 0));

        _instructions.Add(0x00EDBC, new Instruction(_ => CP_R_R(Register.A, Register.H), "CP A, H", 0x00EDBC, 0, 4));

        _instructions.Add(0x00EDBD, new Instruction(_ => CP_R_R(Register.A, Register.L), "CP A, L", 0x00EDBD, 0, 4));

        _instructions.Add(0x00EDBE, new Instruction(_ => CP_R_aRR(Register.A, RegisterPair.HL), "CP A, (HL)", 0x00EDBE, 0, 4));

        _instructions.Add(0x00EDBF, new Instruction(_ => CP_R_R(Register.A, Register.A), "CP A, A", 0x00EDBF, 0, 4));

        _instructions.Add(0x00EDC0, new Instruction(_ => RET_F(Flag.Zero, true), "RET NZ", 0x00EDC0, 0, 4));

        _instructions.Add(0x00EDC1, new Instruction(_ => POP_RR(RegisterPair.BC), "POP BC", 0x00EDC1, 0, 4));

        _instructions.Add(0x00EDC2, new Instruction(p => JP_F_nn(Flag.Zero, p, true), "JP NZ, nn", 0x00EDC2, 2, 6));

        _instructions.Add(0x00EDC3, new Instruction(JP_nn, "JP nn", 0x00EDC3, 2, 6));

        _instructions.Add(0x00EDC4, new Instruction(p => CALL_F_nn(Flag.Zero, p, true), "CALL NZ, nn", 0x00EDC4, 2, 6));

        _instructions.Add(0x00EDC5, new Instruction(_ => PUSH_RR(RegisterPair.BC), "PUSH BC", 0x00EDC5, 0, 4));

        _instructions.Add(0x00EDC6, new Instruction(p => ADD_R_n(Register.A, p), "ADD A, n", 0x00EDC6, 1, 5));

        _instructions.Add(0x00EDC7, new Instruction(_ => RST(0x00), "RST 0x00", 0x00EDC7, 0, 4));

        _instructions.Add(0x00EDC8, new Instruction(_ => RET_F(Flag.Zero), "RET Z", 0x00EDC8, 0, 4));

        _instructions.Add(0x00EDC9, new Instruction(_ => RET(), "RET", 0x00EDC9, 0, 4));

        _instructions.Add(0x00EDCA, new Instruction(p => JP_F_nn(Flag.Zero, p), "JP Z, nn", 0x00EDCA, 2, 6));

        _instructions.Add(0x00EDCB, new Instruction(_ => PREFIX(0xCB), "PREFIX 0xCB", 0x00EDCB, 0, 4, true));

        _instructions.Add(0x00EDCC, new Instruction(p => CALL_F_nn(Flag.Zero, p), "CALL Z, nn", 0x00EDCC, 2, 6));

        _instructions.Add(0x00EDCD, new Instruction(CALL_nn, "CALL nn", 0x00EDCD, 2, 6));

        _instructions.Add(0x00EDCE, new Instruction(p => ADC_R_n(Register.A, p), "ADC A, n", 0x00EDCE, 1, 5));

        _instructions.Add(0x00EDCF, new Instruction(_ => RST(0x08), "RST 0x08", 0x00EDCF, 0, 4));

        _instructions.Add(0x00EDD0, new Instruction(_ => RET_F(Flag.Carry, true), "RET NC", 0x00EDD0, 0, 4));

        _instructions.Add(0x00EDD1, new Instruction(_ => POP_RR(RegisterPair.DE), "POP DE", 0x00EDD1, 0, 4));

        _instructions.Add(0x00EDD2, new Instruction(p => JP_F_nn(Flag.Carry, p, true), "JP NC, nn", 0x00EDD2, 2, 6));

        _instructions.Add(0x00EDD3, new Instruction(p => OUT_an_R(p, Register.A), "OUT (n), A", 0x00EDD3, 1, 5));

        _instructions.Add(0x00EDD4, new Instruction(p => CALL_F_nn(Flag.Carry, p, true), "CALL NC, nn", 0x00EDD4, 2, 6));

        _instructions.Add(0x00EDD5, new Instruction(_ => PUSH_RR(RegisterPair.DE), "PUSH DE", 0x00EDD5, 0, 4));

        _instructions.Add(0x00EDD6, new Instruction(p => SUB_R_n(Register.A, p), "SUB A, n", 0x00EDD6, 1, 5));

        _instructions.Add(0x00EDD7, new Instruction(_ => RST(0x10), "RST 0x10", 0x00EDD7, 0, 4));

        _instructions.Add(0x00EDD8, new Instruction(_ => RET_F(Flag.Carry), "RET C", 0x00EDD8, 0, 4));

        _instructions.Add(0x00EDD9, new Instruction(_ => EXX(), "EXX", 0x00EDD9, 0, 4));

        _instructions.Add(0x00EDDA, new Instruction(p => JP_F_nn(Flag.Carry, p), "JP C, nn", 0x00EDDA, 2, 6));

        _instructions.Add(0x00EDDB, new Instruction(p => IN_R_n(Register.A, p), "IN A, n", 0x00EDDB, 1, 5));

        _instructions.Add(0x00EDDC, new Instruction(p => CALL_F_nn(Flag.Carry, p), "CALL C, nn", 0x00EDDC, 2, 6));

        _instructions.Add(0x00EDDD, new Instruction(_ => PREFIX(0xDD), "PREFIX 0xDD", 0x00EDDD, 0, 4, true));

        _instructions.Add(0x00EDDE, new Instruction(p => SBC_R_n(Register.A, p), "SBC A, n", 0x00EDDE, 1, 5));

        _instructions.Add(0x00EDDF, new Instruction(_ => RST(0x18), "RST 0x18", 0x00EDDF, 0, 4));

        _instructions.Add(0x00EDE0, new Instruction(_ => RET_F(Flag.ParityOverflow, true), "RET PO", 0x00EDE0, 0, 4));

        _instructions.Add(0x00EDE1, new Instruction(_ => POP_RR(RegisterPair.HL), "POP HL", 0x00EDE1, 0, 4));

        _instructions.Add(0x00EDE2, new Instruction(p => JP_F_nn(Flag.ParityOverflow, p, true), "JP PO, nn", 0x00EDE2, 2, 6));

        _instructions.Add(0x00EDE3, new Instruction(_ => EX_aRR_RR(RegisterPair.SP, RegisterPair.HL), "EX (SP), HL", 0x00EDE3, 0, 4));

        _instructions.Add(0x00EDE4, new Instruction(p => CALL_F_nn(Flag.ParityOverflow, p, true), "CALL PO, nn", 0x00EDE4, 2, 6));

        _instructions.Add(0x00EDE5, new Instruction(_ => PUSH_RR(RegisterPair.HL), "PUSH HL", 0x00EDE5, 0, 4));

        _instructions.Add(0x00EDE6, new Instruction(p => AND_R_n(Register.A, p), "AND A, n", 0x00EDE6, 1, 5));

        _instructions.Add(0x00EDE7, new Instruction(_ => RST(0x20), "RST 0x20", 0x00EDE7, 0, 4));

        _instructions.Add(0x00EDE8, new Instruction(_ => RET_F(Flag.ParityOverflow), "RET PE", 0x00EDE8, 0, 4));

        _instructions.Add(0x00EDE9, new Instruction(_ => JP_aRR(RegisterPair.HL), "JP (HL)", 0x00EDE9, 0, 4));

        _instructions.Add(0x00EDEA, new Instruction(p => JP_F_nn(Flag.ParityOverflow, p), "JP PE, nn", 0x00EDEA, 2, 6));

        _instructions.Add(0x00EDEB, new Instruction(_ => EX_RR_RR(RegisterPair.DE, RegisterPair.HL), "EX DE, HL", 0x00EDEB, 0, 4));

        _instructions.Add(0x00EDEC, new Instruction(p => CALL_F_nn(Flag.ParityOverflow, p), "CALL PE, nn", 0x00EDEC, 2, 6));

        _instructions.Add(0x00EDED, new Instruction(_ => PREFIX(0xED), "PREFIX 0xED", 0x00EDED, 0, 4, true));

        _instructions.Add(0x00EDEE, new Instruction(p => XOR_R_n(Register.A, p), "XOR A, n", 0x00EDEE, 1, 5));

        _instructions.Add(0x00EDEF, new Instruction(_ => RST(0x28), "RST 0x28", 0x00EDEF, 0, 4));

        _instructions.Add(0x00EDF0, new Instruction(_ => RET_F(Flag.Sign, true), "RET NS", 0x00EDF0, 0, 4));

        _instructions.Add(0x00EDF1, new Instruction(_ => POP_RR(RegisterPair.AF), "POP AF", 0x00EDF1, 0, 4));

        _instructions.Add(0x00EDF2, new Instruction(p => JP_F_nn(Flag.Sign, p, true), "JP NS, nn", 0x00EDF2, 2, 6));

        _instructions.Add(0x00EDF3, new Instruction(_ => DI(), "DI", 0x00EDF3, 0, 4));

        _instructions.Add(0x00EDF4, new Instruction(p => CALL_F_nn(Flag.Sign, p, true), "CALL NS, nn", 0x00EDF4, 2, 6));

        _instructions.Add(0x00EDF5, new Instruction(_ => PUSH_RR(RegisterPair.AF), "PUSH AF", 0x00EDF5, 0, 4));

        _instructions.Add(0x00EDF6, new Instruction(p => OR_R_n(Register.A, p), "OR A, n", 0x00EDF6, 1, 5));

        _instructions.Add(0x00EDF7, new Instruction(_ => RST(0x30), "RST 0x30", 0x00EDF7, 0, 4));

        _instructions.Add(0x00EDF8, new Instruction(_ => RET_F(Flag.Sign), "RET S", 0x00EDF8, 0, 4));

        _instructions.Add(0x00EDF9, new Instruction(_ => LD_RR_RR(RegisterPair.SP, RegisterPair.HL), "LD SP, HL", 0x00EDF9, 0, 4));

        _instructions.Add(0x00EDFA, new Instruction(p => JP_F_nn(Flag.Sign, p), "JP S, nn", 0x00EDFA, 2, 6));

        _instructions.Add(0x00EDFB, new Instruction(_ => EI(), "EI", 0x00EDFB, 0, 4));

        _instructions.Add(0x00EDFC, new Instruction(p => CALL_F_nn(Flag.Sign, p), "CALL S, nn", 0x00EDFC, 2, 6));

        _instructions.Add(0x00EDFD, new Instruction(_ => PREFIX(0xFD), "PREFIX 0xFD", 0x00EDFD, 0, 4, true));

        _instructions.Add(0x00EDFE, new Instruction(p => CP_R_n(Register.A, p), "CP A, n", 0x00EDFE, 1, 5));

        _instructions.Add(0x00EDFF, new Instruction(_ => RST(0x38), "RST 0x38", 0x00EDFF, 0, 4));
    }
}