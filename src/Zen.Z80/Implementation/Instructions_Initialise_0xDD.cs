using Zen.Z80.Processor;

// ReSharper disable ConvertClosureToMethodGroup
// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void Initialise0xDD()
    {
        _instructions.Add(0x00DD00, new Instruction(_ => NOP(), "NOP", 0x00DD00, 0, 4));

        _instructions.Add(0x00DD01, new Instruction(p => LD_RR_nn(RegisterPair.BC, p), "LD BC, nn", 0x00DD01, 2, 4));

        _instructions.Add(0x00DD02, new Instruction(_ => LD_aRR_R(RegisterPair.BC, Register.A), "LD (BC), A", 0x00DD02, 0, 4));

        _instructions.Add(0x00DD03, new Instruction(_ => INC_RR(RegisterPair.BC), "INC BC", 0x00DD03, 0, 4));

        _instructions.Add(0x00DD04, new Instruction(_ => INC_R(Register.B), "INC B", 0x00DD04, 0, 4));

        _instructions.Add(0x00DD05, new Instruction(_ => DEC_R(Register.B), "DEC B", 0x00DD05, 0, 4));

        _instructions.Add(0x00DD06, new Instruction(p => LD_R_n(Register.B, p), "LD B, n", 0x00DD06, 1, 4));

        _instructions.Add(0x00DD07, new Instruction(_ => RLCA(), "RLCA", 0x00DD07, 0, 4));

        _instructions.Add(0x00DD08, new Instruction(_ => EX_RR_RR(RegisterPair.AF, RegisterPair.AF_), "EX AF, AF'", 0x00DD08, 0, 4));

        _instructions.Add(0x00DD09, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.BC), "ADD IX, BC", 0x00DD09, 0, 4));

        _instructions.Add(0x00DD0A, new Instruction(_ => LD_R_aRR(Register.A, RegisterPair.BC), "LD A, (BC)", 0x00DD0A, 0, 4));

        _instructions.Add(0x00DD0B, new Instruction(_ => DEC_RR(RegisterPair.BC), "DEC BC", 0x00DD0B, 0, 4));

        _instructions.Add(0x00DD0C, new Instruction(_ => INC_R(Register.C), "INC C", 0x00DD0C, 0, 4));

        _instructions.Add(0x00DD0D, new Instruction(_ => DEC_R(Register.C), "DEC C", 0x00DD0D, 0, 4));

        _instructions.Add(0x00DD0E, new Instruction(p => LD_R_n(Register.C, p), "LD C, n", 0x00DD0E, 1, 4));

        _instructions.Add(0x00DD0F, new Instruction(_ => RRCA(), "RRCA", 0x00DD0F, 0, 4));

        _instructions.Add(0x00DD10, new Instruction(p => DJNZ_e(p), "DJNZ e", 0x00DD10, 1, 4));

        _instructions.Add(0x00DD11, new Instruction(p => LD_RR_nn(RegisterPair.DE, p), "LD DE, nn", 0x00DD11, 2, 4));

        _instructions.Add(0x00DD12, new Instruction(_ => LD_aRR_R(RegisterPair.DE, Register.A), "LD (DE), A", 0x00DD12, 0, 4));

        _instructions.Add(0x00DD13, new Instruction(_ => INC_RR(RegisterPair.DE), "INC DE", 0x00DD13, 0, 4));

        _instructions.Add(0x00DD14, new Instruction(_ => INC_R(Register.D), "INC D", 0x00DD14, 0, 4));

        _instructions.Add(0x00DD15, new Instruction(_ => DEC_R(Register.D), "DEC D", 0x00DD15, 0, 4));

        _instructions.Add(0x00DD16, new Instruction(p => LD_R_n(Register.D, p), "LD D, n", 0x00DD16, 1, 4));

        _instructions.Add(0x00DD17, new Instruction(_ => RLA(), "RLA", 0x00DD17, 0, 4));

        _instructions.Add(0x00DD18, new Instruction(p => JR_e(p), "JR e", 0x00DD18, 1, 4));

        _instructions.Add(0x00DD19, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.DE), "ADD IX, DE", 0x00DD19, 0, 4));

        _instructions.Add(0x00DD1A, new Instruction(_ => LD_R_aRR(Register.A, RegisterPair.DE), "LD A, (DE)", 0x00DD1A, 0, 4));

        _instructions.Add(0x00DD1B, new Instruction(_ => DEC_RR(RegisterPair.DE), "DEC DE", 0x00DD1B, 0, 4));

        _instructions.Add(0x00DD1C, new Instruction(_ => INC_R(Register.E), "INC E", 0x00DD1C, 0, 4));

        _instructions.Add(0x00DD1D, new Instruction(_ => DEC_R(Register.E), "DEC E", 0x00DD1D, 0, 4));

        _instructions.Add(0x00DD1E, new Instruction(p => LD_R_n(Register.E, p), "LD E, n", 0x00DD1E, 1, 4));

        _instructions.Add(0x00DD1F, new Instruction(_ => RRA(), "RRA", 0x00DD1F, 0, 4));

        _instructions.Add(0x00DD20, new Instruction(p => JR_F_e(Flag.Zero, p, true), "JR NZ, e", 0x00DD20, 1, 4));

        _instructions.Add(0x00DD21, new Instruction(p => LD_RR_nn(RegisterPair.IX, p), "LD IX, nn", 0x00DD21, 2, 4));

        _instructions.Add(0x00DD22, new Instruction(p => LD_ann_RR(p, RegisterPair.IX), "LD (nn), IX", 0x00DD22, 2, 4));

        _instructions.Add(0x00DD23, new Instruction(_ => INC_RR(RegisterPair.IX), "INC IX", 0x00DD23, 0, 4));

        _instructions.Add(0x00DD24, new Instruction(_ => INC_R(Register.IXh), "INC IXh", 0x00DD24, 0, 4));

        _instructions.Add(0x00DD25, new Instruction(_ => DEC_R(Register.IXh), "DEC IXh", 0x00DD25, 0, 4));

        _instructions.Add(0x00DD26, new Instruction(p => LD_R_n(Register.IXh, p), "LD IXh, n", 0x00DD26, 1, 4));

        _instructions.Add(0x00DD27, new Instruction(_ => DAA(), "DAA", 0x00DD27, 0, 4));

        _instructions.Add(0x00DD28, new Instruction(p => JR_F_e(Flag.Zero, p), "JR Z, e", 0x00DD28, 1, 4));

        _instructions.Add(0x00DD29, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.IX), "ADD IX, IX", 0x00DD29, 0, 4));

        _instructions.Add(0x00DD2A, new Instruction(p => LD_RR_ann(RegisterPair.IX, p), "LD IX, (nn)", 0x00DD2A, 2, 4));

        _instructions.Add(0x00DD2B, new Instruction(_ => DEC_RR(RegisterPair.IX), "DEC IX", 0x00DD2B, 0, 4));

        _instructions.Add(0x00DD2C, new Instruction(_ => INC_R(Register.IXl), "INC IXl", 0x00DD2C, 0, 4));

        _instructions.Add(0x00DD2D, new Instruction(_ => DEC_R(Register.IXl), "DEC IXl", 0x00DD2D, 0, 4));

        _instructions.Add(0x00DD2E, new Instruction(p => LD_R_n(Register.IXl, p), "LD IXl, n", 0x00DD2E, 1, 4));

        _instructions.Add(0x00DD2F, new Instruction(_ => CPL(), "CPL", 0x00DD2F, 0, 4));

        _instructions.Add(0x00DD30, new Instruction(p => JR_F_e(Flag.Carry, p, true), "JR NC, e", 0x00DD30, 1, 4));

        _instructions.Add(0x00DD31, new Instruction(p => LD_RR_nn(RegisterPair.SP, p), "LD SP, nn", 0x00DD31, 2, 4));

        _instructions.Add(0x00DD32, new Instruction(p => LD_ann_R(p, Register.A), "LD (nn), A", 0x00DD32, 2, 4));

        _instructions.Add(0x00DD33, new Instruction(_ => INC_RR(RegisterPair.SP), "INC SP", 0x00DD33, 0, 4));

        _instructions.Add(0x00DD34, new Instruction(p => INC_aRRd(RegisterPair.IX, p), "INC (IX + d)", 0x00DD34, 1));

        _instructions.Add(0x00DD35, new Instruction(p => DEC_aRRd(RegisterPair.IX, p), "DEC (IX + d)", 0x00DD35, 1));

        _instructions.Add(0x00DD36, new Instruction(p => LD_aRRd_n(RegisterPair.IX, p), "LD (IX + d), n", 0x00DD36, 2));

        _instructions.Add(0x00DD37, new Instruction(_ => SCF(), "SCF", 0x00DD37, 0, 4));

        _instructions.Add(0x00DD38, new Instruction(p => JR_F_e(Flag.Carry, p), "JR C, e", 0x00DD38, 1, 4));

        _instructions.Add(0x00DD39, new Instruction(_ => ADD_RR_RR(RegisterPair.IX, RegisterPair.SP), "ADD IX, SP", 0x00DD39, 0, 4));

        _instructions.Add(0x00DD3A, new Instruction(p => LD_R_ann(Register.A, p), "LD A, (nn)", 0x00DD3A, 2, 4));

        _instructions.Add(0x00DD3B, new Instruction(_ => DEC_RR(RegisterPair.SP), "DEC SP", 0x00DD3B, 0, 4));

        _instructions.Add(0x00DD3C, new Instruction(_ => INC_R(Register.A), "INC A", 0x00DD3C, 0, 4));

        _instructions.Add(0x00DD3D, new Instruction(_ => DEC_R(Register.A), "DEC A", 0x00DD3D, 0, 4));

        _instructions.Add(0x00DD3E, new Instruction(p => LD_R_n(Register.A, p), "LD A, n", 0x00DD3E, 1, 4));

        _instructions.Add(0x00DD3F, new Instruction(_ => CCF(), "CCF", 0x00DD3F, 0, 4));

        _instructions.Add(0x00DD40, new Instruction(_ => LD_R_R(Register.B, Register.B), "LD B, B", 0x00DD40, 0, 4));

        _instructions.Add(0x00DD41, new Instruction(_ => LD_R_R(Register.B, Register.C), "LD B, C", 0x00DD41, 0, 4));

        _instructions.Add(0x00DD42, new Instruction(_ => LD_R_R(Register.B, Register.D), "LD B, D", 0x00DD42, 0, 4));

        _instructions.Add(0x00DD43, new Instruction(_ => LD_R_R(Register.B, Register.E), "LD B, E", 0x00DD43, 0, 4));

        _instructions.Add(0x00DD44, new Instruction(_ => LD_R_R(Register.B, Register.IXh), "LD B, IXh", 0x00DD44, 0, 4));

        _instructions.Add(0x00DD45, new Instruction(_ => LD_R_R(Register.B, Register.IXl), "LD B, IXl", 0x00DD45, 0, 4));

        _instructions.Add(0x00DD46, new Instruction(p => LD_R_aRRd(Register.B, RegisterPair.IX, p), "LD B, (IX + d)", 0x00DD46, 1));

        _instructions.Add(0x00DD47, new Instruction(_ => LD_R_R(Register.B, Register.A), "LD B, A", 0x00DD47, 0, 4));

        _instructions.Add(0x00DD48, new Instruction(_ => LD_R_R(Register.C, Register.B), "LD C, B", 0x00DD48, 0, 4));

        _instructions.Add(0x00DD49, new Instruction(_ => LD_R_R(Register.C, Register.C), "LD C, C", 0x00DD49, 0, 4));

        _instructions.Add(0x00DD4A, new Instruction(_ => LD_R_R(Register.C, Register.D), "LD C, D", 0x00DD4A, 0, 4));

        _instructions.Add(0x00DD4B, new Instruction(_ => LD_R_R(Register.C, Register.E), "LD C, E", 0x00DD4B, 0, 4));

        _instructions.Add(0x00DD4C, new Instruction(_ => LD_R_R(Register.C, Register.IXh), "LD C, IXh", 0x00DD4C, 0, 4));

        _instructions.Add(0x00DD4D, new Instruction(_ => LD_R_R(Register.C, Register.IXl), "LD C, IXl", 0x00DD4D, 0, 4));

        _instructions.Add(0x00DD4E, new Instruction(p => LD_R_aRRd(Register.C, RegisterPair.IX, p), "LD C, (IX + d)", 0x00DD4E, 1));

        _instructions.Add(0x00DD4F, new Instruction(_ => LD_R_R(Register.C, Register.A), "LD C, A", 0x00DD4F, 0, 4));

        _instructions.Add(0x00DD50, new Instruction(_ => LD_R_R(Register.D, Register.B), "LD D, B", 0x00DD50, 0, 4));

        _instructions.Add(0x00DD51, new Instruction(_ => LD_R_R(Register.D, Register.C), "LD D, C", 0x00DD51, 0, 4));

        _instructions.Add(0x00DD52, new Instruction(_ => LD_R_R(Register.D, Register.D), "LD D, D", 0x00DD52, 0, 4));

        _instructions.Add(0x00DD53, new Instruction(_ => LD_R_R(Register.D, Register.E), "LD D, E", 0x00DD53, 0, 4));

        _instructions.Add(0x00DD54, new Instruction(_ => LD_R_R(Register.D, Register.IXh), "LD D, IXh", 0x00DD54, 0, 4));

        _instructions.Add(0x00DD55, new Instruction(_ => LD_R_R(Register.D, Register.IXl), "LD D, IXl", 0x00DD55, 0, 4));

        _instructions.Add(0x00DD56, new Instruction(p => LD_R_aRRd(Register.D, RegisterPair.IX, p), "LD D, (IX + d)", 0x00DD56, 1));

        _instructions.Add(0x00DD57, new Instruction(_ => LD_R_R(Register.D, Register.A), "LD D, A", 0x00DD57, 0, 4));

        _instructions.Add(0x00DD58, new Instruction(_ => LD_R_R(Register.E, Register.B), "LD E, B", 0x00DD58, 0, 4));

        _instructions.Add(0x00DD59, new Instruction(_ => LD_R_R(Register.E, Register.C), "LD E, C", 0x00DD59, 0, 4));

        _instructions.Add(0x00DD5A, new Instruction(_ => LD_R_R(Register.E, Register.D), "LD E, D", 0x00DD5A, 0, 4));

        _instructions.Add(0x00DD5B, new Instruction(_ => LD_R_R(Register.E, Register.E), "LD E, E", 0x00DD5B, 0, 4));

        _instructions.Add(0x00DD5C, new Instruction(_ => LD_R_R(Register.E, Register.IXh), "LD E, IXh", 0x00DD5C, 0, 4));

        _instructions.Add(0x00DD5D, new Instruction(_ => LD_R_R(Register.E, Register.IXl), "LD E, IXl", 0x00DD5D, 0, 4));

        _instructions.Add(0x00DD5E, new Instruction(p => LD_R_aRRd(Register.E, RegisterPair.IX, p), "LD E, (IX + d)", 0x00DD5E, 1));

        _instructions.Add(0x00DD5F, new Instruction(_ => LD_R_R(Register.E, Register.A), "LD E, A", 0x00DD5F, 0, 4));

        _instructions.Add(0x00DD60, new Instruction(_ => LD_R_R(Register.IXh, Register.B), "LD IXh, B", 0x00DD60, 0, 4));

        _instructions.Add(0x00DD61, new Instruction(_ => LD_R_R(Register.IXh, Register.C), "LD IXh, C", 0x00DD61, 0, 4));

        _instructions.Add(0x00DD62, new Instruction(_ => LD_R_R(Register.IXh, Register.D), "LD IXh, D", 0x00DD62, 0, 4));

        _instructions.Add(0x00DD63, new Instruction(_ => LD_R_R(Register.IXh, Register.E), "LD IXh, E", 0x00DD63, 0, 4));

        _instructions.Add(0x00DD64, new Instruction(_ => LD_R_R(Register.IXh, Register.IXh), "LD IXh, IXh", 0x00DD64, 0, 4));

        _instructions.Add(0x00DD65, new Instruction(_ => LD_R_R(Register.IXh, Register.IXl), "LD IXh, IXl", 0x00DD65, 0, 4));

        _instructions.Add(0x00DD66, new Instruction(p => LD_R_aRRd(Register.H, RegisterPair.IX, p), "LD H, (IX + d)", 0x00DD66, 1));

        _instructions.Add(0x00DD67, new Instruction(_ => LD_R_R(Register.IXh, Register.A), "LD IXh, A", 0x00DD67, 0, 4));

        _instructions.Add(0x00DD68, new Instruction(_ => LD_R_R(Register.IXl, Register.B), "LD IXl, B", 0x00DD68, 0, 4));

        _instructions.Add(0x00DD69, new Instruction(_ => LD_R_R(Register.IXl, Register.C), "LD IXl, C", 0x00DD69, 0, 4));

        _instructions.Add(0x00DD6A, new Instruction(_ => LD_R_R(Register.IXl, Register.D), "LD IXl, D", 0x00DD6A, 0, 4));

        _instructions.Add(0x00DD6B, new Instruction(_ => LD_R_R(Register.IXl, Register.E), "LD IXl, E", 0x00DD6B, 0, 4));

        _instructions.Add(0x00DD6C, new Instruction(_ => LD_R_R(Register.IXl, Register.IXh), "LD IXl, IXh", 0x00DD6C, 0, 4));

        _instructions.Add(0x00DD6D, new Instruction(_ => LD_R_R(Register.IXl, Register.IXl), "LD IXl, IXl", 0x00DD6D, 0, 4));

        _instructions.Add(0x00DD6E, new Instruction(p => LD_R_aRRd(Register.L, RegisterPair.IX, p), "LD L, (IX + d)", 0x00DD6E, 1));

        _instructions.Add(0x00DD6F, new Instruction(_ => LD_R_R(Register.IXl, Register.A), "LD IXl, A", 0x00DD6F, 0, 4));

        _instructions.Add(0x00DD70, new Instruction(p => LD_aRRd_R(RegisterPair.IX, p, Register.B), "LD (IX + d), B", 0x00DD70, 1));

        _instructions.Add(0x00DD71, new Instruction(p => LD_aRRd_R(RegisterPair.IX, p, Register.C), "LD (IX + d), C", 0x00DD71, 1));

        _instructions.Add(0x00DD72, new Instruction(p => LD_aRRd_R(RegisterPair.IX, p, Register.D), "LD (IX + d), D", 0x00DD72, 1));

        _instructions.Add(0x00DD73, new Instruction(p => LD_aRRd_R(RegisterPair.IX, p, Register.E), "LD (IX + d), E", 0x00DD73, 1));

        _instructions.Add(0x00DD74, new Instruction(p => LD_aRRd_R(RegisterPair.IX, p, Register.H), "LD (IX + d), H", 0x00DD74, 1));

        _instructions.Add(0x00DD75, new Instruction(p => LD_aRRd_R(RegisterPair.IX, p, Register.L), "LD (IX + d), L", 0x00DD75, 1));

        _instructions.Add(0x00DD76, new Instruction(_ => HALT(), "HALT", 0x00DD76, 0, 4));

        _instructions.Add(0x00DD77, new Instruction(p => LD_aRRd_R(RegisterPair.IX, p, Register.A), "LD (IX + d), A", 0x00DD77, 1));

        _instructions.Add(0x00DD78, new Instruction(_ => LD_R_R(Register.A, Register.B), "LD A, B", 0x00DD78, 0, 4));

        _instructions.Add(0x00DD79, new Instruction(_ => LD_R_R(Register.A, Register.C), "LD A, C", 0x00DD79, 0, 4));

        _instructions.Add(0x00DD7A, new Instruction(_ => LD_R_R(Register.A, Register.D), "LD A, D", 0x00DD7A, 0, 4));

        _instructions.Add(0x00DD7B, new Instruction(_ => LD_R_R(Register.A, Register.E), "LD A, E", 0x00DD7B, 0, 4));

        _instructions.Add(0x00DD7C, new Instruction(_ => LD_R_R(Register.A, Register.IXh), "LD A, IXh", 0x00DD7C, 0, 4));

        _instructions.Add(0x00DD7D, new Instruction(_ => LD_R_R(Register.A, Register.IXl), "LD A, IXl", 0x00DD7D, 0, 4));

        _instructions.Add(0x00DD7E, new Instruction(p => LD_R_aRRd(Register.A, RegisterPair.IX, p), "LD A, (IX + d)", 0x00DD7E, 1));

        _instructions.Add(0x00DD7F, new Instruction(_ => LD_R_R(Register.A, Register.A), "LD A, A", 0x00DD7F, 0, 4));

        _instructions.Add(0x00DD80, new Instruction(_ => ADD_R_R(Register.A, Register.B), "ADD A, B", 0x00DD80, 0, 4));

        _instructions.Add(0x00DD81, new Instruction(_ => ADD_R_R(Register.A, Register.C), "ADD A, C", 0x00DD81, 0, 4));

        _instructions.Add(0x00DD82, new Instruction(_ => ADD_R_R(Register.A, Register.D), "ADD A, D", 0x00DD82, 0, 4));

        _instructions.Add(0x00DD83, new Instruction(_ => ADD_R_R(Register.A, Register.E), "ADD A, E", 0x00DD83, 0, 4));

        _instructions.Add(0x00DD84, new Instruction(_ => ADD_R_R(Register.A, Register.IXh), "ADD A, IXh", 0x00DD84, 0, 4));

        _instructions.Add(0x00DD85, new Instruction(_ => ADD_R_R(Register.A, Register.IXl), "ADD A, IXl", 0x00DD85, 0, 4));

        _instructions.Add(0x00DD86, new Instruction(p => ADD_R_aRRd(Register.A, RegisterPair.IX, p), "ADD A, (IX + d)", 0x00DD86, 1));

        _instructions.Add(0x00DD87, new Instruction(_ => ADD_R_R(Register.A, Register.A), "ADD A, A", 0x00DD87, 0, 4));

        _instructions.Add(0x00DD88, new Instruction(_ => ADC_R_R(Register.A, Register.B), "ADC A, B", 0x00DD88, 0, 4));

        _instructions.Add(0x00DD89, new Instruction(_ => ADC_R_R(Register.A, Register.C), "ADC A, C", 0x00DD89, 0, 4));

        _instructions.Add(0x00DD8A, new Instruction(_ => ADC_R_R(Register.A, Register.D), "ADC A, D", 0x00DD8A, 0, 4));

        _instructions.Add(0x00DD8B, new Instruction(_ => ADC_R_R(Register.A, Register.E), "ADC A, E", 0x00DD8B, 0, 4));

        _instructions.Add(0x00DD8C, new Instruction(_ => ADC_R_R(Register.A, Register.IXh), "ADC A, IXh", 0x00DD8C, 0, 4));

        _instructions.Add(0x00DD8D, new Instruction(_ => ADC_R_R(Register.A, Register.IXl), "ADC A, IXl", 0x00DD8D, 0, 4));

        _instructions.Add(0x00DD8E, new Instruction(p => ADC_R_aRRd(Register.A, RegisterPair.IX, p), "ADC A, (IX + d)", 0x00DD8E, 1));

        _instructions.Add(0x00DD8F, new Instruction(_ => ADC_R_R(Register.A, Register.A), "ADC A, A", 0x00DD8F, 0, 4));

        _instructions.Add(0x00DD90, new Instruction(_ => SUB_R_R(Register.A, Register.B), "SUB A, B", 0x00DD90, 0, 4));

        _instructions.Add(0x00DD91, new Instruction(_ => SUB_R_R(Register.A, Register.C), "SUB A, C", 0x00DD91, 0, 4));

        _instructions.Add(0x00DD92, new Instruction(_ => SUB_R_R(Register.A, Register.D), "SUB A, D", 0x00DD92, 0, 4));

        _instructions.Add(0x00DD93, new Instruction(_ => SUB_R_R(Register.A, Register.E), "SUB A, E", 0x00DD93, 0, 4));

        _instructions.Add(0x00DD94, new Instruction(_ => SUB_R_R(Register.A, Register.IXh), "SUB A, IXh", 0x00DD94, 0, 4));

        _instructions.Add(0x00DD95, new Instruction(_ => SUB_R_R(Register.A, Register.IXl), "SUB A, IXl", 0x00DD95, 0, 4));

        _instructions.Add(0x00DD96, new Instruction(p => SUB_R_aRRd(Register.A, RegisterPair.IX, p), "SUB A, (IX + d)", 0x00DD96, 1));

        _instructions.Add(0x00DD97, new Instruction(_ => SUB_R_R(Register.A, Register.A), "SUB A, A", 0x00DD97, 0, 4));

        _instructions.Add(0x00DD98, new Instruction(_ => SBC_R_R(Register.A, Register.B), "SBC A, B", 0x00DD98, 0, 4));

        _instructions.Add(0x00DD99, new Instruction(_ => SBC_R_R(Register.A, Register.C), "SBC A, C", 0x00DD99, 0, 4));

        _instructions.Add(0x00DD9A, new Instruction(_ => SBC_R_R(Register.A, Register.D), "SBC A, D", 0x00DD9A, 0, 4));

        _instructions.Add(0x00DD9B, new Instruction(_ => SBC_R_R(Register.A, Register.E), "SBC A, E", 0x00DD9B, 0, 4));

        _instructions.Add(0x00DD9C, new Instruction(_ => SBC_R_R(Register.A, Register.IXh), "SBC A, IXh", 0x00DD9C, 0, 4));

        _instructions.Add(0x00DD9D, new Instruction(_ => SBC_R_R(Register.A, Register.IXl), "SBC A, IXl", 0x00DD9D, 0, 4));

        _instructions.Add(0x00DD9E, new Instruction(p => SBC_R_aRRd(Register.A, RegisterPair.IX, p), "SBC A, (IX + d)", 0x00DD9E, 1));

        _instructions.Add(0x00DD9F, new Instruction(_ => SBC_R_R(Register.A, Register.A), "SBC A, A", 0x00DD9F, 0, 4));

        _instructions.Add(0x00DDA0, new Instruction(_ => AND_R_R(Register.A, Register.B), "AND A, B", 0x00DDA0, 0, 4));

        _instructions.Add(0x00DDA1, new Instruction(_ => AND_R_R(Register.A, Register.C), "AND A, C", 0x00DDA1, 0, 4));

        _instructions.Add(0x00DDA2, new Instruction(_ => AND_R_R(Register.A, Register.D), "AND A, D", 0x00DDA2, 0, 4));

        _instructions.Add(0x00DDA3, new Instruction(_ => AND_R_R(Register.A, Register.E), "AND A, E", 0x00DDA3, 0, 4));

        _instructions.Add(0x00DDA4, new Instruction(_ => AND_R_R(Register.A, Register.IXh), "AND A, IXh", 0x00DDA4, 0, 4));

        _instructions.Add(0x00DDA5, new Instruction(_ => AND_R_R(Register.A, Register.IXl), "AND A, IXl", 0x00DDA5, 0, 4));

        _instructions.Add(0x00DDA6, new Instruction(p => AND_R_aRRd(Register.A, RegisterPair.IX, p), "AND A, (IX + d)", 0x00DDA6, 1));

        _instructions.Add(0x00DDA7, new Instruction(_ => AND_R_R(Register.A, Register.A), "AND A, A", 0x00DDA7, 0, 4));

        _instructions.Add(0x00DDA8, new Instruction(_ => XOR_R_R(Register.A, Register.B), "XOR A, B", 0x00DDA8, 0, 4));

        _instructions.Add(0x00DDA9, new Instruction(_ => XOR_R_R(Register.A, Register.C), "XOR A, C", 0x00DDA9, 0, 4));

        _instructions.Add(0x00DDAA, new Instruction(_ => XOR_R_R(Register.A, Register.D), "XOR A, D", 0x00DDAA, 0, 4));

        _instructions.Add(0x00DDAB, new Instruction(_ => XOR_R_R(Register.A, Register.E), "XOR A, E", 0x00DDAB, 0, 4));

        _instructions.Add(0x00DDAC, new Instruction(_ => XOR_R_R(Register.A, Register.IXh), "XOR A, IXh", 0x00DDAC, 0, 4));

        _instructions.Add(0x00DDAD, new Instruction(_ => XOR_R_R(Register.A, Register.IXl), "XOR A, IXl", 0x00DDAD, 0, 4));

        _instructions.Add(0x00DDAE, new Instruction(p => XOR_R_aRRd(Register.A, RegisterPair.IX, p), "XOR A, (IX + d)", 0x00DDAE, 1));

        _instructions.Add(0x00DDAF, new Instruction(_ => XOR_R_R(Register.A, Register.A), "XOR A, A", 0x00DDAF, 0, 4));

        _instructions.Add(0x00DDB0, new Instruction(_ => OR_R_R(Register.A, Register.B), "OR A, B", 0x00DDB0, 0, 4));

        _instructions.Add(0x00DDB1, new Instruction(_ => OR_R_R(Register.A, Register.C), "OR A, C", 0x00DDB1, 0, 4));

        _instructions.Add(0x00DDB2, new Instruction(_ => OR_R_R(Register.A, Register.D), "OR A, D", 0x00DDB2, 0, 4));

        _instructions.Add(0x00DDB3, new Instruction(_ => OR_R_R(Register.A, Register.E), "OR A, E", 0x00DDB3, 0, 4));

        _instructions.Add(0x00DDB4, new Instruction(_ => OR_R_R(Register.A, Register.IXh), "OR A, IXh", 0x00DDB4, 0, 4));

        _instructions.Add(0x00DDB5, new Instruction(_ => OR_R_R(Register.A, Register.IXl), "OR A, IXl", 0x00DDB5, 0, 4));

        _instructions.Add(0x00DDB6, new Instruction(p => OR_R_aRRd(Register.A, RegisterPair.IX, p), "OR A, (IX + d)", 0x00DDB6, 1));

        _instructions.Add(0x00DDB7, new Instruction(_ => OR_R_R(Register.A, Register.A), "OR A, A", 0x00DDB7, 0, 4));

        _instructions.Add(0x00DDB8, new Instruction(_ => CP_R_R(Register.A, Register.B), "CP A, B", 0x00DDB8, 0, 4));

        _instructions.Add(0x00DDB9, new Instruction(_ => CP_R_R(Register.A, Register.C), "CP A, C", 0x00DDB9, 0, 4));

        _instructions.Add(0x00DDBA, new Instruction(_ => CP_R_R(Register.A, Register.D), "CP A, D", 0x00DDBA, 0, 4));

        _instructions.Add(0x00DDBB, new Instruction(_ => CP_R_R(Register.A, Register.E), "CP A, E", 0x00DDBB, 0, 4));

        _instructions.Add(0x00DDBC, new Instruction(_ => CP_R_R(Register.A, Register.IXh), "CP A, IXh", 0x00DDBC, 0, 4));

        _instructions.Add(0x00DDBD, new Instruction(_ => CP_R_R(Register.A, Register.IXl), "CP A, IXl", 0x00DDBD, 0, 4));

        _instructions.Add(0x00DDBE, new Instruction(p => CP_R_aRRd(Register.A, RegisterPair.IX, p), "CP A, (IX + d)", 0x00DDBE, 1));

        _instructions.Add(0x00DDBF, new Instruction(_ => CP_R_R(Register.A, Register.A), "CP A, A", 0x00DDBF, 0, 4));

        _instructions.Add(0x00DDC0, new Instruction(_ => RET_F(Flag.Zero, true), "RET NZ", 0x00DDC0, 0, 4));

        _instructions.Add(0x00DDC1, new Instruction(_ => POP_RR(RegisterPair.BC), "POP BC", 0x00DDC1, 0, 4));

        _instructions.Add(0x00DDC2, new Instruction(p => JP_F_nn(Flag.Zero, p, true), "JP NZ, nn", 0x00DDC2, 2, 4));

        _instructions.Add(0x00DDC3, new Instruction(p => JP_nn(p), "JP nn", 0x00DDC3, 2, 4));

        _instructions.Add(0x00DDC4, new Instruction(p => CALL_F_nn(Flag.Zero, p, true), "CALL NZ, nn", 0x00DDC4, 2, 4));

        _instructions.Add(0x00DDC5, new Instruction(_ => PUSH_RR(RegisterPair.BC), "PUSH BC", 0x00DDC5, 0, 4));

        _instructions.Add(0x00DDC6, new Instruction(p => ADD_R_n(Register.A, p), "ADD A, n", 0x00DDC6, 1, 4));

        _instructions.Add(0x00DDC7, new Instruction(_ => RST(0x00), "RST 0x00", 0x00DDC7, 0, 4));

        _instructions.Add(0x00DDC8, new Instruction(_ => RET_F(Flag.Zero), "RET Z", 0x00DDC8, 0, 4));

        _instructions.Add(0x00DDC9, new Instruction(_ => RET(), "RET", 0x00DDC9, 0, 4));

        _instructions.Add(0x00DDCA, new Instruction(p => JP_F_nn(Flag.Zero, p), "JP Z, nn", 0x00DDCA, 2, 4));

        _instructions.Add(0x00DDCB, new Instruction(_ => PREFIX(0xDDCB), "PREFIX 0xDDCB", 0x00DDCB, 2, 0, true));

        _instructions.Add(0x00DDCC, new Instruction(p => CALL_F_nn(Flag.Zero, p), "CALL Z, nn", 0x00DDCC, 2, 4));

        _instructions.Add(0x00DDCD, new Instruction(p => CALL_nn(p), "CALL nn", 0x00DDCD, 2, 4));

        _instructions.Add(0x00DDCE, new Instruction(p => ADC_R_n(Register.A, p), "ADC A, n", 0x00DDCE, 1, 4));

        _instructions.Add(0x00DDCF, new Instruction(_ => RST(0x08), "RST 0x08", 0x00DDCF, 0, 4));

        _instructions.Add(0x00DDD0, new Instruction(_ => RET_F(Flag.Carry, true), "RET NC", 0x00DDD0, 0, 4));

        _instructions.Add(0x00DDD1, new Instruction(_ => POP_RR(RegisterPair.DE), "POP DE", 0x00DDD1, 0, 4));

        _instructions.Add(0x00DDD2, new Instruction(p => JP_F_nn(Flag.Carry, p, true), "JP NC, nn", 0x00DDD2, 2, 4));

        _instructions.Add(0x00DDD3, new Instruction(p => OUT_an_R(p, Register.A), "OUT (n), A", 0x00DDD3, 1, 4));

        _instructions.Add(0x00DDD4, new Instruction(p => CALL_F_nn(Flag.Carry, p, true), "CALL NC, nn", 0x00DDD4, 2, 4));

        _instructions.Add(0x00DDD5, new Instruction(_ => PUSH_RR(RegisterPair.DE), "PUSH DE", 0x00DDD5, 0, 4));

        _instructions.Add(0x00DDD6, new Instruction(p => SUB_R_n(Register.A, p), "SUB A, n", 0x00DDD6, 1, 4));

        _instructions.Add(0x00DDD7, new Instruction(_ => RST(0x10), "RST 0x10", 0x00DDD7, 0, 4));

        _instructions.Add(0x00DDD8, new Instruction(_ => RET_F(Flag.Carry), "RET C", 0x00DDD8, 0, 4));

        _instructions.Add(0x00DDD9, new Instruction(_ => EXX(), "EXX", 0x00DDD9, 0, 4));

        _instructions.Add(0x00DDDA, new Instruction(p => JP_F_nn(Flag.Carry, p), "JP C, nn", 0x00DDDA, 2, 4));

        _instructions.Add(0x00DDDB, new Instruction(p => IN_R_n(Register.A, p), "IN A, n", 0x00DDDB, 1, 4));

        _instructions.Add(0x00DDDC, new Instruction(p => CALL_F_nn(Flag.Carry, p), "CALL C, nn", 0x00DDDC, 2, 4));

        _instructions.Add(0x00DDDD, new Instruction(_ => PREFIX(0xDD), "PREFIX 0xDD", 0x00DDDD, 0, 4, true));

        _instructions.Add(0x00DDDE, new Instruction(p => SBC_R_n(Register.A, p), "SBC A, n", 0x00DDDE, 1, 4));

        _instructions.Add(0x00DDDF, new Instruction(_ => RST(0x18), "RST 0x18", 0x00DDDF, 0, 4));

        _instructions.Add(0x00DDE0, new Instruction(_ => RET_F(Flag.ParityOverflow, true), "RET PO", 0x00DDE0, 0, 4));

        _instructions.Add(0x00DDE1, new Instruction(_ => POP_RR(RegisterPair.IX), "POP IX", 0x00DDE1, 0, 4));

        _instructions.Add(0x00DDE2, new Instruction(p => JP_F_nn(Flag.ParityOverflow, p, true), "JP PO, nn", 0x00DDE2, 2, 4));

        _instructions.Add(0x00DDE3, new Instruction(_ => EX_aRR_RR(RegisterPair.SP, RegisterPair.IX), "EX (SP), IX", 0x00DDE3, 0, 4));

        _instructions.Add(0x00DDE4, new Instruction(p => CALL_F_nn(Flag.ParityOverflow, p, true), "CALL PO, nn", 0x00DDE4, 2, 4));

        _instructions.Add(0x00DDE5, new Instruction(_ => PUSH_RR(RegisterPair.IX), "PUSH IX", 0x00DDE5, 0, 4));

        _instructions.Add(0x00DDE6, new Instruction(p => AND_R_n(Register.A, p), "AND A, n", 0x00DDE6, 1, 4));

        _instructions.Add(0x00DDE7, new Instruction(_ => RST(0x20), "RST 0x20", 0x00DDE7, 0, 4));

        _instructions.Add(0x00DDE8, new Instruction(_ => RET_F(Flag.ParityOverflow), "RET PE", 0x00DDE8, 0, 4));

        _instructions.Add(0x00DDE9, new Instruction(_ => JP_aRR(RegisterPair.IX), "JP (IX)", 0x00DDE9, 0, 4));

        _instructions.Add(0x00DDEA, new Instruction(p => JP_F_nn(Flag.ParityOverflow, p), "JP PE, nn", 0x00DDEA, 2, 4));

        _instructions.Add(0x00DDEB, new Instruction(_ => EX_RR_RR(RegisterPair.DE, RegisterPair.HL), "EX DE, HL", 0x00DDEB, 0, 4));

        _instructions.Add(0x00DDEC, new Instruction(p => CALL_F_nn(Flag.ParityOverflow, p), "CALL PE, nn", 0x00DDEC, 2, 4));

        _instructions.Add(0x00DDED, new Instruction(_ => PREFIX(0xED), "PREFIX 0xED", 0x00DDED, 0, 4, true));

        _instructions.Add(0x00DDEE, new Instruction(p => XOR_R_n(Register.A, p), "XOR A, n", 0x00DDEE, 1, 4));

        _instructions.Add(0x00DDEF, new Instruction(_ => RST(0x28), "RST 0x28", 0x00DDEF, 0, 4));

        _instructions.Add(0x00DDF0, new Instruction(_ => RET_F(Flag.Sign, true), "RET NS", 0x00DDF0, 0, 4));

        _instructions.Add(0x00DDF1, new Instruction(_ => POP_RR(RegisterPair.AF), "POP AF", 0x00DDF1, 0, 4));

        _instructions.Add(0x00DDF2, new Instruction(p => JP_F_nn(Flag.Sign, p, true), "JP NS, nn", 0x00DDF2, 2, 4));

        _instructions.Add(0x00DDF3, new Instruction(_ => DI(), "DI", 0x00DDF3, 0, 4));

        _instructions.Add(0x00DDF4, new Instruction(p => CALL_F_nn(Flag.Sign, p, true), "CALL NS, nn", 0x00DDF4, 2, 4));

        _instructions.Add(0x00DDF5, new Instruction(_ => PUSH_RR(RegisterPair.AF), "PUSH AF", 0x00DDF5, 0, 4));

        _instructions.Add(0x00DDF6, new Instruction(p => OR_R_n(Register.A, p), "OR A, n", 0x00DDF6, 1, 4));

        _instructions.Add(0x00DDF7, new Instruction(_ => RST(0x30), "RST 0x30", 0x00DDF7, 0, 4));

        _instructions.Add(0x00DDF8, new Instruction(_ => RET_F(Flag.Sign), "RET S", 0x00DDF8, 0, 4));

        _instructions.Add(0x00DDF9, new Instruction(_ => LD_RR_RR(RegisterPair.SP, RegisterPair.IX), "LD SP, IX", 0x00DDF9, 0, 4));

        _instructions.Add(0x00DDFA, new Instruction(p => JP_F_nn(Flag.Sign, p), "JP S, nn", 0x00DDFA, 2, 4));

        _instructions.Add(0x00DDFB, new Instruction(_ => EI(), "EI", 0x00DDFB, 0, 4));

        _instructions.Add(0x00DDFC, new Instruction(p => CALL_F_nn(Flag.Sign, p), "CALL S, nn", 0x00DDFC, 2, 4));

        _instructions.Add(0x00DDFD, new Instruction(_ => PREFIX(0xFD), "PREFIX 0xFD", 0x00DDFD, 0, 4, true));

        _instructions.Add(0x00DDFE, new Instruction(p => CP_R_n(Register.A, p), "CP A, n", 0x00DDFE, 1, 4));

        _instructions.Add(0x00DDFF, new Instruction(_ => RST(0x38), "RST 0x38", 0x00DDFF, 0, 4));
    }
}