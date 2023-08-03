using Zen.Z80.Processor;

// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    public void Initialise0xDDCB()
    {
        _instructions.Add(0xDDCB00, new Instruction(p => RLC_aRRd_R(RegisterPair.IX, p, Register.B), "RLC (IX + d), B", 0xDDCB00, 1));

        _instructions.Add(0xDDCB01, new Instruction(p => RLC_aRRd_R(RegisterPair.IX, p, Register.C), "RLC (IX + d), C", 0xDDCB01, 1));

        _instructions.Add(0xDDCB02, new Instruction(p => RLC_aRRd_R(RegisterPair.IX, p, Register.D), "RLC (IX + d), D", 0xDDCB02, 1));

        _instructions.Add(0xDDCB03, new Instruction(p => RLC_aRRd_R(RegisterPair.IX, p, Register.E), "RLC (IX + d), E", 0xDDCB03, 1));

        _instructions.Add(0xDDCB04, new Instruction(p => RLC_aRRd_R(RegisterPair.IX, p, Register.H), "RLC (IX + d), H", 0xDDCB04, 1));

        _instructions.Add(0xDDCB05, new Instruction(p => RLC_aRRd_R(RegisterPair.IX, p, Register.L), "RLC (IX + d), L", 0xDDCB05, 1));

        _instructions.Add(0xDDCB06, new Instruction(p => RLC_aRRd(RegisterPair.IX, p), "RLC (IX + d)", 0xDDCB06, 1));

        _instructions.Add(0xDDCB07, new Instruction(p => RLC_aRRd_R(RegisterPair.IX, p, Register.A), "RLC (IX + d), A", 0xDDCB07, 1));

        _instructions.Add(0xDDCB08, new Instruction(p => RRC_aRRd_R(RegisterPair.IX, p, Register.B), "RRC (IX + d), B", 0xDDCB08, 1));

        _instructions.Add(0xDDCB09, new Instruction(p => RRC_aRRd_R(RegisterPair.IX, p, Register.C), "RRC (IX + d), C", 0xDDCB09, 1));

        _instructions.Add(0xDDCB0A, new Instruction(p => RRC_aRRd_R(RegisterPair.IX, p, Register.D), "RRC (IX + d), D", 0xDDCB0A, 1));

        _instructions.Add(0xDDCB0B, new Instruction(p => RRC_aRRd_R(RegisterPair.IX, p, Register.E), "RRC (IX + d), E", 0xDDCB0B, 1));

        _instructions.Add(0xDDCB0C, new Instruction(p => RRC_aRRd_R(RegisterPair.IX, p, Register.H), "RRC (IX + d), H", 0xDDCB0C, 1));

        _instructions.Add(0xDDCB0D, new Instruction(p => RRC_aRRd_R(RegisterPair.IX, p, Register.L), "RRC (IX + d), L", 0xDDCB0D, 1));

        _instructions.Add(0xDDCB0E, new Instruction(p => RRC_aRRd(RegisterPair.IX, p), "RRC (IX + d)", 0xDDCB0E, 1));

        _instructions.Add(0xDDCB0F, new Instruction(p => RRC_aRRd_R(RegisterPair.IX, p, Register.A), "RRC (IX + d), A", 0xDDCB0F, 1));

        _instructions.Add(0xDDCB10, new Instruction(p => RL_aRRd_R(RegisterPair.IX, p, Register.B), "RL (IX + d), B", 0xDDCB10, 1));

        _instructions.Add(0xDDCB11, new Instruction(p => RL_aRRd_R(RegisterPair.IX, p, Register.C), "RL (IX + d), C", 0xDDCB11, 1));

        _instructions.Add(0xDDCB12, new Instruction(p => RL_aRRd_R(RegisterPair.IX, p, Register.D), "RL (IX + d), D", 0xDDCB12, 1));

        _instructions.Add(0xDDCB13, new Instruction(p => RL_aRRd_R(RegisterPair.IX, p, Register.E), "RL (IX + d), E", 0xDDCB13, 1));

        _instructions.Add(0xDDCB14, new Instruction(p => RL_aRRd_R(RegisterPair.IX, p, Register.H), "RL (IX + d), H", 0xDDCB14, 1));

        _instructions.Add(0xDDCB15, new Instruction(p => RL_aRRd_R(RegisterPair.IX, p, Register.L), "RL (IX + d), L", 0xDDCB15, 1));

        _instructions.Add(0xDDCB16, new Instruction(p => RL_aRRd(RegisterPair.IX, p), "RL (IX + d)", 0xDDCB16, 1));

        _instructions.Add(0xDDCB17, new Instruction(p => RL_aRRd_R(RegisterPair.IX, p, Register.A), "RL (IX + d), A", 0xDDCB17, 1));

        _instructions.Add(0xDDCB18, new Instruction(p => RR_aRRd_R(RegisterPair.IX, p, Register.B), "RR (IX + d), B", 0xDDCB18, 1));

        _instructions.Add(0xDDCB19, new Instruction(p => RR_aRRd_R(RegisterPair.IX, p, Register.C), "RR (IX + d), C", 0xDDCB19, 1));

        _instructions.Add(0xDDCB1A, new Instruction(p => RR_aRRd_R(RegisterPair.IX, p, Register.D), "RR (IX + d), D", 0xDDCB1A, 1));

        _instructions.Add(0xDDCB1B, new Instruction(p => RR_aRRd_R(RegisterPair.IX, p, Register.E), "RR (IX + d), E", 0xDDCB1B, 1));

        _instructions.Add(0xDDCB1C, new Instruction(p => RR_aRRd_R(RegisterPair.IX, p, Register.H), "RR (IX + d), H", 0xDDCB1C, 1));

        _instructions.Add(0xDDCB1D, new Instruction(p => RR_aRRd_R(RegisterPair.IX, p, Register.L), "RR (IX + d), L", 0xDDCB1D, 1));

        _instructions.Add(0xDDCB1E, new Instruction(p => RR_aRRd(RegisterPair.IX, p), "RR (IX + d)", 0xDDCB1E, 1));

        _instructions.Add(0xDDCB1F, new Instruction(p => RR_aRRd_R(RegisterPair.IX, p, Register.A), "RR (IX + d), A", 0xDDCB1F, 1));

        _instructions.Add(0xDDCB20, new Instruction(p => SLA_aRRd_R(RegisterPair.IX, p, Register.B), "SLA (IX + d), B", 0xDDCB20, 1));

        _instructions.Add(0xDDCB21, new Instruction(p => SLA_aRRd_R(RegisterPair.IX, p, Register.C), "SLA (IX + d), C", 0xDDCB21, 1));

        _instructions.Add(0xDDCB22, new Instruction(p => SLA_aRRd_R(RegisterPair.IX, p, Register.D), "SLA (IX + d), D", 0xDDCB22, 1));

        _instructions.Add(0xDDCB23, new Instruction(p => SLA_aRRd_R(RegisterPair.IX, p, Register.E), "SLA (IX + d), E", 0xDDCB23, 1));

        _instructions.Add(0xDDCB24, new Instruction(p => SLA_aRRd_R(RegisterPair.IX, p, Register.H), "SLA (IX + d), H", 0xDDCB24, 1));

        _instructions.Add(0xDDCB25, new Instruction(p => SLA_aRRd_R(RegisterPair.IX, p, Register.L), "SLA (IX + d), L", 0xDDCB25, 1));

        _instructions.Add(0xDDCB26, new Instruction(p => SLA_aRRd(RegisterPair.IX, p), "SLA (IX + d)", 0xDDCB26, 1));

        _instructions.Add(0xDDCB27, new Instruction(p => SLA_aRRd_R(RegisterPair.IX, p, Register.A), "SLA (IX + d), A", 0xDDCB27, 1));

        _instructions.Add(0xDDCB28, new Instruction(p => SRA_aRRd_R(RegisterPair.IX, p, Register.B), "SRA (IX + d), B", 0xDDCB28, 1));

        _instructions.Add(0xDDCB29, new Instruction(p => SRA_aRRd_R(RegisterPair.IX, p, Register.C), "SRA (IX + d), C", 0xDDCB29, 1));

        _instructions.Add(0xDDCB2A, new Instruction(p => SRA_aRRd_R(RegisterPair.IX, p, Register.D), "SRA (IX + d), D", 0xDDCB2A, 1));

        _instructions.Add(0xDDCB2B, new Instruction(p => SRA_aRRd_R(RegisterPair.IX, p, Register.E), "SRA (IX + d), E", 0xDDCB2B, 1));

        _instructions.Add(0xDDCB2C, new Instruction(p => SRA_aRRd_R(RegisterPair.IX, p, Register.H), "SRA (IX + d), H", 0xDDCB2C, 1));

        _instructions.Add(0xDDCB2D, new Instruction(p => SRA_aRRd_R(RegisterPair.IX, p, Register.L), "SRA (IX + d), L", 0xDDCB2D, 1));

        _instructions.Add(0xDDCB2E, new Instruction(p => SRA_aRRd(RegisterPair.IX, p), "SRA (IX + d)", 0xDDCB2E, 1));

        _instructions.Add(0xDDCB2F, new Instruction(p => SRA_aRRd_R(RegisterPair.IX, p, Register.A), "SRA (IX + d), A", 0xDDCB2F, 1));

        _instructions.Add(0xDDCB30, new Instruction(p => SLL_aRRd_R(RegisterPair.IX, p, Register.B), "SLL (IX + d), B", 0xDDCB30, 1));

        _instructions.Add(0xDDCB31, new Instruction(p => SLL_aRRd_R(RegisterPair.IX, p, Register.C), "SLL (IX + d), C", 0xDDCB31, 1));

        _instructions.Add(0xDDCB32, new Instruction(p => SLL_aRRd_R(RegisterPair.IX, p, Register.D), "SLL (IX + d), D", 0xDDCB32, 1));

        _instructions.Add(0xDDCB33, new Instruction(p => SLL_aRRd_R(RegisterPair.IX, p, Register.E), "SLL (IX + d), E", 0xDDCB33, 1));

        _instructions.Add(0xDDCB34, new Instruction(p => SLL_aRRd_R(RegisterPair.IX, p, Register.H), "SLL (IX + d), H", 0xDDCB34, 1));

        _instructions.Add(0xDDCB35, new Instruction(p => SLL_aRRd_R(RegisterPair.IX, p, Register.L), "SLL (IX + d), L", 0xDDCB35, 1));

        _instructions.Add(0xDDCB36, new Instruction(p => SLL_aRRd(RegisterPair.IX, p), "SLL (IX + d)", 0xDDCB36, 1));

        _instructions.Add(0xDDCB37, new Instruction(p => SLL_aRRd_R(RegisterPair.IX, p, Register.A), "SLL (IX + d), A", 0xDDCB37, 1));

        _instructions.Add(0xDDCB38, new Instruction(p => SRL_aRRd_R(RegisterPair.IX, p, Register.B), "SRL (IX + d), B", 0xDDCB38, 1));

        _instructions.Add(0xDDCB39, new Instruction(p => SRL_aRRd_R(RegisterPair.IX, p, Register.C), "SRL (IX + d), C", 0xDDCB39, 1));

        _instructions.Add(0xDDCB3A, new Instruction(p => SRL_aRRd_R(RegisterPair.IX, p, Register.D), "SRL (IX + d), D", 0xDDCB3A, 1));

        _instructions.Add(0xDDCB3B, new Instruction(p => SRL_aRRd_R(RegisterPair.IX, p, Register.E), "SRL (IX + d), E", 0xDDCB3B, 1));

        _instructions.Add(0xDDCB3C, new Instruction(p => SRL_aRRd_R(RegisterPair.IX, p, Register.H), "SRL (IX + d), H", 0xDDCB3C, 1));

        _instructions.Add(0xDDCB3D, new Instruction(p => SRL_aRRd_R(RegisterPair.IX, p, Register.L), "SRL (IX + d), L", 0xDDCB3D, 1));

        _instructions.Add(0xDDCB3E, new Instruction(p => SRL_aRRd(RegisterPair.IX, p), "SRL (IX + d)", 0xDDCB3E, 1));

        _instructions.Add(0xDDCB3F, new Instruction(p => SRL_aRRd_R(RegisterPair.IX, p, Register.A), "SRL (IX + d), A", 0xDDCB3F, 1));

        _instructions.Add(0xDDCB40, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IX, p), "BIT 0, (IX + d)", 0xDDCB40, 1));

        _instructions.Add(0xDDCB41, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IX, p), "BIT 0, (IX + d)", 0xDDCB41, 1));

        _instructions.Add(0xDDCB42, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IX, p), "BIT 0, (IX + d)", 0xDDCB42, 1));

        _instructions.Add(0xDDCB43, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IX, p), "BIT 0, (IX + d)", 0xDDCB43, 1));

        _instructions.Add(0xDDCB44, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IX, p), "BIT 0, (IX + d)", 0xDDCB44, 1));

        _instructions.Add(0xDDCB45, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IX, p), "BIT 0, (IX + d)", 0xDDCB45, 1));

        _instructions.Add(0xDDCB46, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IX, p), "BIT 0, (IX + d)", 0xDDCB46, 1));

        _instructions.Add(0xDDCB47, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IX, p), "BIT 0, (IX + d)", 0xDDCB47, 1));

        _instructions.Add(0xDDCB48, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IX, p), "BIT 1, (IX + d)", 0xDDCB48, 1));

        _instructions.Add(0xDDCB49, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IX, p), "BIT 1, (IX + d)", 0xDDCB49, 1));

        _instructions.Add(0xDDCB4A, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IX, p), "BIT 1, (IX + d)", 0xDDCB4A, 1));

        _instructions.Add(0xDDCB4B, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IX, p), "BIT 1, (IX + d)", 0xDDCB4B, 1));

        _instructions.Add(0xDDCB4C, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IX, p), "BIT 1, (IX + d)", 0xDDCB4C, 1));

        _instructions.Add(0xDDCB4D, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IX, p), "BIT 1, (IX + d)", 0xDDCB4D, 1));

        _instructions.Add(0xDDCB4E, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IX, p), "BIT 1, (IX + d)", 0xDDCB4E, 1));

        _instructions.Add(0xDDCB4F, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IX, p), "BIT 1, (IX + d)", 0xDDCB4F, 1));

        _instructions.Add(0xDDCB50, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IX, p), "BIT 2, (IX + d)", 0xDDCB50, 1));

        _instructions.Add(0xDDCB51, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IX, p), "BIT 2, (IX + d)", 0xDDCB51, 1));

        _instructions.Add(0xDDCB52, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IX, p), "BIT 2, (IX + d)", 0xDDCB52, 1));

        _instructions.Add(0xDDCB53, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IX, p), "BIT 2, (IX + d)", 0xDDCB53, 1));

        _instructions.Add(0xDDCB54, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IX, p), "BIT 2, (IX + d)", 0xDDCB54, 1));

        _instructions.Add(0xDDCB55, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IX, p), "BIT 2, (IX + d)", 0xDDCB55, 1));

        _instructions.Add(0xDDCB56, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IX, p), "BIT 2, (IX + d)", 0xDDCB56, 1));

        _instructions.Add(0xDDCB57, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IX, p), "BIT 2, (IX + d)", 0xDDCB57, 1));

        _instructions.Add(0xDDCB58, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IX, p), "BIT 3, (IX + d)", 0xDDCB58, 1));

        _instructions.Add(0xDDCB59, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IX, p), "BIT 3, (IX + d)", 0xDDCB59, 1));

        _instructions.Add(0xDDCB5A, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IX, p), "BIT 3, (IX + d)", 0xDDCB5A, 1));

        _instructions.Add(0xDDCB5B, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IX, p), "BIT 3, (IX + d)", 0xDDCB5B, 1));

        _instructions.Add(0xDDCB5C, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IX, p), "BIT 3, (IX + d)", 0xDDCB5C, 1));

        _instructions.Add(0xDDCB5D, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IX, p), "BIT 3, (IX + d)", 0xDDCB5D, 1));

        _instructions.Add(0xDDCB5E, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IX, p), "BIT 3, (IX + d)", 0xDDCB5E, 1));

        _instructions.Add(0xDDCB5F, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IX, p), "BIT 3, (IX + d)", 0xDDCB5F, 1));

        _instructions.Add(0xDDCB60, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IX, p), "BIT 4, (IX + d)", 0xDDCB60, 1));

        _instructions.Add(0xDDCB61, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IX, p), "BIT 4, (IX + d)", 0xDDCB61, 1));

        _instructions.Add(0xDDCB62, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IX, p), "BIT 4, (IX + d)", 0xDDCB62, 1));

        _instructions.Add(0xDDCB63, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IX, p), "BIT 4, (IX + d)", 0xDDCB63, 1));

        _instructions.Add(0xDDCB64, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IX, p), "BIT 4, (IX + d)", 0xDDCB64, 1));

        _instructions.Add(0xDDCB65, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IX, p), "BIT 4, (IX + d)", 0xDDCB65, 1));

        _instructions.Add(0xDDCB66, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IX, p), "BIT 4, (IX + d)", 0xDDCB66, 1));

        _instructions.Add(0xDDCB67, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IX, p), "BIT 4, (IX + d)", 0xDDCB67, 1));

        _instructions.Add(0xDDCB68, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IX, p), "BIT 5, (IX + d)", 0xDDCB68, 1));

        _instructions.Add(0xDDCB69, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IX, p), "BIT 5, (IX + d)", 0xDDCB69, 1));

        _instructions.Add(0xDDCB6A, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IX, p), "BIT 5, (IX + d)", 0xDDCB6A, 1));

        _instructions.Add(0xDDCB6B, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IX, p), "BIT 5, (IX + d)", 0xDDCB6B, 1));

        _instructions.Add(0xDDCB6C, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IX, p), "BIT 5, (IX + d)", 0xDDCB6C, 1));

        _instructions.Add(0xDDCB6D, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IX, p), "BIT 5, (IX + d)", 0xDDCB6D, 1));

        _instructions.Add(0xDDCB6E, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IX, p), "BIT 5, (IX + d)", 0xDDCB6E, 1));

        _instructions.Add(0xDDCB6F, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IX, p), "BIT 5, (IX + d)", 0xDDCB6F, 1));

        _instructions.Add(0xDDCB70, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IX, p), "BIT 6, (IX + d)", 0xDDCB70, 1));

        _instructions.Add(0xDDCB71, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IX, p), "BIT 6, (IX + d)", 0xDDCB71, 1));

        _instructions.Add(0xDDCB72, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IX, p), "BIT 6, (IX + d)", 0xDDCB72, 1));

        _instructions.Add(0xDDCB73, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IX, p), "BIT 6, (IX + d)", 0xDDCB73, 1));

        _instructions.Add(0xDDCB74, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IX, p), "BIT 6, (IX + d)", 0xDDCB74, 1));

        _instructions.Add(0xDDCB75, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IX, p), "BIT 6, (IX + d)", 0xDDCB75, 1));

        _instructions.Add(0xDDCB76, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IX, p), "BIT 6, (IX + d)", 0xDDCB76, 1));

        _instructions.Add(0xDDCB77, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IX, p), "BIT 6, (IX + d)", 0xDDCB77, 1));

        _instructions.Add(0xDDCB78, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IX, p), "BIT 7, (IX + d)", 0xDDCB78, 1));

        _instructions.Add(0xDDCB79, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IX, p), "BIT 7, (IX + d)", 0xDDCB79, 1));

        _instructions.Add(0xDDCB7A, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IX, p), "BIT 7, (IX + d)", 0xDDCB7A, 1));

        _instructions.Add(0xDDCB7B, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IX, p), "BIT 7, (IX + d)", 0xDDCB7B, 1));

        _instructions.Add(0xDDCB7C, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IX, p), "BIT 7, (IX + d)", 0xDDCB7C, 1));

        _instructions.Add(0xDDCB7D, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IX, p), "BIT 7, (IX + d)", 0xDDCB7D, 1));

        _instructions.Add(0xDDCB7E, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IX, p), "BIT 7, (IX + d)", 0xDDCB7E, 1));

        _instructions.Add(0xDDCB7F, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IX, p), "BIT 7, (IX + d)", 0xDDCB7F, 1));

        _instructions.Add(0xDDCB80, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IX, p, Register.B), "RES 0, (IX + d), B", 0xDDCB80, 1));

        _instructions.Add(0xDDCB81, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IX, p, Register.C), "RES 0, (IX + d), C", 0xDDCB81, 1));

        _instructions.Add(0xDDCB82, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IX, p, Register.D), "RES 0, (IX + d), D", 0xDDCB82, 1));

        _instructions.Add(0xDDCB83, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IX, p, Register.E), "RES 0, (IX + d), E", 0xDDCB83, 1));

        _instructions.Add(0xDDCB84, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IX, p, Register.H), "RES 0, (IX + d), H", 0xDDCB84, 1));

        _instructions.Add(0xDDCB85, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IX, p, Register.L), "RES 0, (IX + d), L", 0xDDCB85, 1));

        _instructions.Add(0xDDCB86, new Instruction(p => RES_b_aRRd(0x01, RegisterPair.IX, p), "RES 0, (IX + d)", 0xDDCB86, 1));

        _instructions.Add(0xDDCB87, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IX, p, Register.A), "RES 0, (IX + d), A", 0xDDCB87, 1));

        _instructions.Add(0xDDCB88, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IX, p, Register.B), "RES 1, (IX + d), B", 0xDDCB88, 1));

        _instructions.Add(0xDDCB89, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IX, p, Register.C), "RES 1, (IX + d), C", 0xDDCB89, 1));

        _instructions.Add(0xDDCB8A, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IX, p, Register.D), "RES 1, (IX + d), D", 0xDDCB8A, 1));

        _instructions.Add(0xDDCB8B, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IX, p, Register.E), "RES 1, (IX + d), E", 0xDDCB8B, 1));

        _instructions.Add(0xDDCB8C, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IX, p, Register.H), "RES 1, (IX + d), H", 0xDDCB8C, 1));

        _instructions.Add(0xDDCB8D, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IX, p, Register.L), "RES 1, (IX + d), L", 0xDDCB8D, 1));

        _instructions.Add(0xDDCB8E, new Instruction(p => RES_b_aRRd(0x02, RegisterPair.IX, p), "RES 1, (IX + d)", 0xDDCB8E, 1));

        _instructions.Add(0xDDCB8F, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IX, p, Register.A), "RES 1, (IX + d), A", 0xDDCB8F, 1));

        _instructions.Add(0xDDCB90, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IX, p, Register.B), "RES 2, (IX + d), B", 0xDDCB90, 1));

        _instructions.Add(0xDDCB91, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IX, p, Register.C), "RES 2, (IX + d), C", 0xDDCB91, 1));

        _instructions.Add(0xDDCB92, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IX, p, Register.D), "RES 2, (IX + d), D", 0xDDCB92, 1));

        _instructions.Add(0xDDCB93, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IX, p, Register.E), "RES 2, (IX + d), E", 0xDDCB93, 1));

        _instructions.Add(0xDDCB94, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IX, p, Register.H), "RES 2, (IX + d), H", 0xDDCB94, 1));

        _instructions.Add(0xDDCB95, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IX, p, Register.L), "RES 2, (IX + d), L", 0xDDCB95, 1));

        _instructions.Add(0xDDCB96, new Instruction(p => RES_b_aRRd(0x04, RegisterPair.IX, p), "RES 2, (IX + d)", 0xDDCB96, 1));

        _instructions.Add(0xDDCB97, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IX, p, Register.A), "RES 2, (IX + d), A", 0xDDCB97, 1));

        _instructions.Add(0xDDCB98, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IX, p, Register.B), "RES 3, (IX + d), B", 0xDDCB98, 1));

        _instructions.Add(0xDDCB99, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IX, p, Register.C), "RES 3, (IX + d), C", 0xDDCB99, 1));

        _instructions.Add(0xDDCB9A, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IX, p, Register.D), "RES 3, (IX + d), D", 0xDDCB9A, 1));

        _instructions.Add(0xDDCB9B, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IX, p, Register.E), "RES 3, (IX + d), E", 0xDDCB9B, 1));

        _instructions.Add(0xDDCB9C, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IX, p, Register.H), "RES 3, (IX + d), H", 0xDDCB9C, 1));

        _instructions.Add(0xDDCB9D, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IX, p, Register.L), "RES 3, (IX + d), L", 0xDDCB9D, 1));

        _instructions.Add(0xDDCB9E, new Instruction(p => RES_b_aRRd(0x08, RegisterPair.IX, p), "RES 3, (IX + d)", 0xDDCB9E, 1));

        _instructions.Add(0xDDCB9F, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IX, p, Register.A), "RES 3, (IX + d), A", 0xDDCB9F, 1));

        _instructions.Add(0xDDCBA0, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IX, p, Register.B), "RES 4, (IX + d), B", 0xDDCBA0, 1));

        _instructions.Add(0xDDCBA1, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IX, p, Register.C), "RES 4, (IX + d), C", 0xDDCBA1, 1));

        _instructions.Add(0xDDCBA2, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IX, p, Register.D), "RES 4, (IX + d), D", 0xDDCBA2, 1));

        _instructions.Add(0xDDCBA3, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IX, p, Register.E), "RES 4, (IX + d), E", 0xDDCBA3, 1));

        _instructions.Add(0xDDCBA4, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IX, p, Register.H), "RES 4, (IX + d), H", 0xDDCBA4, 1));

        _instructions.Add(0xDDCBA5, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IX, p, Register.L), "RES 4, (IX + d), L", 0xDDCBA5, 1));

        _instructions.Add(0xDDCBA6, new Instruction(p => RES_b_aRRd(0x10, RegisterPair.IX, p), "RES 4, (IX + d)", 0xDDCBA6, 1));

        _instructions.Add(0xDDCBA7, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IX, p, Register.A), "RES 4, (IX + d), A", 0xDDCBA7, 1));

        _instructions.Add(0xDDCBA8, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IX, p, Register.B), "RES 5, (IX + d), B", 0xDDCBA8, 1));

        _instructions.Add(0xDDCBA9, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IX, p, Register.C), "RES 5, (IX + d), C", 0xDDCBA9, 1));

        _instructions.Add(0xDDCBAA, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IX, p, Register.D), "RES 5, (IX + d), D", 0xDDCBAA, 1));

        _instructions.Add(0xDDCBAB, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IX, p, Register.E), "RES 5, (IX + d), E", 0xDDCBAB, 1));

        _instructions.Add(0xDDCBAC, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IX, p, Register.H), "RES 5, (IX + d), H", 0xDDCBAC, 1));

        _instructions.Add(0xDDCBAD, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IX, p, Register.L), "RES 5, (IX + d), L", 0xDDCBAD, 1));

        _instructions.Add(0xDDCBAE, new Instruction(p => RES_b_aRRd(0x20, RegisterPair.IX, p), "RES 5, (IX + d)", 0xDDCBAE, 1));

        _instructions.Add(0xDDCBAF, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IX, p, Register.A), "RES 5, (IX + d), A", 0xDDCBAF, 1));

        _instructions.Add(0xDDCBB0, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IX, p, Register.B), "RES 6, (IX + d), B", 0xDDCBB0, 1));

        _instructions.Add(0xDDCBB1, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IX, p, Register.C), "RES 6, (IX + d), C", 0xDDCBB1, 1));

        _instructions.Add(0xDDCBB2, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IX, p, Register.D), "RES 6, (IX + d), D", 0xDDCBB2, 1));

        _instructions.Add(0xDDCBB3, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IX, p, Register.E), "RES 6, (IX + d), E", 0xDDCBB3, 1));

        _instructions.Add(0xDDCBB4, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IX, p, Register.H), "RES 6, (IX + d), H", 0xDDCBB4, 1));

        _instructions.Add(0xDDCBB5, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IX, p, Register.L), "RES 6, (IX + d), L", 0xDDCBB5, 1));

        _instructions.Add(0xDDCBB6, new Instruction(p => RES_b_aRRd(0x40, RegisterPair.IX, p), "RES 6, (IX + d)", 0xDDCBB6, 1));

        _instructions.Add(0xDDCBB7, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IX, p, Register.A), "RES 6, (IX + d), A", 0xDDCBB7, 1));

        _instructions.Add(0xDDCBB8, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IX, p, Register.B), "RES 7, (IX + d), B", 0xDDCBB8, 1));

        _instructions.Add(0xDDCBB9, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IX, p, Register.C), "RES 7, (IX + d), C", 0xDDCBB9, 1));

        _instructions.Add(0xDDCBBA, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IX, p, Register.D), "RES 7, (IX + d), D", 0xDDCBBA, 1));

        _instructions.Add(0xDDCBBB, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IX, p, Register.E), "RES 7, (IX + d), E", 0xDDCBBB, 1));

        _instructions.Add(0xDDCBBC, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IX, p, Register.H), "RES 7, (IX + d), H", 0xDDCBBC, 1));

        _instructions.Add(0xDDCBBD, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IX, p, Register.L), "RES 7, (IX + d), L", 0xDDCBBD, 1));

        _instructions.Add(0xDDCBBE, new Instruction(p => RES_b_aRRd(0x80, RegisterPair.IX, p), "RES 7, (IX + d)", 0xDDCBBE, 1));

        _instructions.Add(0xDDCBBF, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IX, p, Register.A), "RES 7, (IX + d), A", 0xDDCBBF, 1));

        _instructions.Add(0xDDCBC0, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IX, p, Register.B), "SET 0, (IX + d), B", 0xDDCBC0, 1));

        _instructions.Add(0xDDCBC1, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IX, p, Register.C), "SET 0, (IX + d), C", 0xDDCBC1, 1));

        _instructions.Add(0xDDCBC2, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IX, p, Register.D), "SET 0, (IX + d), D", 0xDDCBC2, 1));

        _instructions.Add(0xDDCBC3, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IX, p, Register.E), "SET 0, (IX + d), E", 0xDDCBC3, 1));

        _instructions.Add(0xDDCBC4, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IX, p, Register.H), "SET 0, (IX + d), H", 0xDDCBC4, 1));

        _instructions.Add(0xDDCBC5, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IX, p, Register.L), "SET 0, (IX + d), L", 0xDDCBC5, 1));

        _instructions.Add(0xDDCBC6, new Instruction(p => SET_b_aRRd(0x01, RegisterPair.IX, p), "SET 0, (IX + d)", 0xDDCBC6, 1));

        _instructions.Add(0xDDCBC7, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IX, p, Register.A), "SET 0, (IX + d), A", 0xDDCBC7, 1));

        _instructions.Add(0xDDCBC8, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IX, p, Register.B), "SET 1, (IX + d), B", 0xDDCBC8, 1));

        _instructions.Add(0xDDCBC9, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IX, p, Register.C), "SET 1, (IX + d), C", 0xDDCBC9, 1));

        _instructions.Add(0xDDCBCA, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IX, p, Register.D), "SET 1, (IX + d), D", 0xDDCBCA, 1));

        _instructions.Add(0xDDCBCB, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IX, p, Register.E), "SET 1, (IX + d), E", 0xDDCBCB, 1));

        _instructions.Add(0xDDCBCC, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IX, p, Register.H), "SET 1, (IX + d), H", 0xDDCBCC, 1));

        _instructions.Add(0xDDCBCD, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IX, p, Register.L), "SET 1, (IX + d), L", 0xDDCBCD, 1));

        _instructions.Add(0xDDCBCE, new Instruction(p => SET_b_aRRd(0x02, RegisterPair.IX, p), "SET 1, (IX + d)", 0xDDCBCE, 1));

        _instructions.Add(0xDDCBCF, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IX, p, Register.A), "SET 1, (IX + d), A", 0xDDCBCF, 1));

        _instructions.Add(0xDDCBD0, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IX, p, Register.B), "SET 2, (IX + d), B", 0xDDCBD0, 1));

        _instructions.Add(0xDDCBD1, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IX, p, Register.C), "SET 2, (IX + d), C", 0xDDCBD1, 1));

        _instructions.Add(0xDDCBD2, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IX, p, Register.D), "SET 2, (IX + d), D", 0xDDCBD2, 1));

        _instructions.Add(0xDDCBD3, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IX, p, Register.E), "SET 2, (IX + d), E", 0xDDCBD3, 1));

        _instructions.Add(0xDDCBD4, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IX, p, Register.H), "SET 2, (IX + d), H", 0xDDCBD4, 1));

        _instructions.Add(0xDDCBD5, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IX, p, Register.L), "SET 2, (IX + d), L", 0xDDCBD5, 1));

        _instructions.Add(0xDDCBD6, new Instruction(p => SET_b_aRRd(0x04, RegisterPair.IX, p), "SET 2, (IX + d)", 0xDDCBD6, 1));

        _instructions.Add(0xDDCBD7, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IX, p, Register.A), "SET 2, (IX + d), A", 0xDDCBD7, 1));

        _instructions.Add(0xDDCBD8, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IX, p, Register.B), "SET 3, (IX + d), B", 0xDDCBD8, 1));

        _instructions.Add(0xDDCBD9, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IX, p, Register.C), "SET 3, (IX + d), C", 0xDDCBD9, 1));

        _instructions.Add(0xDDCBDA, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IX, p, Register.D), "SET 3, (IX + d), D", 0xDDCBDA, 1));

        _instructions.Add(0xDDCBDB, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IX, p, Register.E), "SET 3, (IX + d), E", 0xDDCBDB, 1));

        _instructions.Add(0xDDCBDC, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IX, p, Register.H), "SET 3, (IX + d), H", 0xDDCBDC, 1));

        _instructions.Add(0xDDCBDD, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IX, p, Register.L), "SET 3, (IX + d), L", 0xDDCBDD, 1));

        _instructions.Add(0xDDCBDE, new Instruction(p => SET_b_aRRd(0x08, RegisterPair.IX, p), "SET 3, (IX + d)", 0xDDCBDE, 1));

        _instructions.Add(0xDDCBDF, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IX, p, Register.A), "SET 3, (IX + d), A", 0xDDCBDF, 1));

        _instructions.Add(0xDDCBE0, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IX, p, Register.B), "SET 4, (IX + d), B", 0xDDCBE0, 1));

        _instructions.Add(0xDDCBE1, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IX, p, Register.C), "SET 4, (IX + d), C", 0xDDCBE1, 1));

        _instructions.Add(0xDDCBE2, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IX, p, Register.D), "SET 4, (IX + d), D", 0xDDCBE2, 1));

        _instructions.Add(0xDDCBE3, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IX, p, Register.E), "SET 4, (IX + d), E", 0xDDCBE3, 1));

        _instructions.Add(0xDDCBE4, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IX, p, Register.H), "SET 4, (IX + d), H", 0xDDCBE4, 1));

        _instructions.Add(0xDDCBE5, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IX, p, Register.L), "SET 4, (IX + d), L", 0xDDCBE5, 1));

        _instructions.Add(0xDDCBE6, new Instruction(p => SET_b_aRRd(0x10, RegisterPair.IX, p), "SET 4, (IX + d)", 0xDDCBE6, 1));

        _instructions.Add(0xDDCBE7, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IX, p, Register.A), "SET 4, (IX + d), A", 0xDDCBE7, 1));

        _instructions.Add(0xDDCBE8, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IX, p, Register.B), "SET 5, (IX + d), B", 0xDDCBE8, 1));

        _instructions.Add(0xDDCBE9, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IX, p, Register.C), "SET 5, (IX + d), C", 0xDDCBE9, 1));

        _instructions.Add(0xDDCBEA, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IX, p, Register.D), "SET 5, (IX + d), D", 0xDDCBEA, 1));

        _instructions.Add(0xDDCBEB, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IX, p, Register.E), "SET 5, (IX + d), E", 0xDDCBEB, 1));

        _instructions.Add(0xDDCBEC, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IX, p, Register.H), "SET 5, (IX + d), H", 0xDDCBEC, 1));

        _instructions.Add(0xDDCBED, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IX, p, Register.L), "SET 5, (IX + d), L", 0xDDCBED, 1));

        _instructions.Add(0xDDCBEE, new Instruction(p => SET_b_aRRd(0x20, RegisterPair.IX, p), "SET 5, (IX + d)", 0xDDCBEE, 1));

        _instructions.Add(0xDDCBEF, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IX, p, Register.A), "SET 5, (IX + d), A", 0xDDCBEF, 1));

        _instructions.Add(0xDDCBF0, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IX, p, Register.B), "SET 6, (IX + d), B", 0xDDCBF0, 1));

        _instructions.Add(0xDDCBF1, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IX, p, Register.C), "SET 6, (IX + d), C", 0xDDCBF1, 1));

        _instructions.Add(0xDDCBF2, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IX, p, Register.D), "SET 6, (IX + d), D", 0xDDCBF2, 1));

        _instructions.Add(0xDDCBF3, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IX, p, Register.E), "SET 6, (IX + d), E", 0xDDCBF3, 1));

        _instructions.Add(0xDDCBF4, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IX, p, Register.H), "SET 6, (IX + d), H", 0xDDCBF4, 1));

        _instructions.Add(0xDDCBF5, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IX, p, Register.L), "SET 6, (IX + d), L", 0xDDCBF5, 1));

        _instructions.Add(0xDDCBF6, new Instruction(p => SET_b_aRRd(0x40, RegisterPair.IX, p), "SET 6, (IX + d)", 0xDDCBF6, 1));

        _instructions.Add(0xDDCBF7, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IX, p, Register.A), "SET 6, (IX + d), A", 0xDDCBF7, 1));

        _instructions.Add(0xDDCBF8, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IX, p, Register.B), "SET 7, (IX + d), B", 0xDDCBF8, 1));

        _instructions.Add(0xDDCBF9, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IX, p, Register.C), "SET 7, (IX + d), C", 0xDDCBF9, 1));

        _instructions.Add(0xDDCBFA, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IX, p, Register.D), "SET 7, (IX + d), D", 0xDDCBFA, 1));

        _instructions.Add(0xDDCBFB, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IX, p, Register.E), "SET 7, (IX + d), E", 0xDDCBFB, 1));

        _instructions.Add(0xDDCBFC, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IX, p, Register.H), "SET 7, (IX + d), H", 0xDDCBFC, 1));

        _instructions.Add(0xDDCBFD, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IX, p, Register.L), "SET 7, (IX + d), L", 0xDDCBFD, 1));

        _instructions.Add(0xDDCBFE, new Instruction(p => SET_b_aRRd(0x80, RegisterPair.IX, p), "SET 7, (IX + d)", 0xDDCBFE, 1));

        _instructions.Add(0xDDCBFF, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IX, p, Register.A), "SET 7, (IX + d), A", 0xDDCBFF, 1));
    }
}