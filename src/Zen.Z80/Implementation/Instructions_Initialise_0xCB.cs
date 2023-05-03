using Zen.Z80.Processor;

// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    public void Initialise0xCB()
    {
        _instructions.Add(0x00CB00, new Instruction(_ => RLC_R(Register.B), "RLC B", 0x00CB00, 0));

        _instructions.Add(0x00CB01, new Instruction(_ => RLC_R(Register.C), "RLC C", 0x00CB01, 0));

        _instructions.Add(0x00CB02, new Instruction(_ => RLC_R(Register.D), "RLC D", 0x00CB02, 0));

        _instructions.Add(0x00CB03, new Instruction(_ => RLC_R(Register.E), "RLC E", 0x00CB03, 0));

        _instructions.Add(0x00CB04, new Instruction(_ => RLC_R(Register.H), "RLC H", 0x00CB04, 0));

        _instructions.Add(0x00CB05, new Instruction(_ => RLC_R(Register.L), "RLC L", 0x00CB05, 0));

        _instructions.Add(0x00CB06, new Instruction(_ => RLC_aRR(RegisterPair.HL), "RLC (HL)", 0x00CB06, 0));

        _instructions.Add(0x00CB07, new Instruction(_ => RLC_R(Register.A), "RLC A", 0x00CB07, 0));

        _instructions.Add(0x00CB08, new Instruction(_ => RRC_R(Register.B), "RRC B", 0x00CB08, 0));

        _instructions.Add(0x00CB09, new Instruction(_ => RRC_R(Register.C), "RRC C", 0x00CB09, 0));

        _instructions.Add(0x00CB0A, new Instruction(_ => RRC_R(Register.D), "RRC D", 0x00CB0A, 0));

        _instructions.Add(0x00CB0B, new Instruction(_ => RRC_R(Register.E), "RRC E", 0x00CB0B, 0));

        _instructions.Add(0x00CB0C, new Instruction(_ => RRC_R(Register.H), "RRC H", 0x00CB0C, 0));

        _instructions.Add(0x00CB0D, new Instruction(_ => RRC_R(Register.L), "RRC L", 0x00CB0D, 0));

        _instructions.Add(0x00CB0E, new Instruction(_ => RRC_aRR(RegisterPair.HL), "RRC (HL)", 0x00CB0E, 0));

        _instructions.Add(0x00CB0F, new Instruction(_ => RRC_R(Register.A), "RRC A", 0x00CB0F, 0));

        _instructions.Add(0x00CB10, new Instruction(_ => RL_R(Register.B), "RL B", 0x00CB10, 0));

        _instructions.Add(0x00CB11, new Instruction(_ => RL_R(Register.C), "RL C", 0x00CB11, 0));

        _instructions.Add(0x00CB12, new Instruction(_ => RL_R(Register.D), "RL D", 0x00CB12, 0));

        _instructions.Add(0x00CB13, new Instruction(_ => RL_R(Register.E), "RL E", 0x00CB13, 0));

        _instructions.Add(0x00CB14, new Instruction(_ => RL_R(Register.H), "RL H", 0x00CB14, 0));

        _instructions.Add(0x00CB15, new Instruction(_ => RL_R(Register.L), "RL L", 0x00CB15, 0));

        _instructions.Add(0x00CB16, new Instruction(_ => RL_aRR(RegisterPair.HL), "RL (HL)", 0x00CB16, 0));

        _instructions.Add(0x00CB17, new Instruction(_ => RL_R(Register.A), "RL A", 0x00CB17, 0));

        _instructions.Add(0x00CB18, new Instruction(_ => RR_R(Register.B), "RR B", 0x00CB18, 0));

        _instructions.Add(0x00CB19, new Instruction(_ => RR_R(Register.C), "RR C", 0x00CB19, 0));

        _instructions.Add(0x00CB1A, new Instruction(_ => RR_R(Register.D), "RR D", 0x00CB1A, 0));

        _instructions.Add(0x00CB1B, new Instruction(_ => RR_R(Register.E), "RR E", 0x00CB1B, 0));

        _instructions.Add(0x00CB1C, new Instruction(_ => RR_R(Register.H), "RR H", 0x00CB1C, 0));

        _instructions.Add(0x00CB1D, new Instruction(_ => RR_R(Register.L), "RR L", 0x00CB1D, 0));

        _instructions.Add(0x00CB1E, new Instruction(_ => RR_aRR(RegisterPair.HL), "RR (HL)", 0x00CB1E, 0));

        _instructions.Add(0x00CB1F, new Instruction(_ => RR_R(Register.A), "RR A", 0x00CB1F, 0));

        _instructions.Add(0x00CB20, new Instruction(_ => SLA_R(Register.B), "SLA B", 0x00CB20, 0));

        _instructions.Add(0x00CB21, new Instruction(_ => SLA_R(Register.C), "SLA C", 0x00CB21, 0));

        _instructions.Add(0x00CB22, new Instruction(_ => SLA_R(Register.D), "SLA D", 0x00CB22, 0));

        _instructions.Add(0x00CB23, new Instruction(_ => SLA_R(Register.E), "SLA E", 0x00CB23, 0));

        _instructions.Add(0x00CB24, new Instruction(_ => SLA_R(Register.H), "SLA H", 0x00CB24, 0));

        _instructions.Add(0x00CB25, new Instruction(_ => SLA_R(Register.L), "SLA L", 0x00CB25, 0));

        _instructions.Add(0x00CB26, new Instruction(_ => SLA_aRR(RegisterPair.HL), "SLA (HL)", 0x00CB26, 0));

        _instructions.Add(0x00CB27, new Instruction(_ => SLA_R(Register.A), "SLA A", 0x00CB27, 0));

        _instructions.Add(0x00CB28, new Instruction(_ => SRA_R(Register.B), "SRA B", 0x00CB28, 0));

        _instructions.Add(0x00CB29, new Instruction(_ => SRA_R(Register.C), "SRA C", 0x00CB29, 0));

        _instructions.Add(0x00CB2A, new Instruction(_ => SRA_R(Register.D), "SRA D", 0x00CB2A, 0));

        _instructions.Add(0x00CB2B, new Instruction(_ => SRA_R(Register.E), "SRA E", 0x00CB2B, 0));

        _instructions.Add(0x00CB2C, new Instruction(_ => SRA_R(Register.H), "SRA H", 0x00CB2C, 0));

        _instructions.Add(0x00CB2D, new Instruction(_ => SRA_R(Register.L), "SRA L", 0x00CB2D, 0));

        _instructions.Add(0x00CB2E, new Instruction(_ => SRA_aRR(RegisterPair.HL), "SRA (HL)", 0x00CB2E, 0));

        _instructions.Add(0x00CB2F, new Instruction(_ => SRA_R(Register.A), "SRA A", 0x00CB2F, 0));

        _instructions.Add(0x00CB30, new Instruction(_ => SLL_R(Register.B), "SLL B", 0x00CB30, 0));

        _instructions.Add(0x00CB31, new Instruction(_ => SLL_R(Register.C), "SLL C", 0x00CB31, 0));

        _instructions.Add(0x00CB32, new Instruction(_ => SLL_R(Register.D), "SLL D", 0x00CB32, 0));

        _instructions.Add(0x00CB33, new Instruction(_ => SLL_R(Register.E), "SLL E", 0x00CB33, 0));

        _instructions.Add(0x00CB34, new Instruction(_ => SLL_R(Register.H), "SLL H", 0x00CB34, 0));

        _instructions.Add(0x00CB35, new Instruction(_ => SLL_R(Register.L), "SLL L", 0x00CB35, 0));

        _instructions.Add(0x00CB36, new Instruction(_ => SLL_aRR(RegisterPair.HL), "SLL (HL)", 0x00CB36, 0));

        _instructions.Add(0x00CB37, new Instruction(_ => SLL_R(Register.A), "SLL A", 0x00CB37, 0));

        _instructions.Add(0x00CB38, new Instruction(_ => SRL_R(Register.B), "SRL B", 0x00CB38, 0));

        _instructions.Add(0x00CB39, new Instruction(_ => SRL_R(Register.C), "SRL C", 0x00CB39, 0));

        _instructions.Add(0x00CB3A, new Instruction(_ => SRL_R(Register.D), "SRL D", 0x00CB3A, 0));

        _instructions.Add(0x00CB3B, new Instruction(_ => SRL_R(Register.E), "SRL E", 0x00CB3B, 0));

        _instructions.Add(0x00CB3C, new Instruction(_ => SRL_R(Register.H), "SRL H", 0x00CB3C, 0));

        _instructions.Add(0x00CB3D, new Instruction(_ => SRL_R(Register.L), "SRL L", 0x00CB3D, 0));

        _instructions.Add(0x00CB3E, new Instruction(_ => SRL_aRR(RegisterPair.HL), "SRL (HL)", 0x00CB3E, 0));

        _instructions.Add(0x00CB3F, new Instruction(_ => SRL_R(Register.A), "SRL A", 0x00CB3F, 0));

        _instructions.Add(0x00CB40, new Instruction(_ => BIT_b_R(0x01, Register.B), "BIT 0, B", 0x00CB40, 0));

        _instructions.Add(0x00CB41, new Instruction(_ => BIT_b_R(0x01, Register.C), "BIT 0, C", 0x00CB41, 0));

        _instructions.Add(0x00CB42, new Instruction(_ => BIT_b_R(0x01, Register.D), "BIT 0, D", 0x00CB42, 0));

        _instructions.Add(0x00CB43, new Instruction(_ => BIT_b_R(0x01, Register.E), "BIT 0, E", 0x00CB43, 0));

        _instructions.Add(0x00CB44, new Instruction(_ => BIT_b_R(0x01, Register.H), "BIT 0, H", 0x00CB44, 0));

        _instructions.Add(0x00CB45, new Instruction(_ => BIT_b_R(0x01, Register.L), "BIT 0, L", 0x00CB45, 0));

        _instructions.Add(0x00CB46, new Instruction(_ => BIT_b_aRR(0x01, RegisterPair.HL), "BIT 0, (HL)", 0x00CB46, 0));

        _instructions.Add(0x00CB47, new Instruction(_ => BIT_b_R(0x01, Register.A), "BIT 0, A", 0x00CB47, 0));

        _instructions.Add(0x00CB48, new Instruction(_ => BIT_b_R(0x02, Register.B), "BIT 1, B", 0x00CB48, 0));

        _instructions.Add(0x00CB49, new Instruction(_ => BIT_b_R(0x02, Register.C), "BIT 1, C", 0x00CB49, 0));

        _instructions.Add(0x00CB4A, new Instruction(_ => BIT_b_R(0x02, Register.D), "BIT 1, D", 0x00CB4A, 0));

        _instructions.Add(0x00CB4B, new Instruction(_ => BIT_b_R(0x02, Register.E), "BIT 1, E", 0x00CB4B, 0));

        _instructions.Add(0x00CB4C, new Instruction(_ => BIT_b_R(0x02, Register.H), "BIT 1, H", 0x00CB4C, 0));

        _instructions.Add(0x00CB4D, new Instruction(_ => BIT_b_R(0x02, Register.L), "BIT 1, L", 0x00CB4D, 0));

        _instructions.Add(0x00CB4E, new Instruction(_ => BIT_b_aRR(0x02, RegisterPair.HL), "BIT 1, (HL)", 0x00CB4E, 0));

        _instructions.Add(0x00CB4F, new Instruction(_ => BIT_b_R(0x02, Register.A), "BIT 1, A", 0x00CB4F, 0));

        _instructions.Add(0x00CB50, new Instruction(_ => BIT_b_R(0x04, Register.B), "BIT 2, B", 0x00CB50, 0));

        _instructions.Add(0x00CB51, new Instruction(_ => BIT_b_R(0x04, Register.C), "BIT 2, C", 0x00CB51, 0));

        _instructions.Add(0x00CB52, new Instruction(_ => BIT_b_R(0x04, Register.D), "BIT 2, D", 0x00CB52, 0));

        _instructions.Add(0x00CB53, new Instruction(_ => BIT_b_R(0x04, Register.E), "BIT 2, E", 0x00CB53, 0));

        _instructions.Add(0x00CB54, new Instruction(_ => BIT_b_R(0x04, Register.H), "BIT 2, H", 0x00CB54, 0));

        _instructions.Add(0x00CB55, new Instruction(_ => BIT_b_R(0x04, Register.L), "BIT 2, L", 0x00CB55, 0));

        _instructions.Add(0x00CB56, new Instruction(_ => BIT_b_aRR(0x04, RegisterPair.HL), "BIT 2, (HL)", 0x00CB56, 0));

        _instructions.Add(0x00CB57, new Instruction(_ => BIT_b_R(0x04, Register.A), "BIT 2, A", 0x00CB57, 0));

        _instructions.Add(0x00CB58, new Instruction(_ => BIT_b_R(0x08, Register.B), "BIT 3, B", 0x00CB58, 0));

        _instructions.Add(0x00CB59, new Instruction(_ => BIT_b_R(0x08, Register.C), "BIT 3, C", 0x00CB59, 0));

        _instructions.Add(0x00CB5A, new Instruction(_ => BIT_b_R(0x08, Register.D), "BIT 3, D", 0x00CB5A, 0));

        _instructions.Add(0x00CB5B, new Instruction(_ => BIT_b_R(0x08, Register.E), "BIT 3, E", 0x00CB5B, 0));

        _instructions.Add(0x00CB5C, new Instruction(_ => BIT_b_R(0x08, Register.H), "BIT 3, H", 0x00CB5C, 0));

        _instructions.Add(0x00CB5D, new Instruction(_ => BIT_b_R(0x08, Register.L), "BIT 3, L", 0x00CB5D, 0));

        _instructions.Add(0x00CB5E, new Instruction(_ => BIT_b_aRR(0x08, RegisterPair.HL), "BIT 3, (HL)", 0x00CB5E, 0));

        _instructions.Add(0x00CB5F, new Instruction(_ => BIT_b_R(0x08, Register.A), "BIT 3, A", 0x00CB5F, 0));

        _instructions.Add(0x00CB60, new Instruction(_ => BIT_b_R(0x10, Register.B), "BIT 4, B", 0x00CB60, 0));

        _instructions.Add(0x00CB61, new Instruction(_ => BIT_b_R(0x10, Register.C), "BIT 4, C", 0x00CB61, 0));

        _instructions.Add(0x00CB62, new Instruction(_ => BIT_b_R(0x10, Register.D), "BIT 4, D", 0x00CB62, 0));

        _instructions.Add(0x00CB63, new Instruction(_ => BIT_b_R(0x10, Register.E), "BIT 4, E", 0x00CB63, 0));

        _instructions.Add(0x00CB64, new Instruction(_ => BIT_b_R(0x10, Register.H), "BIT 4, H", 0x00CB64, 0));

        _instructions.Add(0x00CB65, new Instruction(_ => BIT_b_R(0x10, Register.L), "BIT 4, L", 0x00CB65, 0));

        _instructions.Add(0x00CB66, new Instruction(_ => BIT_b_aRR(0x10, RegisterPair.HL), "BIT 4, (HL)", 0x00CB66, 0));

        _instructions.Add(0x00CB67, new Instruction(_ => BIT_b_R(0x10, Register.A), "BIT 4, A", 0x00CB67, 0));

        _instructions.Add(0x00CB68, new Instruction(_ => BIT_b_R(0x20, Register.B), "BIT 5, B", 0x00CB68, 0));

        _instructions.Add(0x00CB69, new Instruction(_ => BIT_b_R(0x20, Register.C), "BIT 5, C", 0x00CB69, 0));

        _instructions.Add(0x00CB6A, new Instruction(_ => BIT_b_R(0x20, Register.D), "BIT 5, D", 0x00CB6A, 0));

        _instructions.Add(0x00CB6B, new Instruction(_ => BIT_b_R(0x20, Register.E), "BIT 5, E", 0x00CB6B, 0));

        _instructions.Add(0x00CB6C, new Instruction(_ => BIT_b_R(0x20, Register.H), "BIT 5, H", 0x00CB6C, 0));

        _instructions.Add(0x00CB6D, new Instruction(_ => BIT_b_R(0x20, Register.L), "BIT 5, L", 0x00CB6D, 0));

        _instructions.Add(0x00CB6E, new Instruction(_ => BIT_b_aRR(0x20, RegisterPair.HL), "BIT 5, (HL)", 0x00CB6E, 0));

        _instructions.Add(0x00CB6F, new Instruction(_ => BIT_b_R(0x20, Register.A), "BIT 5, A", 0x00CB6F, 0));

        _instructions.Add(0x00CB70, new Instruction(_ => BIT_b_R(0x40, Register.B), "BIT 6, B", 0x00CB70, 0));

        _instructions.Add(0x00CB71, new Instruction(_ => BIT_b_R(0x40, Register.C), "BIT 6, C", 0x00CB71, 0));

        _instructions.Add(0x00CB72, new Instruction(_ => BIT_b_R(0x40, Register.D), "BIT 6, D", 0x00CB72, 0));

        _instructions.Add(0x00CB73, new Instruction(_ => BIT_b_R(0x40, Register.E), "BIT 6, E", 0x00CB73, 0));

        _instructions.Add(0x00CB74, new Instruction(_ => BIT_b_R(0x40, Register.H), "BIT 6, H", 0x00CB74, 0));

        _instructions.Add(0x00CB75, new Instruction(_ => BIT_b_R(0x40, Register.L), "BIT 6, L", 0x00CB75, 0));

        _instructions.Add(0x00CB76, new Instruction(_ => BIT_b_aRR(0x40, RegisterPair.HL), "BIT 6, (HL)", 0x00CB76, 0));

        _instructions.Add(0x00CB77, new Instruction(_ => BIT_b_R(0x40, Register.A), "BIT 6, A", 0x00CB77, 0));

        _instructions.Add(0x00CB78, new Instruction(_ => BIT_b_R(0x80, Register.B), "BIT 7, B", 0x00CB78, 0));

        _instructions.Add(0x00CB79, new Instruction(_ => BIT_b_R(0x80, Register.C), "BIT 7, C", 0x00CB79, 0));

        _instructions.Add(0x00CB7A, new Instruction(_ => BIT_b_R(0x80, Register.D), "BIT 7, D", 0x00CB7A, 0));

        _instructions.Add(0x00CB7B, new Instruction(_ => BIT_b_R(0x80, Register.E), "BIT 7, E", 0x00CB7B, 0));

        _instructions.Add(0x00CB7C, new Instruction(_ => BIT_b_R(0x80, Register.H), "BIT 7, H", 0x00CB7C, 0));

        _instructions.Add(0x00CB7D, new Instruction(_ => BIT_b_R(0x80, Register.L), "BIT 7, L", 0x00CB7D, 0));

        _instructions.Add(0x00CB7E, new Instruction(_ => BIT_b_aRR(0x80, RegisterPair.HL), "BIT 7, (HL)", 0x00CB7E, 0));

        _instructions.Add(0x00CB7F, new Instruction(_ => BIT_b_R(0x80, Register.A), "BIT 7, A", 0x00CB7F, 0));

        _instructions.Add(0x00CB80, new Instruction(_ => RES_b_R(0x01, Register.B), "RES 0, B", 0x00CB80, 0));

        _instructions.Add(0x00CB81, new Instruction(_ => RES_b_R(0x01, Register.C), "RES 0, C", 0x00CB81, 0));

        _instructions.Add(0x00CB82, new Instruction(_ => RES_b_R(0x01, Register.D), "RES 0, D", 0x00CB82, 0));

        _instructions.Add(0x00CB83, new Instruction(_ => RES_b_R(0x01, Register.E), "RES 0, E", 0x00CB83, 0));

        _instructions.Add(0x00CB84, new Instruction(_ => RES_b_R(0x01, Register.H), "RES 0, H", 0x00CB84, 0));

        _instructions.Add(0x00CB85, new Instruction(_ => RES_b_R(0x01, Register.L), "RES 0, L", 0x00CB85, 0));

        _instructions.Add(0x00CB86, new Instruction(_ => RES_b_aRR(0x01, RegisterPair.HL), "RES 0, (HL)", 0x00CB86, 0));

        _instructions.Add(0x00CB87, new Instruction(_ => RES_b_R(0x01, Register.A), "RES 0, A", 0x00CB87, 0));

        _instructions.Add(0x00CB88, new Instruction(_ => RES_b_R(0x02, Register.B), "RES 1, B", 0x00CB88, 0));

        _instructions.Add(0x00CB89, new Instruction(_ => RES_b_R(0x02, Register.C), "RES 1, C", 0x00CB89, 0));

        _instructions.Add(0x00CB8A, new Instruction(_ => RES_b_R(0x02, Register.D), "RES 1, D", 0x00CB8A, 0));

        _instructions.Add(0x00CB8B, new Instruction(_ => RES_b_R(0x02, Register.E), "RES 1, E", 0x00CB8B, 0));

        _instructions.Add(0x00CB8C, new Instruction(_ => RES_b_R(0x02, Register.H), "RES 1, H", 0x00CB8C, 0));

        _instructions.Add(0x00CB8D, new Instruction(_ => RES_b_R(0x02, Register.L), "RES 1, L", 0x00CB8D, 0));

        _instructions.Add(0x00CB8E, new Instruction(_ => RES_b_aRR(0x02, RegisterPair.HL), "RES 1, (HL)", 0x00CB8E, 0));

        _instructions.Add(0x00CB8F, new Instruction(_ => RES_b_R(0x02, Register.A), "RES 1, A", 0x00CB8F, 0));

        _instructions.Add(0x00CB90, new Instruction(_ => RES_b_R(0x04, Register.B), "RES 2, B", 0x00CB90, 0));

        _instructions.Add(0x00CB91, new Instruction(_ => RES_b_R(0x04, Register.C), "RES 2, C", 0x00CB91, 0));

        _instructions.Add(0x00CB92, new Instruction(_ => RES_b_R(0x04, Register.D), "RES 2, D", 0x00CB92, 0));

        _instructions.Add(0x00CB93, new Instruction(_ => RES_b_R(0x04, Register.E), "RES 2, E", 0x00CB93, 0));

        _instructions.Add(0x00CB94, new Instruction(_ => RES_b_R(0x04, Register.H), "RES 2, H", 0x00CB94, 0));

        _instructions.Add(0x00CB95, new Instruction(_ => RES_b_R(0x04, Register.L), "RES 2, L", 0x00CB95, 0));

        _instructions.Add(0x00CB96, new Instruction(_ => RES_b_aRR(0x04, RegisterPair.HL), "RES 2, (HL)", 0x00CB96, 0));

        _instructions.Add(0x00CB97, new Instruction(_ => RES_b_R(0x04, Register.A), "RES 2, A", 0x00CB97, 0));

        _instructions.Add(0x00CB98, new Instruction(_ => RES_b_R(0x08, Register.B), "RES 3, B", 0x00CB98, 0));

        _instructions.Add(0x00CB99, new Instruction(_ => RES_b_R(0x08, Register.C), "RES 3, C", 0x00CB99, 0));

        _instructions.Add(0x00CB9A, new Instruction(_ => RES_b_R(0x08, Register.D), "RES 3, D", 0x00CB9A, 0));

        _instructions.Add(0x00CB9B, new Instruction(_ => RES_b_R(0x08, Register.E), "RES 3, E", 0x00CB9B, 0));

        _instructions.Add(0x00CB9C, new Instruction(_ => RES_b_R(0x08, Register.H), "RES 3, H", 0x00CB9C, 0));

        _instructions.Add(0x00CB9D, new Instruction(_ => RES_b_R(0x08, Register.L), "RES 3, L", 0x00CB9D, 0));

        _instructions.Add(0x00CB9E, new Instruction(_ => RES_b_aRR(0x08, RegisterPair.HL), "RES 3, (HL)", 0x00CB9E, 0));

        _instructions.Add(0x00CB9F, new Instruction(_ => RES_b_R(0x08, Register.A), "RES 3, A", 0x00CB9F, 0));

        _instructions.Add(0x00CBA0, new Instruction(_ => RES_b_R(0x10, Register.B), "RES 4, B", 0x00CBA0, 0));

        _instructions.Add(0x00CBA1, new Instruction(_ => RES_b_R(0x10, Register.C), "RES 4, C", 0x00CBA1, 0));

        _instructions.Add(0x00CBA2, new Instruction(_ => RES_b_R(0x10, Register.D), "RES 4, D", 0x00CBA2, 0));

        _instructions.Add(0x00CBA3, new Instruction(_ => RES_b_R(0x10, Register.E), "RES 4, E", 0x00CBA3, 0));

        _instructions.Add(0x00CBA4, new Instruction(_ => RES_b_R(0x10, Register.H), "RES 4, H", 0x00CBA4, 0));

        _instructions.Add(0x00CBA5, new Instruction(_ => RES_b_R(0x10, Register.L), "RES 4, L", 0x00CBA5, 0));

        _instructions.Add(0x00CBA6, new Instruction(_ => RES_b_aRR(0x10, RegisterPair.HL), "RES 4, (HL)", 0x00CBA6, 0));

        _instructions.Add(0x00CBA7, new Instruction(_ => RES_b_R(0x10, Register.A), "RES 4, A", 0x00CBA7, 0));

        _instructions.Add(0x00CBA8, new Instruction(_ => RES_b_R(0x20, Register.B), "RES 5, B", 0x00CBA8, 0));

        _instructions.Add(0x00CBA9, new Instruction(_ => RES_b_R(0x20, Register.C), "RES 5, C", 0x00CBA9, 0));

        _instructions.Add(0x00CBAA, new Instruction(_ => RES_b_R(0x20, Register.D), "RES 5, D", 0x00CBAA, 0));

        _instructions.Add(0x00CBAB, new Instruction(_ => RES_b_R(0x20, Register.E), "RES 5, E", 0x00CBAB, 0));

        _instructions.Add(0x00CBAC, new Instruction(_ => RES_b_R(0x20, Register.H), "RES 5, H", 0x00CBAC, 0));

        _instructions.Add(0x00CBAD, new Instruction(_ => RES_b_R(0x20, Register.L), "RES 5, L", 0x00CBAD, 0));

        _instructions.Add(0x00CBAE, new Instruction(_ => RES_b_aRR(0x20, RegisterPair.HL), "RES 5, (HL)", 0x00CBAE, 0));

        _instructions.Add(0x00CBAF, new Instruction(_ => RES_b_R(0x20, Register.A), "RES 5, A", 0x00CBAF, 0));

        _instructions.Add(0x00CBB0, new Instruction(_ => RES_b_R(0x40, Register.B), "RES 6, B", 0x00CBB0, 0));

        _instructions.Add(0x00CBB1, new Instruction(_ => RES_b_R(0x40, Register.C), "RES 6, C", 0x00CBB1, 0));

        _instructions.Add(0x00CBB2, new Instruction(_ => RES_b_R(0x40, Register.D), "RES 6, D", 0x00CBB2, 0));

        _instructions.Add(0x00CBB3, new Instruction(_ => RES_b_R(0x40, Register.E), "RES 6, E", 0x00CBB3, 0));

        _instructions.Add(0x00CBB4, new Instruction(_ => RES_b_R(0x40, Register.H), "RES 6, H", 0x00CBB4, 0));

        _instructions.Add(0x00CBB5, new Instruction(_ => RES_b_R(0x40, Register.L), "RES 6, L", 0x00CBB5, 0));

        _instructions.Add(0x00CBB6, new Instruction(_ => RES_b_aRR(0x40, RegisterPair.HL), "RES 6, (HL)", 0x00CBB6, 0));

        _instructions.Add(0x00CBB7, new Instruction(_ => RES_b_R(0x40, Register.A), "RES 6, A", 0x00CBB7, 0));

        _instructions.Add(0x00CBB8, new Instruction(_ => RES_b_R(0x80, Register.B), "RES 7, B", 0x00CBB8, 0));

        _instructions.Add(0x00CBB9, new Instruction(_ => RES_b_R(0x80, Register.C), "RES 7, C", 0x00CBB9, 0));

        _instructions.Add(0x00CBBA, new Instruction(_ => RES_b_R(0x80, Register.D), "RES 7, D", 0x00CBBA, 0));

        _instructions.Add(0x00CBBB, new Instruction(_ => RES_b_R(0x80, Register.E), "RES 7, E", 0x00CBBB, 0));

        _instructions.Add(0x00CBBC, new Instruction(_ => RES_b_R(0x80, Register.H), "RES 7, H", 0x00CBBC, 0));

        _instructions.Add(0x00CBBD, new Instruction(_ => RES_b_R(0x80, Register.L), "RES 7, L", 0x00CBBD, 0));

        _instructions.Add(0x00CBBE, new Instruction(_ => RES_b_aRR(0x80, RegisterPair.HL), "RES 7, (HL)", 0x00CBBE, 0));

        _instructions.Add(0x00CBBF, new Instruction(_ => RES_b_R(0x80, Register.A), "RES 7, A", 0x00CBBF, 0));

        _instructions.Add(0x00CBC0, new Instruction(_ => SET_b_R(0x01, Register.B), "SET 0, B", 0x00CBC0, 0));

        _instructions.Add(0x00CBC1, new Instruction(_ => SET_b_R(0x01, Register.C), "SET 0, C", 0x00CBC1, 0));

        _instructions.Add(0x00CBC2, new Instruction(_ => SET_b_R(0x01, Register.D), "SET 0, D", 0x00CBC2, 0));

        _instructions.Add(0x00CBC3, new Instruction(_ => SET_b_R(0x01, Register.E), "SET 0, E", 0x00CBC3, 0));

        _instructions.Add(0x00CBC4, new Instruction(_ => SET_b_R(0x01, Register.H), "SET 0, H", 0x00CBC4, 0));

        _instructions.Add(0x00CBC5, new Instruction(_ => SET_b_R(0x01, Register.L), "SET 0, L", 0x00CBC5, 0));

        _instructions.Add(0x00CBC6, new Instruction(_ => SET_b_aRR(0x01, RegisterPair.HL), "SET 0, (HL)", 0x00CBC6, 0));

        _instructions.Add(0x00CBC7, new Instruction(_ => SET_b_R(0x01, Register.A), "SET 0, A", 0x00CBC7, 0));

        _instructions.Add(0x00CBC8, new Instruction(_ => SET_b_R(0x02, Register.B), "SET 1, B", 0x00CBC8, 0));

        _instructions.Add(0x00CBC9, new Instruction(_ => SET_b_R(0x02, Register.C), "SET 1, C", 0x00CBC9, 0));

        _instructions.Add(0x00CBCA, new Instruction(_ => SET_b_R(0x02, Register.D), "SET 1, D", 0x00CBCA, 0));

        _instructions.Add(0x00CBCB, new Instruction(_ => SET_b_R(0x02, Register.E), "SET 1, E", 0x00CBCB, 0));

        _instructions.Add(0x00CBCC, new Instruction(_ => SET_b_R(0x02, Register.H), "SET 1, H", 0x00CBCC, 0));

        _instructions.Add(0x00CBCD, new Instruction(_ => SET_b_R(0x02, Register.L), "SET 1, L", 0x00CBCD, 0));

        _instructions.Add(0x00CBCE, new Instruction(_ => SET_b_aRR(0x02, RegisterPair.HL), "SET 1, (HL)", 0x00CBCE, 0));

        _instructions.Add(0x00CBCF, new Instruction(_ => SET_b_R(0x02, Register.A), "SET 1, A", 0x00CBCF, 0));

        _instructions.Add(0x00CBD0, new Instruction(_ => SET_b_R(0x04, Register.B), "SET 2, B", 0x00CBD0, 0));

        _instructions.Add(0x00CBD1, new Instruction(_ => SET_b_R(0x04, Register.C), "SET 2, C", 0x00CBD1, 0));

        _instructions.Add(0x00CBD2, new Instruction(_ => SET_b_R(0x04, Register.D), "SET 2, D", 0x00CBD2, 0));

        _instructions.Add(0x00CBD3, new Instruction(_ => SET_b_R(0x04, Register.E), "SET 2, E", 0x00CBD3, 0));

        _instructions.Add(0x00CBD4, new Instruction(_ => SET_b_R(0x04, Register.H), "SET 2, H", 0x00CBD4, 0));

        _instructions.Add(0x00CBD5, new Instruction(_ => SET_b_R(0x04, Register.L), "SET 2, L", 0x00CBD5, 0));

        _instructions.Add(0x00CBD6, new Instruction(_ => SET_b_aRR(0x04, RegisterPair.HL), "SET 2, (HL)", 0x00CBD6, 0));

        _instructions.Add(0x00CBD7, new Instruction(_ => SET_b_R(0x04, Register.A), "SET 2, A", 0x00CBD7, 0));

        _instructions.Add(0x00CBD8, new Instruction(_ => SET_b_R(0x08, Register.B), "SET 3, B", 0x00CBD8, 0));

        _instructions.Add(0x00CBD9, new Instruction(_ => SET_b_R(0x08, Register.C), "SET 3, C", 0x00CBD9, 0));

        _instructions.Add(0x00CBDA, new Instruction(_ => SET_b_R(0x08, Register.D), "SET 3, D", 0x00CBDA, 0));

        _instructions.Add(0x00CBDB, new Instruction(_ => SET_b_R(0x08, Register.E), "SET 3, E", 0x00CBDB, 0));

        _instructions.Add(0x00CBDC, new Instruction(_ => SET_b_R(0x08, Register.H), "SET 3, H", 0x00CBDC, 0));

        _instructions.Add(0x00CBDD, new Instruction(_ => SET_b_R(0x08, Register.L), "SET 3, L", 0x00CBDD, 0));

        _instructions.Add(0x00CBDE, new Instruction(_ => SET_b_aRR(0x08, RegisterPair.HL), "SET 3, (HL)", 0x00CBDE, 0));

        _instructions.Add(0x00CBDF, new Instruction(_ => SET_b_R(0x08, Register.A), "SET 3, A", 0x00CBDF, 0));

        _instructions.Add(0x00CBE0, new Instruction(_ => SET_b_R(0x10, Register.B), "SET 4, B", 0x00CBE0, 0));

        _instructions.Add(0x00CBE1, new Instruction(_ => SET_b_R(0x10, Register.C), "SET 4, C", 0x00CBE1, 0));

        _instructions.Add(0x00CBE2, new Instruction(_ => SET_b_R(0x10, Register.D), "SET 4, D", 0x00CBE2, 0));

        _instructions.Add(0x00CBE3, new Instruction(_ => SET_b_R(0x10, Register.E), "SET 4, E", 0x00CBE3, 0));

        _instructions.Add(0x00CBE4, new Instruction(_ => SET_b_R(0x10, Register.H), "SET 4, H", 0x00CBE4, 0));

        _instructions.Add(0x00CBE5, new Instruction(_ => SET_b_R(0x10, Register.L), "SET 4, L", 0x00CBE5, 0));

        _instructions.Add(0x00CBE6, new Instruction(_ => SET_b_aRR(0x10, RegisterPair.HL), "SET 4, (HL)", 0x00CBE6, 0));

        _instructions.Add(0x00CBE7, new Instruction(_ => SET_b_R(0x10, Register.A), "SET 4, A", 0x00CBE7, 0));

        _instructions.Add(0x00CBE8, new Instruction(_ => SET_b_R(0x20, Register.B), "SET 5, B", 0x00CBE8, 0));

        _instructions.Add(0x00CBE9, new Instruction(_ => SET_b_R(0x20, Register.C), "SET 5, C", 0x00CBE9, 0));

        _instructions.Add(0x00CBEA, new Instruction(_ => SET_b_R(0x20, Register.D), "SET 5, D", 0x00CBEA, 0));

        _instructions.Add(0x00CBEB, new Instruction(_ => SET_b_R(0x20, Register.E), "SET 5, E", 0x00CBEB, 0));

        _instructions.Add(0x00CBEC, new Instruction(_ => SET_b_R(0x20, Register.H), "SET 5, H", 0x00CBEC, 0));

        _instructions.Add(0x00CBED, new Instruction(_ => SET_b_R(0x20, Register.L), "SET 5, L", 0x00CBED, 0));

        _instructions.Add(0x00CBEE, new Instruction(_ => SET_b_aRR(0x20, RegisterPair.HL), "SET 5, (HL)", 0x00CBEE, 0));

        _instructions.Add(0x00CBEF, new Instruction(_ => SET_b_R(0x20, Register.A), "SET 5, A", 0x00CBEF, 0));

        _instructions.Add(0x00CBF0, new Instruction(_ => SET_b_R(0x40, Register.B), "SET 6, B", 0x00CBF0, 0));

        _instructions.Add(0x00CBF1, new Instruction(_ => SET_b_R(0x40, Register.C), "SET 6, C", 0x00CBF1, 0));

        _instructions.Add(0x00CBF2, new Instruction(_ => SET_b_R(0x40, Register.D), "SET 6, D", 0x00CBF2, 0));

        _instructions.Add(0x00CBF3, new Instruction(_ => SET_b_R(0x40, Register.E), "SET 6, E", 0x00CBF3, 0));

        _instructions.Add(0x00CBF4, new Instruction(_ => SET_b_R(0x40, Register.H), "SET 6, H", 0x00CBF4, 0));

        _instructions.Add(0x00CBF5, new Instruction(_ => SET_b_R(0x40, Register.L), "SET 6, L", 0x00CBF5, 0));

        _instructions.Add(0x00CBF6, new Instruction(_ => SET_b_aRR(0x40, RegisterPair.HL), "SET 6, (HL)", 0x00CBF6, 0));

        _instructions.Add(0x00CBF7, new Instruction(_ => SET_b_R(0x40, Register.A), "SET 6, A", 0x00CBF7, 0));

        _instructions.Add(0x00CBF8, new Instruction(_ => SET_b_R(0x80, Register.B), "SET 7, B", 0x00CBF8, 0));

        _instructions.Add(0x00CBF9, new Instruction(_ => SET_b_R(0x80, Register.C), "SET 7, C", 0x00CBF9, 0));

        _instructions.Add(0x00CBFA, new Instruction(_ => SET_b_R(0x80, Register.D), "SET 7, D", 0x00CBFA, 0));

        _instructions.Add(0x00CBFB, new Instruction(_ => SET_b_R(0x80, Register.E), "SET 7, E", 0x00CBFB, 0));

        _instructions.Add(0x00CBFC, new Instruction(_ => SET_b_R(0x80, Register.H), "SET 7, H", 0x00CBFC, 0));

        _instructions.Add(0x00CBFD, new Instruction(_ => SET_b_R(0x80, Register.L), "SET 7, L", 0x00CBFD, 0));

        _instructions.Add(0x00CBFE, new Instruction(_ => SET_b_aRR(0x80, RegisterPair.HL), "SET 7, (HL)", 0x00CBFE, 0));

        _instructions.Add(0x00CBFF, new Instruction(_ => SET_b_R(0x80, Register.A), "SET 7, A", 0x00CBFF, 0));
    }
}