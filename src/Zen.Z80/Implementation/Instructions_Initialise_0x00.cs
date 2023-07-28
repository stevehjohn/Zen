using Zen.Z80.Processor;

// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    public void Initialise0x00()
    {
        _instructions.Add(0x000000, new Instruction(_ => NOP(), "NOP", 0x000000, 0));

        _instructions.Add(0x000001, new Instruction(p => LD_RR_nn(RegisterPair.BC, p), "LD BC, nn", 0x000001, 2));

        _instructions.Add(0x000002, new Instruction(_ => LD_aRR_R(RegisterPair.BC, Register.A), "LD (BC), A", 0x000002, 0));

        _instructions.Add(0x000003, new Instruction(_ => INC_RR(RegisterPair.BC), "INC BC", 0x000003, 0));

        _instructions.Add(0x000004, new Instruction(_ => INC_R(Register.B), "INC B", 0x000004, 0));

        _instructions.Add(0x000005, new Instruction(_ => DEC_R(Register.B), "DEC B", 0x000005, 0));

        _instructions.Add(0x000006, new Instruction(p => LD_R_n(Register.B, p), "LD B, n", 0x000006, 1));

        _instructions.Add(0x000007, new Instruction(_ => RLCA(), "RLCA", 0x000007, 0));

        _instructions.Add(0x000008, new Instruction(_ => EX_RR_RR(RegisterPair.AF, RegisterPair.AF_), "EX AF, AF'", 0x000008, 0));

        _instructions.Add(0x000009, new Instruction(_ => ADD_RR_RR(RegisterPair.HL, RegisterPair.BC), "ADD HL, BC", 0x000009, 0));

        _instructions.Add(0x00000A, new Instruction(_ => LD_R_aRR(Register.A, RegisterPair.BC), "LD A, (BC)", 0x00000A, 0));

        _instructions.Add(0x00000B, new Instruction(_ => DEC_RR(RegisterPair.BC), "DEC BC", 0x00000B, 0));

        _instructions.Add(0x00000C, new Instruction(_ => INC_R(Register.C), "INC C", 0x00000C, 0));

        _instructions.Add(0x00000D, new Instruction(_ => DEC_R(Register.C), "DEC C", 0x00000D, 0));

        _instructions.Add(0x00000E, new Instruction(p => LD_R_n(Register.C, p), "LD C, n", 0x00000E, 1));

        _instructions.Add(0x00000F, new Instruction(_ => RRCA(), "RRCA", 0x00000F, 0));

        _instructions.Add(0x000010, new Instruction(DJNZ_e, "DJNZ e", 0x000010, 1));

        _instructions.Add(0x000011, new Instruction(p => LD_RR_nn(RegisterPair.DE, p), "LD DE, nn", 0x000011, 2));

        _instructions.Add(0x000012, new Instruction(_ => LD_aRR_R(RegisterPair.DE, Register.A), "LD (DE), A", 0x000012, 0));

        _instructions.Add(0x000013, new Instruction(_ => INC_RR(RegisterPair.DE), "INC DE", 0x000013, 0));

        _instructions.Add(0x000014, new Instruction(_ => INC_R(Register.D), "INC D", 0x000014, 0));

        _instructions.Add(0x000015, new Instruction(_ => DEC_R(Register.D), "DEC D", 0x000015, 0));

        _instructions.Add(0x000016, new Instruction(p => LD_R_n(Register.D, p), "LD D, n", 0x000016, 1));

        _instructions.Add(0x000017, new Instruction(_ => RLA(), "RLA", 0x000017, 0));

        _instructions.Add(0x000018, new Instruction(JR_e, "JR e", 0x000018, 1));

        _instructions.Add(0x000019, new Instruction(_ => ADD_RR_RR(RegisterPair.HL, RegisterPair.DE), "ADD HL, DE", 0x000019, 0));

        _instructions.Add(0x00001A, new Instruction(_ => LD_R_aRR(Register.A, RegisterPair.DE), "LD A, (DE)", 0x00001A, 0));

        _instructions.Add(0x00001B, new Instruction(_ => DEC_RR(RegisterPair.DE), "DEC DE", 0x00001B, 0));

        _instructions.Add(0x00001C, new Instruction(_ => INC_R(Register.E), "INC E", 0x00001C, 0));

        _instructions.Add(0x00001D, new Instruction(_ => DEC_R(Register.E), "DEC E", 0x00001D, 0));

        _instructions.Add(0x00001E, new Instruction(p => LD_R_n(Register.E, p), "LD E, n", 0x00001E, 1));

        _instructions.Add(0x00001F, new Instruction(_ => RRA(), "RRA", 0x00001F, 0));

        _instructions.Add(0x000020, new Instruction(p => JR_F_e(Flag.Zero, p, true), "JR NZ, e", 0x000020, 1));

        _instructions.Add(0x000021, new Instruction(p => LD_RR_nn(RegisterPair.HL, p), "LD HL, nn", 0x000021, 2));

        _instructions.Add(0x000022, new Instruction(p => LD_ann_RR(p, RegisterPair.HL), "LD (nn), HL", 0x000022, 2));

        _instructions.Add(0x000023, new Instruction(_ => INC_RR(RegisterPair.HL), "INC HL", 0x000023, 0));

        _instructions.Add(0x000024, new Instruction(_ => INC_R(Register.H), "INC H", 0x000024, 0));

        _instructions.Add(0x000025, new Instruction(_ => DEC_R(Register.H), "DEC H", 0x000025, 0));

        _instructions.Add(0x000026, new Instruction(p => LD_R_n(Register.H, p), "LD H, n", 0x000026, 1));

        _instructions.Add(0x000027, new Instruction(_ => DAA(), "DAA", 0x000027, 0));

        _instructions.Add(0x000028, new Instruction(p => JR_F_e(Flag.Zero, p), "JR Z, e", 0x000028, 1));

        _instructions.Add(0x000029, new Instruction(_ => ADD_RR_RR(RegisterPair.HL, RegisterPair.HL), "ADD HL, HL", 0x000029, 0));

        _instructions.Add(0x00002A, new Instruction(p => LD_RR_ann(RegisterPair.HL, p), "LD HL, (nn)", 0x00002A, 2));

        _instructions.Add(0x00002B, new Instruction(_ => DEC_RR(RegisterPair.HL), "DEC HL", 0x00002B, 0));

        _instructions.Add(0x00002C, new Instruction(_ => INC_R(Register.L), "INC L", 0x00002C, 0));

        _instructions.Add(0x00002D, new Instruction(_ => DEC_R(Register.L), "DEC L", 0x00002D, 0));

        _instructions.Add(0x00002E, new Instruction(p => LD_R_n(Register.L, p), "LD L, n", 0x00002E, 1));

        _instructions.Add(0x00002F, new Instruction(_ => CPL(), "CPL", 0x00002F, 0));

        _instructions.Add(0x000030, new Instruction(p => JR_F_e(Flag.Carry, p, true), "JR NC, e", 0x000030, 1));

        _instructions.Add(0x000031, new Instruction(p => LD_RR_nn(RegisterPair.SP, p), "LD SP, nn", 0x000031, 2));

        _instructions.Add(0x000032, new Instruction(p => LD_ann_R(p, Register.A), "LD (nn), A", 0x000032, 2));

        _instructions.Add(0x000033, new Instruction(_ => INC_RR(RegisterPair.SP), "INC SP", 0x000033, 0));

        _instructions.Add(0x000034, new Instruction(_ => INC_aRR(RegisterPair.HL), "INC (HL)", 0x000034, 0));

        _instructions.Add(0x000035, new Instruction(_ => DEC_aRR(RegisterPair.HL), "DEC (HL)", 0x000035, 0));

        _instructions.Add(0x000036, new Instruction(p => LD_aRR_n(RegisterPair.HL, p), "LD (HL), n", 0x000036, 1));

        _instructions.Add(0x000037, new Instruction(_ => SCF(), "SCF", 0x000037, 0));

        _instructions.Add(0x000038, new Instruction(p => JR_F_e(Flag.Carry, p), "JR C, e", 0x000038, 1));

        _instructions.Add(0x000039, new Instruction(_ => ADD_RR_RR(RegisterPair.HL, RegisterPair.SP), "ADD HL, SP", 0x000039, 0));

        _instructions.Add(0x00003A, new Instruction(p => LD_R_ann(Register.A, p), "LD A, (nn)", 0x00003A, 2));

        _instructions.Add(0x00003B, new Instruction(_ => DEC_RR(RegisterPair.SP), "DEC SP", 0x00003B, 0));

        _instructions.Add(0x00003C, new Instruction(_ => INC_R(Register.A), "INC A", 0x00003C, 0));

        _instructions.Add(0x00003D, new Instruction(_ => DEC_R(Register.A), "DEC A", 0x00003D, 0));

        _instructions.Add(0x00003E, new Instruction(p => LD_R_n(Register.A, p), "LD A, n", 0x00003E, 1));

        _instructions.Add(0x00003F, new Instruction(_ => CCF(), "CCF", 0x00003F, 0));

        _instructions.Add(0x000040, new Instruction(_ => LD_R_R(Register.B, Register.B), "LD B, B", 0x000040, 0));

        _instructions.Add(0x000041, new Instruction(_ => LD_R_R(Register.B, Register.C), "LD B, C", 0x000041, 0));

        _instructions.Add(0x000042, new Instruction(_ => LD_R_R(Register.B, Register.D), "LD B, D", 0x000042, 0));

        _instructions.Add(0x000043, new Instruction(_ => LD_R_R(Register.B, Register.E), "LD B, E", 0x000043, 0));

        _instructions.Add(0x000044, new Instruction(_ => LD_R_R(Register.B, Register.H), "LD B, H", 0x000044, 0));

        _instructions.Add(0x000045, new Instruction(_ => LD_R_R(Register.B, Register.L), "LD B, L", 0x000045, 0));

        _instructions.Add(0x000046, new Instruction(_ => LD_R_aRR(Register.B, RegisterPair.HL), "LD B, (HL)", 0x000046, 0));

        _instructions.Add(0x000047, new Instruction(_ => LD_R_R(Register.B, Register.A), "LD B, A", 0x000047, 0));

        _instructions.Add(0x000048, new Instruction(_ => LD_R_R(Register.C, Register.B), "LD C, B", 0x000048, 0));

        _instructions.Add(0x000049, new Instruction(_ => LD_R_R(Register.C, Register.C), "LD C, C", 0x000049, 0));

        _instructions.Add(0x00004A, new Instruction(_ => LD_R_R(Register.C, Register.D), "LD C, D", 0x00004A, 0));

        _instructions.Add(0x00004B, new Instruction(_ => LD_R_R(Register.C, Register.E), "LD C, E", 0x00004B, 0));

        _instructions.Add(0x00004C, new Instruction(_ => LD_R_R(Register.C, Register.H), "LD C, H", 0x00004C, 0));

        _instructions.Add(0x00004D, new Instruction(_ => LD_R_R(Register.C, Register.L), "LD C, L", 0x00004D, 0));

        _instructions.Add(0x00004E, new Instruction(_ => LD_R_aRR(Register.C, RegisterPair.HL), "LD C, (HL)", 0x00004E, 0));

        _instructions.Add(0x00004F, new Instruction(_ => LD_R_R(Register.C, Register.A), "LD C, A", 0x00004F, 0));

        _instructions.Add(0x000050, new Instruction(_ => LD_R_R(Register.D, Register.B), "LD D, B", 0x000050, 0));

        _instructions.Add(0x000051, new Instruction(_ => LD_R_R(Register.D, Register.C), "LD D, C", 0x000051, 0));

        _instructions.Add(0x000052, new Instruction(_ => LD_R_R(Register.D, Register.D), "LD D, D", 0x000052, 0));

        _instructions.Add(0x000053, new Instruction(_ => LD_R_R(Register.D, Register.E), "LD D, E", 0x000053, 0));

        _instructions.Add(0x000054, new Instruction(_ => LD_R_R(Register.D, Register.H), "LD D, H", 0x000054, 0));

        _instructions.Add(0x000055, new Instruction(_ => LD_R_R(Register.D, Register.L), "LD D, L", 0x000055, 0));

        _instructions.Add(0x000056, new Instruction(_ => LD_R_aRR(Register.D, RegisterPair.HL), "LD D, (HL)", 0x000056, 0));

        _instructions.Add(0x000057, new Instruction(_ => LD_R_R(Register.D, Register.A), "LD D, A", 0x000057, 0));

        _instructions.Add(0x000058, new Instruction(_ => LD_R_R(Register.E, Register.B), "LD E, B", 0x000058, 0));

        _instructions.Add(0x000059, new Instruction(_ => LD_R_R(Register.E, Register.C), "LD E, C", 0x000059, 0));

        _instructions.Add(0x00005A, new Instruction(_ => LD_R_R(Register.E, Register.D), "LD E, D", 0x00005A, 0));

        _instructions.Add(0x00005B, new Instruction(_ => LD_R_R(Register.E, Register.E), "LD E, E", 0x00005B, 0));

        _instructions.Add(0x00005C, new Instruction(_ => LD_R_R(Register.E, Register.H), "LD E, H", 0x00005C, 0));

        _instructions.Add(0x00005D, new Instruction(_ => LD_R_R(Register.E, Register.L), "LD E, L", 0x00005D, 0));

        _instructions.Add(0x00005E, new Instruction(_ => LD_R_aRR(Register.E, RegisterPair.HL), "LD E, (HL)", 0x00005E, 0));

        _instructions.Add(0x00005F, new Instruction(_ => LD_R_R(Register.E, Register.A), "LD E, A", 0x00005F, 0));

        _instructions.Add(0x000060, new Instruction(_ => LD_R_R(Register.H, Register.B), "LD H, B", 0x000060, 0));

        _instructions.Add(0x000061, new Instruction(_ => LD_R_R(Register.H, Register.C), "LD H, C", 0x000061, 0));

        _instructions.Add(0x000062, new Instruction(_ => LD_R_R(Register.H, Register.D), "LD H, D", 0x000062, 0));

        _instructions.Add(0x000063, new Instruction(_ => LD_R_R(Register.H, Register.E), "LD H, E", 0x000063, 0));

        _instructions.Add(0x000064, new Instruction(_ => LD_R_R(Register.H, Register.H), "LD H, H", 0x000064, 0));

        _instructions.Add(0x000065, new Instruction(_ => LD_R_R(Register.H, Register.L), "LD H, L", 0x000065, 0));

        _instructions.Add(0x000066, new Instruction(_ => LD_R_aRR(Register.H, RegisterPair.HL), "LD H, (HL)", 0x000066, 0));

        _instructions.Add(0x000067, new Instruction(_ => LD_R_R(Register.H, Register.A), "LD H, A", 0x000067, 0));

        _instructions.Add(0x000068, new Instruction(_ => LD_R_R(Register.L, Register.B), "LD L, B", 0x000068, 0));

        _instructions.Add(0x000069, new Instruction(_ => LD_R_R(Register.L, Register.C), "LD L, C", 0x000069, 0));

        _instructions.Add(0x00006A, new Instruction(_ => LD_R_R(Register.L, Register.D), "LD L, D", 0x00006A, 0));

        _instructions.Add(0x00006B, new Instruction(_ => LD_R_R(Register.L, Register.E), "LD L, E", 0x00006B, 0));

        _instructions.Add(0x00006C, new Instruction(_ => LD_R_R(Register.L, Register.H), "LD L, H", 0x00006C, 0));

        _instructions.Add(0x00006D, new Instruction(_ => LD_R_R(Register.L, Register.L), "LD L, L", 0x00006D, 0));

        _instructions.Add(0x00006E, new Instruction(_ => LD_R_aRR(Register.L, RegisterPair.HL), "LD L, (HL)", 0x00006E, 0));

        _instructions.Add(0x00006F, new Instruction(_ => LD_R_R(Register.L, Register.A), "LD L, A", 0x00006F, 0));

        _instructions.Add(0x000070, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.B), "LD (HL), B", 0x000070, 0));

        _instructions.Add(0x000071, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.C), "LD (HL), C", 0x000071, 0));

        _instructions.Add(0x000072, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.D), "LD (HL), D", 0x000072, 0));

        _instructions.Add(0x000073, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.E), "LD (HL), E", 0x000073, 0));

        _instructions.Add(0x000074, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.H), "LD (HL), H", 0x000074, 0));

        _instructions.Add(0x000075, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.L), "LD (HL), L", 0x000075, 0));

        _instructions.Add(0x000076, new Instruction(_ => HALT(), "HALT", 0x000076, 0));

        _instructions.Add(0x000077, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.A), "LD (HL), A", 0x000077, 0));

        _instructions.Add(0x000078, new Instruction(_ => LD_R_R(Register.A, Register.B), "LD A, B", 0x000078, 0));

        _instructions.Add(0x000079, new Instruction(_ => LD_R_R(Register.A, Register.C), "LD A, C", 0x000079, 0));

        _instructions.Add(0x00007A, new Instruction(_ => LD_R_R(Register.A, Register.D), "LD A, D", 0x00007A, 0));

        _instructions.Add(0x00007B, new Instruction(_ => LD_R_R(Register.A, Register.E), "LD A, E", 0x00007B, 0));

        _instructions.Add(0x00007C, new Instruction(_ => LD_R_R(Register.A, Register.H), "LD A, H", 0x00007C, 0));

        _instructions.Add(0x00007D, new Instruction(_ => LD_R_R(Register.A, Register.L), "LD A, L", 0x00007D, 0));

        _instructions.Add(0x00007E, new Instruction(_ => LD_R_aRR(Register.A, RegisterPair.HL), "LD A, (HL)", 0x00007E, 0));

        _instructions.Add(0x00007F, new Instruction(_ => LD_R_R(Register.A, Register.A), "LD A, A", 0x00007F, 0));

        _instructions.Add(0x000080, new Instruction(_ => ADD_R_R(Register.A, Register.B), "ADD A, B", 0x000080, 0));

        _instructions.Add(0x000081, new Instruction(_ => ADD_R_R(Register.A, Register.C), "ADD A, C", 0x000081, 0));

        _instructions.Add(0x000082, new Instruction(_ => ADD_R_R(Register.A, Register.D), "ADD A, D", 0x000082, 0));

        _instructions.Add(0x000083, new Instruction(_ => ADD_R_R(Register.A, Register.E), "ADD A, E", 0x000083, 0));

        _instructions.Add(0x000084, new Instruction(_ => ADD_R_R(Register.A, Register.H), "ADD A, H", 0x000084, 0));

        _instructions.Add(0x000085, new Instruction(_ => ADD_R_R(Register.A, Register.L), "ADD A, L", 0x000085, 0));

        _instructions.Add(0x000086, new Instruction(_ => ADD_R_aRR(Register.A, RegisterPair.HL), "ADD A, (HL)", 0x000086, 0));

        _instructions.Add(0x000087, new Instruction(_ => ADD_R_R(Register.A, Register.A), "ADD A, A", 0x000087, 0));

        _instructions.Add(0x000088, new Instruction(_ => ADC_R_R(Register.A, Register.B), "ADC A, B", 0x000088, 0));

        _instructions.Add(0x000089, new Instruction(_ => ADC_R_R(Register.A, Register.C), "ADC A, C", 0x000089, 0));

        _instructions.Add(0x00008A, new Instruction(_ => ADC_R_R(Register.A, Register.D), "ADC A, D", 0x00008A, 0));

        _instructions.Add(0x00008B, new Instruction(_ => ADC_R_R(Register.A, Register.E), "ADC A, E", 0x00008B, 0));

        _instructions.Add(0x00008C, new Instruction(_ => ADC_R_R(Register.A, Register.H), "ADC A, H", 0x00008C, 0));

        _instructions.Add(0x00008D, new Instruction(_ => ADC_R_R(Register.A, Register.L), "ADC A, L", 0x00008D, 0));

        _instructions.Add(0x00008E, new Instruction(_ => ADC_R_aRR(Register.A, RegisterPair.HL), "ADC A, (HL)", 0x00008E, 0));

        _instructions.Add(0x00008F, new Instruction(_ => ADC_R_R(Register.A, Register.A), "ADC A, A", 0x00008F, 0));

        _instructions.Add(0x000090, new Instruction(_ => SUB_R_R(Register.A, Register.B), "SUB A, B", 0x000090, 0));

        _instructions.Add(0x000091, new Instruction(_ => SUB_R_R(Register.A, Register.C), "SUB A, C", 0x000091, 0));

        _instructions.Add(0x000092, new Instruction(_ => SUB_R_R(Register.A, Register.D), "SUB A, D", 0x000092, 0));

        _instructions.Add(0x000093, new Instruction(_ => SUB_R_R(Register.A, Register.E), "SUB A, E", 0x000093, 0));

        _instructions.Add(0x000094, new Instruction(_ => SUB_R_R(Register.A, Register.H), "SUB A, H", 0x000094, 0));

        _instructions.Add(0x000095, new Instruction(_ => SUB_R_R(Register.A, Register.L), "SUB A, L", 0x000095, 0));

        _instructions.Add(0x000096, new Instruction(_ => SUB_R_aRR(Register.A, RegisterPair.HL), "SUB A, (HL)", 0x000096, 0));

        _instructions.Add(0x000097, new Instruction(_ => SUB_R_R(Register.A, Register.A), "SUB A, A", 0x000097, 0));

        _instructions.Add(0x000098, new Instruction(_ => SBC_R_R(Register.A, Register.B), "SBC A, B", 0x000098, 0));

        _instructions.Add(0x000099, new Instruction(_ => SBC_R_R(Register.A, Register.C), "SBC A, C", 0x000099, 0));

        _instructions.Add(0x00009A, new Instruction(_ => SBC_R_R(Register.A, Register.D), "SBC A, D", 0x00009A, 0));

        _instructions.Add(0x00009B, new Instruction(_ => SBC_R_R(Register.A, Register.E), "SBC A, E", 0x00009B, 0));

        _instructions.Add(0x00009C, new Instruction(_ => SBC_R_R(Register.A, Register.H), "SBC A, H", 0x00009C, 0));

        _instructions.Add(0x00009D, new Instruction(_ => SBC_R_R(Register.A, Register.L), "SBC A, L", 0x00009D, 0));

        _instructions.Add(0x00009E, new Instruction(_ => SBC_R_aRR(Register.A, RegisterPair.HL), "SBC A, (HL)", 0x00009E, 0));

        _instructions.Add(0x00009F, new Instruction(_ => SBC_R_R(Register.A, Register.A), "SBC A, A", 0x00009F, 0));

        _instructions.Add(0x0000A0, new Instruction(_ => AND_R_R(Register.A, Register.B), "AND A, B", 0x0000A0, 0));

        _instructions.Add(0x0000A1, new Instruction(_ => AND_R_R(Register.A, Register.C), "AND A, C", 0x0000A1, 0));

        _instructions.Add(0x0000A2, new Instruction(_ => AND_R_R(Register.A, Register.D), "AND A, D", 0x0000A2, 0));

        _instructions.Add(0x0000A3, new Instruction(_ => AND_R_R(Register.A, Register.E), "AND A, E", 0x0000A3, 0));

        _instructions.Add(0x0000A4, new Instruction(_ => AND_R_R(Register.A, Register.H), "AND A, H", 0x0000A4, 0));

        _instructions.Add(0x0000A5, new Instruction(_ => AND_R_R(Register.A, Register.L), "AND A, L", 0x0000A5, 0));

        _instructions.Add(0x0000A6, new Instruction(_ => AND_R_aRR(Register.A, RegisterPair.HL), "AND A, (HL)", 0x0000A6, 0));

        _instructions.Add(0x0000A7, new Instruction(_ => AND_R_R(Register.A, Register.A), "AND A, A", 0x0000A7, 0));

        _instructions.Add(0x0000A8, new Instruction(_ => XOR_R_R(Register.A, Register.B), "XOR A, B", 0x0000A8, 0));

        _instructions.Add(0x0000A9, new Instruction(_ => XOR_R_R(Register.A, Register.C), "XOR A, C", 0x0000A9, 0));

        _instructions.Add(0x0000AA, new Instruction(_ => XOR_R_R(Register.A, Register.D), "XOR A, D", 0x0000AA, 0));

        _instructions.Add(0x0000AB, new Instruction(_ => XOR_R_R(Register.A, Register.E), "XOR A, E", 0x0000AB, 0));

        _instructions.Add(0x0000AC, new Instruction(_ => XOR_R_R(Register.A, Register.H), "XOR A, H", 0x0000AC, 0));

        _instructions.Add(0x0000AD, new Instruction(_ => XOR_R_R(Register.A, Register.L), "XOR A, L", 0x0000AD, 0));

        _instructions.Add(0x0000AE, new Instruction(_ => XOR_R_aRR(Register.A, RegisterPair.HL), "XOR A, (HL)", 0x0000AE, 0));

        _instructions.Add(0x0000AF, new Instruction(_ => XOR_R_R(Register.A, Register.A), "XOR A, A", 0x0000AF, 0));

        _instructions.Add(0x0000B0, new Instruction(_ => OR_R_R(Register.A, Register.B), "OR A, B", 0x0000B0, 0));

        _instructions.Add(0x0000B1, new Instruction(_ => OR_R_R(Register.A, Register.C), "OR A, C", 0x0000B1, 0));

        _instructions.Add(0x0000B2, new Instruction(_ => OR_R_R(Register.A, Register.D), "OR A, D", 0x0000B2, 0));

        _instructions.Add(0x0000B3, new Instruction(_ => OR_R_R(Register.A, Register.E), "OR A, E", 0x0000B3, 0));

        _instructions.Add(0x0000B4, new Instruction(_ => OR_R_R(Register.A, Register.H), "OR A, H", 0x0000B4, 0));

        _instructions.Add(0x0000B5, new Instruction(_ => OR_R_R(Register.A, Register.L), "OR A, L", 0x0000B5, 0));

        _instructions.Add(0x0000B6, new Instruction(_ => OR_R_aRR(Register.A, RegisterPair.HL), "OR A, (HL)", 0x0000B6, 0));

        _instructions.Add(0x0000B7, new Instruction(_ => OR_R_R(Register.A, Register.A), "OR A, A", 0x0000B7, 0));

        _instructions.Add(0x0000B8, new Instruction(_ => CP_R_R(Register.A, Register.B), "CP A, B", 0x0000B8, 0));

        _instructions.Add(0x0000B9, new Instruction(_ => CP_R_R(Register.A, Register.C), "CP A, C", 0x0000B9, 0));

        _instructions.Add(0x0000BA, new Instruction(_ => CP_R_R(Register.A, Register.D), "CP A, D", 0x0000BA, 0));

        _instructions.Add(0x0000BB, new Instruction(_ => CP_R_R(Register.A, Register.E), "CP A, E", 0x0000BB, 0));

        _instructions.Add(0x0000BC, new Instruction(_ => CP_R_R(Register.A, Register.H), "CP A, H", 0x0000BC, 0));

        _instructions.Add(0x0000BD, new Instruction(_ => CP_R_R(Register.A, Register.L), "CP A, L", 0x0000BD, 0));

        _instructions.Add(0x0000BE, new Instruction(_ => CP_R_aRR(Register.A, RegisterPair.HL), "CP A, (HL)", 0x0000BE, 0));

        _instructions.Add(0x0000BF, new Instruction(_ => CP_R_R(Register.A, Register.A), "CP A, A", 0x0000BF, 0));

        _instructions.Add(0x0000C0, new Instruction(_ => RET_F(Flag.Zero, true), "RET NZ", 0x0000C0, 0));

        _instructions.Add(0x0000C1, new Instruction(_ => POP_RR(RegisterPair.BC), "POP BC", 0x0000C1, 0));

        _instructions.Add(0x0000C2, new Instruction(p => JP_F_nn(Flag.Zero, p, true), "JP NZ, nn", 0x0000C2, 2));

        _instructions.Add(0x0000C3, new Instruction(JP_nn, "JP nn", 0x0000C3, 2));

        _instructions.Add(0x0000C4, new Instruction(p => CALL_F_nn(Flag.Zero, p, true), "CALL NZ, nn", 0x0000C4, 2));

        _instructions.Add(0x0000C5, new Instruction(_ => PUSH_RR(RegisterPair.BC), "PUSH BC", 0x0000C5, 0));

        _instructions.Add(0x0000C6, new Instruction(p => ADD_R_n(Register.A, p), "ADD A, n", 0x0000C6, 1));

        _instructions.Add(0x0000C7, new Instruction(_ => RST(0x00), "RST 0x00", 0x0000C7, 0));

        _instructions.Add(0x0000C8, new Instruction(_ => RET_F(Flag.Zero), "RET Z", 0x0000C8, 0));

        _instructions.Add(0x0000C9, new Instruction(_ => RET(), "RET", 0x0000C9, 0));

        _instructions.Add(0x0000CA, new Instruction(p => JP_F_nn(Flag.Zero, p), "JP Z, nn", 0x0000CA, 2));

        _instructions.Add(0x0000CB, new Instruction(_ => PREFIX(0xCB), "PREFIX 0xCB", 0x0000CB, 0, 0, true));

        _instructions.Add(0x0000CC, new Instruction(p => CALL_F_nn(Flag.Zero, p), "CALL Z, nn", 0x0000CC, 2));

        _instructions.Add(0x0000CD, new Instruction(CALL_nn, "CALL nn", 0x0000CD, 2));

        _instructions.Add(0x0000CE, new Instruction(p => ADC_R_n(Register.A, p), "ADC A, n", 0x0000CE, 1));

        _instructions.Add(0x0000CF, new Instruction(_ => RST(0x08), "RST 0x08", 0x0000CF, 0));

        _instructions.Add(0x0000D0, new Instruction(_ => RET_F(Flag.Carry, true), "RET NC", 0x0000D0, 0));

        _instructions.Add(0x0000D1, new Instruction(_ => POP_RR(RegisterPair.DE), "POP DE", 0x0000D1, 0));

        _instructions.Add(0x0000D2, new Instruction(p => JP_F_nn(Flag.Carry, p, true), "JP NC, nn", 0x0000D2, 2));

        _instructions.Add(0x0000D3, new Instruction(p => OUT_an_R(p, Register.A), "OUT (n), A", 0x0000D3, 1));

        _instructions.Add(0x0000D4, new Instruction(p => CALL_F_nn(Flag.Carry, p, true), "CALL NC, nn", 0x0000D4, 2));

        _instructions.Add(0x0000D5, new Instruction(_ => PUSH_RR(RegisterPair.DE), "PUSH DE", 0x0000D5, 0));

        _instructions.Add(0x0000D6, new Instruction(p => SUB_R_n(Register.A, p), "SUB A, n", 0x0000D6, 1));

        _instructions.Add(0x0000D7, new Instruction(_ => RST(0x10), "RST 0x10", 0x0000D7, 0));

        _instructions.Add(0x0000D8, new Instruction(_ => RET_F(Flag.Carry), "RET C", 0x0000D8, 0));

        _instructions.Add(0x0000D9, new Instruction(_ => EXX(), "EXX", 0x0000D9, 0));

        _instructions.Add(0x0000DA, new Instruction(p => JP_F_nn(Flag.Carry, p), "JP C, nn", 0x0000DA, 2));

        _instructions.Add(0x0000DB, new Instruction(p => IN_R_n(Register.A, p), "IN A, n", 0x0000DB, 1));

        _instructions.Add(0x0000DC, new Instruction(p => CALL_F_nn(Flag.Carry, p), "CALL C, nn", 0x0000DC, 2));

        _instructions.Add(0x0000DD, new Instruction(_ => PREFIX(0xDD), "PREFIX 0xDD", 0x0000DD, 0, 0, true));

        _instructions.Add(0x0000DE, new Instruction(p => SBC_R_n(Register.A, p), "SBC A, n", 0x0000DE, 1));

        _instructions.Add(0x0000DF, new Instruction(_ => RST(0x18), "RST 0x18", 0x0000DF, 0));

        _instructions.Add(0x0000E0, new Instruction(_ => RET_F(Flag.ParityOverflow, true), "RET PO", 0x0000E0, 0));

        _instructions.Add(0x0000E1, new Instruction(_ => POP_RR(RegisterPair.HL), "POP HL", 0x0000E1, 0));

        _instructions.Add(0x0000E2, new Instruction(p => JP_F_nn(Flag.ParityOverflow, p, true), "JP PO, nn", 0x0000E2, 2));

        _instructions.Add(0x0000E3, new Instruction(_ => EX_aRR_RR(RegisterPair.SP, RegisterPair.HL), "EX (SP), HL", 0x0000E3, 0));

        _instructions.Add(0x0000E4, new Instruction(p => CALL_F_nn(Flag.ParityOverflow, p, true), "CALL PO, nn", 0x0000E4, 2));

        _instructions.Add(0x0000E5, new Instruction(_ => PUSH_RR(RegisterPair.HL), "PUSH HL", 0x0000E5, 0));

        _instructions.Add(0x0000E6, new Instruction(p => AND_R_n(Register.A, p), "AND A, n", 0x0000E6, 1));

        _instructions.Add(0x0000E7, new Instruction(_ => RST(0x20), "RST 0x20", 0x0000E7, 0));

        _instructions.Add(0x0000E8, new Instruction(_ => RET_F(Flag.ParityOverflow), "RET PE", 0x0000E8, 0));

        _instructions.Add(0x0000E9, new Instruction(_ => JP_aRR(RegisterPair.HL), "JP (HL)", 0x0000E9, 0));

        _instructions.Add(0x0000EA, new Instruction(p => JP_F_nn(Flag.ParityOverflow, p), "JP PE, nn", 0x0000EA, 2));

        _instructions.Add(0x0000EB, new Instruction(_ => EX_RR_RR(RegisterPair.DE, RegisterPair.HL), "EX DE, HL", 0x0000EB, 0));

        _instructions.Add(0x0000EC, new Instruction(p => CALL_F_nn(Flag.ParityOverflow, p), "CALL PE, nn", 0x0000EC, 2));

        _instructions.Add(0x0000ED, new Instruction(_ => PREFIX(0xED), "PREFIX 0xED", 0x0000ED, 0, 0, true));

        _instructions.Add(0x0000EE, new Instruction(p => XOR_R_n(Register.A, p), "XOR A, n", 0x0000EE, 1));

        _instructions.Add(0x0000EF, new Instruction(_ => RST(0x28), "RST 0x28", 0x0000EF, 0));

        _instructions.Add(0x0000F0, new Instruction(_ => RET_F(Flag.Sign, true), "RET NS", 0x0000F0, 0));

        _instructions.Add(0x0000F1, new Instruction(_ => POP_RR(RegisterPair.AF), "POP AF", 0x0000F1, 0));

        _instructions.Add(0x0000F2, new Instruction(p => JP_F_nn(Flag.Sign, p, true), "JP NS, nn", 0x0000F2, 2));

        _instructions.Add(0x0000F3, new Instruction(_ => DI(), "DI", 0x0000F3, 0));

        _instructions.Add(0x0000F4, new Instruction(p => CALL_F_nn(Flag.Sign, p, true), "CALL NS, nn", 0x0000F4, 2));

        _instructions.Add(0x0000F5, new Instruction(_ => PUSH_RR(RegisterPair.AF), "PUSH AF", 0x0000F5, 0));

        _instructions.Add(0x0000F6, new Instruction(p => OR_R_n(Register.A, p), "OR A, n", 0x0000F6, 1));

        _instructions.Add(0x0000F7, new Instruction(_ => RST(0x30), "RST 0x30", 0x0000F7, 0));

        _instructions.Add(0x0000F8, new Instruction(_ => RET_F(Flag.Sign), "RET S", 0x0000F8, 0));

        _instructions.Add(0x0000F9, new Instruction(_ => LD_RR_RR(RegisterPair.SP, RegisterPair.HL), "LD SP, HL", 0x0000F9, 0));

        _instructions.Add(0x0000FA, new Instruction(p => JP_F_nn(Flag.Sign, p), "JP S, nn", 0x0000FA, 2));

        _instructions.Add(0x0000FB, new Instruction(_ => EI(), "EI", 0x0000FB, 0));

        _instructions.Add(0x0000FC, new Instruction(p => CALL_F_nn(Flag.Sign, p), "CALL S, nn", 0x0000FC, 2));

        _instructions.Add(0x0000FD, new Instruction(_ => PREFIX(0xFD), "PREFIX 0xFD", 0x0000FD, 0, 0, true));

        _instructions.Add(0x0000FE, new Instruction(p => CP_R_n(Register.A, p), "CP A, n", 0x0000FE, 1));

        _instructions.Add(0x0000FF, new Instruction(_ => RST(0x38), "RST 0x38", 0x0000FF, 0));
    }
}