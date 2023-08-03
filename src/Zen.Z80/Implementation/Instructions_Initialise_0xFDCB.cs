using Zen.Z80.Processor;

// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    public void Initialise0xFDCB()
    {
        _instructions.Add(0xFDCB00, new Instruction(p => RLC_aRRd_R(RegisterPair.IY, p, Register.B), "RLC (IY + d), B", 0xFDCB00, 1));

        _instructions.Add(0xFDCB01, new Instruction(p => RLC_aRRd_R(RegisterPair.IY, p, Register.C), "RLC (IY + d), C", 0xFDCB01, 1));

        _instructions.Add(0xFDCB02, new Instruction(p => RLC_aRRd_R(RegisterPair.IY, p, Register.D), "RLC (IY + d), D", 0xFDCB02, 1));

        _instructions.Add(0xFDCB03, new Instruction(p => RLC_aRRd_R(RegisterPair.IY, p, Register.E), "RLC (IY + d), E", 0xFDCB03, 1));

        _instructions.Add(0xFDCB04, new Instruction(p => RLC_aRRd_R(RegisterPair.IY, p, Register.H), "RLC (IY + d), H", 0xFDCB04, 1));

        _instructions.Add(0xFDCB05, new Instruction(p => RLC_aRRd_R(RegisterPair.IY, p, Register.L), "RLC (IY + d), L", 0xFDCB05, 1));

        _instructions.Add(0xFDCB06, new Instruction(p => RLC_aRRd(RegisterPair.IY, p), "RLC (IY + d)", 0xFDCB06, 1));

        _instructions.Add(0xFDCB07, new Instruction(p => RLC_aRRd_R(RegisterPair.IY, p, Register.A), "RLC (IY + d), A", 0xFDCB07, 1));

        _instructions.Add(0xFDCB08, new Instruction(p => RRC_aRRd_R(RegisterPair.IY, p, Register.B), "RRC (IY + d), B", 0xFDCB08, 1));

        _instructions.Add(0xFDCB09, new Instruction(p => RRC_aRRd_R(RegisterPair.IY, p, Register.C), "RRC (IY + d), C", 0xFDCB09, 1));

        _instructions.Add(0xFDCB0A, new Instruction(p => RRC_aRRd_R(RegisterPair.IY, p, Register.D), "RRC (IY + d), D", 0xFDCB0A, 1));

        _instructions.Add(0xFDCB0B, new Instruction(p => RRC_aRRd_R(RegisterPair.IY, p, Register.E), "RRC (IY + d), E", 0xFDCB0B, 1));

        _instructions.Add(0xFDCB0C, new Instruction(p => RRC_aRRd_R(RegisterPair.IY, p, Register.H), "RRC (IY + d), H", 0xFDCB0C, 1));

        _instructions.Add(0xFDCB0D, new Instruction(p => RRC_aRRd_R(RegisterPair.IY, p, Register.L), "RRC (IY + d), L", 0xFDCB0D, 1));

        _instructions.Add(0xFDCB0E, new Instruction(p => RRC_aRRd(RegisterPair.IY, p), "RRC (IY + d)", 0xFDCB0E, 1));

        _instructions.Add(0xFDCB0F, new Instruction(p => RRC_aRRd_R(RegisterPair.IY, p, Register.A), "RRC (IY + d), A", 0xFDCB0F, 1));

        _instructions.Add(0xFDCB10, new Instruction(p => RL_aRRd_R(RegisterPair.IY, p, Register.B), "RL (IY + d), B", 0xFDCB10, 1));

        _instructions.Add(0xFDCB11, new Instruction(p => RL_aRRd_R(RegisterPair.IY, p, Register.C), "RL (IY + d), C", 0xFDCB11, 1));

        _instructions.Add(0xFDCB12, new Instruction(p => RL_aRRd_R(RegisterPair.IY, p, Register.D), "RL (IY + d), D", 0xFDCB12, 1));

        _instructions.Add(0xFDCB13, new Instruction(p => RL_aRRd_R(RegisterPair.IY, p, Register.E), "RL (IY + d), E", 0xFDCB13, 1));

        _instructions.Add(0xFDCB14, new Instruction(p => RL_aRRd_R(RegisterPair.IY, p, Register.H), "RL (IY + d), H", 0xFDCB14, 1));

        _instructions.Add(0xFDCB15, new Instruction(p => RL_aRRd_R(RegisterPair.IY, p, Register.L), "RL (IY + d), L", 0xFDCB15, 1));

        _instructions.Add(0xFDCB16, new Instruction(p => RL_aRRd(RegisterPair.IY, p), "RL (IY + d)", 0xFDCB16, 1));

        _instructions.Add(0xFDCB17, new Instruction(p => RL_aRRd_R(RegisterPair.IY, p, Register.A), "RL (IY + d), A", 0xFDCB17, 1));

        _instructions.Add(0xFDCB18, new Instruction(p => RR_aRRd_R(RegisterPair.IY, p, Register.B), "RR (IY + d), B", 0xFDCB18, 1));

        _instructions.Add(0xFDCB19, new Instruction(p => RR_aRRd_R(RegisterPair.IY, p, Register.C), "RR (IY + d), C", 0xFDCB19, 1));

        _instructions.Add(0xFDCB1A, new Instruction(p => RR_aRRd_R(RegisterPair.IY, p, Register.D), "RR (IY + d), D", 0xFDCB1A, 1));

        _instructions.Add(0xFDCB1B, new Instruction(p => RR_aRRd_R(RegisterPair.IY, p, Register.E), "RR (IY + d), E", 0xFDCB1B, 1));

        _instructions.Add(0xFDCB1C, new Instruction(p => RR_aRRd_R(RegisterPair.IY, p, Register.H), "RR (IY + d), H", 0xFDCB1C, 1));

        _instructions.Add(0xFDCB1D, new Instruction(p => RR_aRRd_R(RegisterPair.IY, p, Register.L), "RR (IY + d), L", 0xFDCB1D, 1));

        _instructions.Add(0xFDCB1E, new Instruction(p => RR_aRRd(RegisterPair.IY, p), "RR (IY + d)", 0xFDCB1E, 1));

        _instructions.Add(0xFDCB1F, new Instruction(p => RR_aRRd_R(RegisterPair.IY, p, Register.A), "RR (IY + d), A", 0xFDCB1F, 1));

        _instructions.Add(0xFDCB20, new Instruction(p => SLA_aRRd_R(RegisterPair.IY, p, Register.B), "SLA (IY + d), B", 0xFDCB20, 1));

        _instructions.Add(0xFDCB21, new Instruction(p => SLA_aRRd_R(RegisterPair.IY, p, Register.C), "SLA (IY + d), C", 0xFDCB21, 1));

        _instructions.Add(0xFDCB22, new Instruction(p => SLA_aRRd_R(RegisterPair.IY, p, Register.D), "SLA (IY + d), D", 0xFDCB22, 1));

        _instructions.Add(0xFDCB23, new Instruction(p => SLA_aRRd_R(RegisterPair.IY, p, Register.E), "SLA (IY + d), E", 0xFDCB23, 1));

        _instructions.Add(0xFDCB24, new Instruction(p => SLA_aRRd_R(RegisterPair.IY, p, Register.H), "SLA (IY + d), H", 0xFDCB24, 1));

        _instructions.Add(0xFDCB25, new Instruction(p => SLA_aRRd_R(RegisterPair.IY, p, Register.L), "SLA (IY + d), L", 0xFDCB25, 1));

        _instructions.Add(0xFDCB26, new Instruction(p => SLA_aRRd(RegisterPair.IY, p), "SLA (IY + d)", 0xFDCB26, 1));

        _instructions.Add(0xFDCB27, new Instruction(p => SLA_aRRd_R(RegisterPair.IY, p, Register.A), "SLA (IY + d), A", 0xFDCB27, 1));

        _instructions.Add(0xFDCB28, new Instruction(p => SRA_aRRd_R(RegisterPair.IY, p, Register.B), "SRA (IY + d), B", 0xFDCB28, 1));

        _instructions.Add(0xFDCB29, new Instruction(p => SRA_aRRd_R(RegisterPair.IY, p, Register.C), "SRA (IY + d), C", 0xFDCB29, 1));

        _instructions.Add(0xFDCB2A, new Instruction(p => SRA_aRRd_R(RegisterPair.IY, p, Register.D), "SRA (IY + d), D", 0xFDCB2A, 1));

        _instructions.Add(0xFDCB2B, new Instruction(p => SRA_aRRd_R(RegisterPair.IY, p, Register.E), "SRA (IY + d), E", 0xFDCB2B, 1));

        _instructions.Add(0xFDCB2C, new Instruction(p => SRA_aRRd_R(RegisterPair.IY, p, Register.H), "SRA (IY + d), H", 0xFDCB2C, 1));

        _instructions.Add(0xFDCB2D, new Instruction(p => SRA_aRRd_R(RegisterPair.IY, p, Register.L), "SRA (IY + d), L", 0xFDCB2D, 1));

        _instructions.Add(0xFDCB2E, new Instruction(p => SRA_aRRd(RegisterPair.IY, p), "SRA (IY + d)", 0xFDCB2E, 1));

        _instructions.Add(0xFDCB2F, new Instruction(p => SRA_aRRd_R(RegisterPair.IY, p, Register.A), "SRA (IY + d), A", 0xFDCB2F, 1));

        _instructions.Add(0xFDCB30, new Instruction(p => SLL_aRRd_R(RegisterPair.IY, p, Register.B), "SLL (IY + d), B", 0xFDCB30, 1));

        _instructions.Add(0xFDCB31, new Instruction(p => SLL_aRRd_R(RegisterPair.IY, p, Register.C), "SLL (IY + d), C", 0xFDCB31, 1));

        _instructions.Add(0xFDCB32, new Instruction(p => SLL_aRRd_R(RegisterPair.IY, p, Register.D), "SLL (IY + d), D", 0xFDCB32, 1));

        _instructions.Add(0xFDCB33, new Instruction(p => SLL_aRRd_R(RegisterPair.IY, p, Register.E), "SLL (IY + d), E", 0xFDCB33, 1));

        _instructions.Add(0xFDCB34, new Instruction(p => SLL_aRRd_R(RegisterPair.IY, p, Register.H), "SLL (IY + d), H", 0xFDCB34, 1));

        _instructions.Add(0xFDCB35, new Instruction(p => SLL_aRRd_R(RegisterPair.IY, p, Register.L), "SLL (IY + d), L", 0xFDCB35, 1));

        _instructions.Add(0xFDCB36, new Instruction(p => SLL_aRRd(RegisterPair.IY, p), "SLL (IY + d)", 0xFDCB36, 1));

        _instructions.Add(0xFDCB37, new Instruction(p => SLL_aRRd_R(RegisterPair.IY, p, Register.A), "SLL (IY + d), A", 0xFDCB37, 1));

        _instructions.Add(0xFDCB38, new Instruction(p => SRL_aRRd_R(RegisterPair.IY, p, Register.B), "SRL (IY + d), B", 0xFDCB38, 1));

        _instructions.Add(0xFDCB39, new Instruction(p => SRL_aRRd_R(RegisterPair.IY, p, Register.C), "SRL (IY + d), C", 0xFDCB39, 1));

        _instructions.Add(0xFDCB3A, new Instruction(p => SRL_aRRd_R(RegisterPair.IY, p, Register.D), "SRL (IY + d), D", 0xFDCB3A, 1));

        _instructions.Add(0xFDCB3B, new Instruction(p => SRL_aRRd_R(RegisterPair.IY, p, Register.E), "SRL (IY + d), E", 0xFDCB3B, 1));

        _instructions.Add(0xFDCB3C, new Instruction(p => SRL_aRRd_R(RegisterPair.IY, p, Register.H), "SRL (IY + d), H", 0xFDCB3C, 1));

        _instructions.Add(0xFDCB3D, new Instruction(p => SRL_aRRd_R(RegisterPair.IY, p, Register.L), "SRL (IY + d), L", 0xFDCB3D, 1));

        _instructions.Add(0xFDCB3E, new Instruction(p => SRL_aRRd(RegisterPair.IY, p), "SRL (IY + d)", 0xFDCB3E, 1));

        _instructions.Add(0xFDCB3F, new Instruction(p => SRL_aRRd_R(RegisterPair.IY, p, Register.A), "SRL (IY + d), A", 0xFDCB3F, 1));

        _instructions.Add(0xFDCB40, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IY, p), "BIT 0, (IY + d)", 0xFDCB40, 1));

        _instructions.Add(0xFDCB41, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IY, p), "BIT 0, (IY + d)", 0xFDCB41, 1));

        _instructions.Add(0xFDCB42, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IY, p), "BIT 0, (IY + d)", 0xFDCB42, 1));

        _instructions.Add(0xFDCB43, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IY, p), "BIT 0, (IY + d)", 0xFDCB43, 1));

        _instructions.Add(0xFDCB44, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IY, p), "BIT 0, (IY + d)", 0xFDCB44, 1));

        _instructions.Add(0xFDCB45, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IY, p), "BIT 0, (IY + d)", 0xFDCB45, 1));

        _instructions.Add(0xFDCB46, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IY, p), "BIT 0, (IY + d)", 0xFDCB46, 1));

        _instructions.Add(0xFDCB47, new Instruction(p => BIT_b_aRRd(0x01, RegisterPair.IY, p), "BIT 0, (IY + d)", 0xFDCB47, 1));

        _instructions.Add(0xFDCB48, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IY, p), "BIT 1, (IY + d)", 0xFDCB48, 1));

        _instructions.Add(0xFDCB49, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IY, p), "BIT 1, (IY + d)", 0xFDCB49, 1));

        _instructions.Add(0xFDCB4A, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IY, p), "BIT 1, (IY + d)", 0xFDCB4A, 1));

        _instructions.Add(0xFDCB4B, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IY, p), "BIT 1, (IY + d)", 0xFDCB4B, 1));

        _instructions.Add(0xFDCB4C, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IY, p), "BIT 1, (IY + d)", 0xFDCB4C, 1));

        _instructions.Add(0xFDCB4D, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IY, p), "BIT 1, (IY + d)", 0xFDCB4D, 1));

        _instructions.Add(0xFDCB4E, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IY, p), "BIT 1, (IY + d)", 0xFDCB4E, 1));

        _instructions.Add(0xFDCB4F, new Instruction(p => BIT_b_aRRd(0x02, RegisterPair.IY, p), "BIT 1, (IY + d)", 0xFDCB4F, 1));

        _instructions.Add(0xFDCB50, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IY, p), "BIT 2, (IY + d)", 0xFDCB50, 1));

        _instructions.Add(0xFDCB51, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IY, p), "BIT 2, (IY + d)", 0xFDCB51, 1));

        _instructions.Add(0xFDCB52, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IY, p), "BIT 2, (IY + d)", 0xFDCB52, 1));

        _instructions.Add(0xFDCB53, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IY, p), "BIT 2, (IY + d)", 0xFDCB53, 1));

        _instructions.Add(0xFDCB54, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IY, p), "BIT 2, (IY + d)", 0xFDCB54, 1));

        _instructions.Add(0xFDCB55, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IY, p), "BIT 2, (IY + d)", 0xFDCB55, 1));

        _instructions.Add(0xFDCB56, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IY, p), "BIT 2, (IY + d)", 0xFDCB56, 1));

        _instructions.Add(0xFDCB57, new Instruction(p => BIT_b_aRRd(0x04, RegisterPair.IY, p), "BIT 2, (IY + d)", 0xFDCB57, 1));

        _instructions.Add(0xFDCB58, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IY, p), "BIT 3, (IY + d)", 0xFDCB58, 1));

        _instructions.Add(0xFDCB59, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IY, p), "BIT 3, (IY + d)", 0xFDCB59, 1));

        _instructions.Add(0xFDCB5A, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IY, p), "BIT 3, (IY + d)", 0xFDCB5A, 1));

        _instructions.Add(0xFDCB5B, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IY, p), "BIT 3, (IY + d)", 0xFDCB5B, 1));

        _instructions.Add(0xFDCB5C, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IY, p), "BIT 3, (IY + d)", 0xFDCB5C, 1));

        _instructions.Add(0xFDCB5D, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IY, p), "BIT 3, (IY + d)", 0xFDCB5D, 1));

        _instructions.Add(0xFDCB5E, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IY, p), "BIT 3, (IY + d)", 0xFDCB5E, 1));

        _instructions.Add(0xFDCB5F, new Instruction(p => BIT_b_aRRd(0x08, RegisterPair.IY, p), "BIT 3, (IY + d)", 0xFDCB5F, 1));

        _instructions.Add(0xFDCB60, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IY, p), "BIT 4, (IY + d)", 0xFDCB60, 1));

        _instructions.Add(0xFDCB61, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IY, p), "BIT 4, (IY + d)", 0xFDCB61, 1));

        _instructions.Add(0xFDCB62, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IY, p), "BIT 4, (IY + d)", 0xFDCB62, 1));

        _instructions.Add(0xFDCB63, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IY, p), "BIT 4, (IY + d)", 0xFDCB63, 1));

        _instructions.Add(0xFDCB64, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IY, p), "BIT 4, (IY + d)", 0xFDCB64, 1));

        _instructions.Add(0xFDCB65, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IY, p), "BIT 4, (IY + d)", 0xFDCB65, 1));

        _instructions.Add(0xFDCB66, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IY, p), "BIT 4, (IY + d)", 0xFDCB66, 1));

        _instructions.Add(0xFDCB67, new Instruction(p => BIT_b_aRRd(0x10, RegisterPair.IY, p), "BIT 4, (IY + d)", 0xFDCB67, 1));

        _instructions.Add(0xFDCB68, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IY, p), "BIT 5, (IY + d)", 0xFDCB68, 1));

        _instructions.Add(0xFDCB69, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IY, p), "BIT 5, (IY + d)", 0xFDCB69, 1));

        _instructions.Add(0xFDCB6A, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IY, p), "BIT 5, (IY + d)", 0xFDCB6A, 1));

        _instructions.Add(0xFDCB6B, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IY, p), "BIT 5, (IY + d)", 0xFDCB6B, 1));

        _instructions.Add(0xFDCB6C, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IY, p), "BIT 5, (IY + d)", 0xFDCB6C, 1));

        _instructions.Add(0xFDCB6D, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IY, p), "BIT 5, (IY + d)", 0xFDCB6D, 1));

        _instructions.Add(0xFDCB6E, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IY, p), "BIT 5, (IY + d)", 0xFDCB6E, 1));

        _instructions.Add(0xFDCB6F, new Instruction(p => BIT_b_aRRd(0x20, RegisterPair.IY, p), "BIT 5, (IY + d)", 0xFDCB6F, 1));

        _instructions.Add(0xFDCB70, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IY, p), "BIT 6, (IY + d)", 0xFDCB70, 1));

        _instructions.Add(0xFDCB71, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IY, p), "BIT 6, (IY + d)", 0xFDCB71, 1));

        _instructions.Add(0xFDCB72, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IY, p), "BIT 6, (IY + d)", 0xFDCB72, 1));

        _instructions.Add(0xFDCB73, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IY, p), "BIT 6, (IY + d)", 0xFDCB73, 1));

        _instructions.Add(0xFDCB74, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IY, p), "BIT 6, (IY + d)", 0xFDCB74, 1));

        _instructions.Add(0xFDCB75, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IY, p), "BIT 6, (IY + d)", 0xFDCB75, 1));

        _instructions.Add(0xFDCB76, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IY, p), "BIT 6, (IY + d)", 0xFDCB76, 1));

        _instructions.Add(0xFDCB77, new Instruction(p => BIT_b_aRRd(0x40, RegisterPair.IY, p), "BIT 6, (IY + d)", 0xFDCB77, 1));

        _instructions.Add(0xFDCB78, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IY, p), "BIT 7, (IY + d)", 0xFDCB78, 1));

        _instructions.Add(0xFDCB79, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IY, p), "BIT 7, (IY + d)", 0xFDCB79, 1));

        _instructions.Add(0xFDCB7A, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IY, p), "BIT 7, (IY + d)", 0xFDCB7A, 1));

        _instructions.Add(0xFDCB7B, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IY, p), "BIT 7, (IY + d)", 0xFDCB7B, 1));

        _instructions.Add(0xFDCB7C, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IY, p), "BIT 7, (IY + d)", 0xFDCB7C, 1));

        _instructions.Add(0xFDCB7D, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IY, p), "BIT 7, (IY + d)", 0xFDCB7D, 1));

        _instructions.Add(0xFDCB7E, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IY, p), "BIT 7, (IY + d)", 0xFDCB7E, 1));

        _instructions.Add(0xFDCB7F, new Instruction(p => BIT_b_aRRd(0x80, RegisterPair.IY, p), "BIT 7, (IY + d)", 0xFDCB7F, 1));

        _instructions.Add(0xFDCB80, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IY, p, Register.B), "RES 0, (IY + d), B", 0xFDCB80, 1));

        _instructions.Add(0xFDCB81, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IY, p, Register.C), "RES 0, (IY + d), C", 0xFDCB81, 1));

        _instructions.Add(0xFDCB82, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IY, p, Register.D), "RES 0, (IY + d), D", 0xFDCB82, 1));

        _instructions.Add(0xFDCB83, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IY, p, Register.E), "RES 0, (IY + d), E", 0xFDCB83, 1));

        _instructions.Add(0xFDCB84, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IY, p, Register.H), "RES 0, (IY + d), H", 0xFDCB84, 1));

        _instructions.Add(0xFDCB85, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IY, p, Register.L), "RES 0, (IY + d), L", 0xFDCB85, 1));

        _instructions.Add(0xFDCB86, new Instruction(p => RES_b_aRRd(0x01, RegisterPair.IY, p), "RES 0, (IY + d)", 0xFDCB86, 1));

        _instructions.Add(0xFDCB87, new Instruction(p => RES_b_aRRd_R(0x01, RegisterPair.IY, p, Register.A), "RES 0, (IY + d), A", 0xFDCB87, 1));

        _instructions.Add(0xFDCB88, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IY, p, Register.B), "RES 1, (IY + d), B", 0xFDCB88, 1));

        _instructions.Add(0xFDCB89, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IY, p, Register.C), "RES 1, (IY + d), C", 0xFDCB89, 1));

        _instructions.Add(0xFDCB8A, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IY, p, Register.D), "RES 1, (IY + d), D", 0xFDCB8A, 1));

        _instructions.Add(0xFDCB8B, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IY, p, Register.E), "RES 1, (IY + d), E", 0xFDCB8B, 1));

        _instructions.Add(0xFDCB8C, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IY, p, Register.H), "RES 1, (IY + d), H", 0xFDCB8C, 1));

        _instructions.Add(0xFDCB8D, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IY, p, Register.L), "RES 1, (IY + d), L", 0xFDCB8D, 1));

        _instructions.Add(0xFDCB8E, new Instruction(p => RES_b_aRRd(0x02, RegisterPair.IY, p), "RES 1, (IY + d)", 0xFDCB8E, 1));

        _instructions.Add(0xFDCB8F, new Instruction(p => RES_b_aRRd_R(0x02, RegisterPair.IY, p, Register.A), "RES 1, (IY + d), A", 0xFDCB8F, 1));

        _instructions.Add(0xFDCB90, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IY, p, Register.B), "RES 2, (IY + d), B", 0xFDCB90, 1));

        _instructions.Add(0xFDCB91, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IY, p, Register.C), "RES 2, (IY + d), C", 0xFDCB91, 1));

        _instructions.Add(0xFDCB92, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IY, p, Register.D), "RES 2, (IY + d), D", 0xFDCB92, 1));

        _instructions.Add(0xFDCB93, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IY, p, Register.E), "RES 2, (IY + d), E", 0xFDCB93, 1));

        _instructions.Add(0xFDCB94, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IY, p, Register.H), "RES 2, (IY + d), H", 0xFDCB94, 1));

        _instructions.Add(0xFDCB95, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IY, p, Register.L), "RES 2, (IY + d), L", 0xFDCB95, 1));

        _instructions.Add(0xFDCB96, new Instruction(p => RES_b_aRRd(0x04, RegisterPair.IY, p), "RES 2, (IY + d)", 0xFDCB96, 1));

        _instructions.Add(0xFDCB97, new Instruction(p => RES_b_aRRd_R(0x04, RegisterPair.IY, p, Register.A), "RES 2, (IY + d), A", 0xFDCB97, 1));

        _instructions.Add(0xFDCB98, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IY, p, Register.B), "RES 3, (IY + d), B", 0xFDCB98, 1));

        _instructions.Add(0xFDCB99, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IY, p, Register.C), "RES 3, (IY + d), C", 0xFDCB99, 1));

        _instructions.Add(0xFDCB9A, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IY, p, Register.D), "RES 3, (IY + d), D", 0xFDCB9A, 1));

        _instructions.Add(0xFDCB9B, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IY, p, Register.E), "RES 3, (IY + d), E", 0xFDCB9B, 1));

        _instructions.Add(0xFDCB9C, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IY, p, Register.H), "RES 3, (IY + d), H", 0xFDCB9C, 1));

        _instructions.Add(0xFDCB9D, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IY, p, Register.L), "RES 3, (IY + d), L", 0xFDCB9D, 1));

        _instructions.Add(0xFDCB9E, new Instruction(p => RES_b_aRRd(0x08, RegisterPair.IY, p), "RES 3, (IY + d)", 0xFDCB9E, 1));

        _instructions.Add(0xFDCB9F, new Instruction(p => RES_b_aRRd_R(0x08, RegisterPair.IY, p, Register.A), "RES 3, (IY + d), A", 0xFDCB9F, 1));

        _instructions.Add(0xFDCBA0, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IY, p, Register.B), "RES 4, (IY + d), B", 0xFDCBA0, 1));

        _instructions.Add(0xFDCBA1, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IY, p, Register.C), "RES 4, (IY + d), C", 0xFDCBA1, 1));

        _instructions.Add(0xFDCBA2, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IY, p, Register.D), "RES 4, (IY + d), D", 0xFDCBA2, 1));

        _instructions.Add(0xFDCBA3, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IY, p, Register.E), "RES 4, (IY + d), E", 0xFDCBA3, 1));

        _instructions.Add(0xFDCBA4, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IY, p, Register.H), "RES 4, (IY + d), H", 0xFDCBA4, 1));

        _instructions.Add(0xFDCBA5, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IY, p, Register.L), "RES 4, (IY + d), L", 0xFDCBA5, 1));

        _instructions.Add(0xFDCBA6, new Instruction(p => RES_b_aRRd(0x10, RegisterPair.IY, p), "RES 4, (IY + d)", 0xFDCBA6, 1));

        _instructions.Add(0xFDCBA7, new Instruction(p => RES_b_aRRd_R(0x10, RegisterPair.IY, p, Register.A), "RES 4, (IY + d), A", 0xFDCBA7, 1));

        _instructions.Add(0xFDCBA8, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IY, p, Register.B), "RES 5, (IY + d), B", 0xFDCBA8, 1));

        _instructions.Add(0xFDCBA9, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IY, p, Register.C), "RES 5, (IY + d), C", 0xFDCBA9, 1));

        _instructions.Add(0xFDCBAA, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IY, p, Register.D), "RES 5, (IY + d), D", 0xFDCBAA, 1));

        _instructions.Add(0xFDCBAB, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IY, p, Register.E), "RES 5, (IY + d), E", 0xFDCBAB, 1));

        _instructions.Add(0xFDCBAC, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IY, p, Register.H), "RES 5, (IY + d), H", 0xFDCBAC, 1));

        _instructions.Add(0xFDCBAD, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IY, p, Register.L), "RES 5, (IY + d), L", 0xFDCBAD, 1));

        _instructions.Add(0xFDCBAE, new Instruction(p => RES_b_aRRd(0x20, RegisterPair.IY, p), "RES 5, (IY + d)", 0xFDCBAE, 1));

        _instructions.Add(0xFDCBAF, new Instruction(p => RES_b_aRRd_R(0x20, RegisterPair.IY, p, Register.A), "RES 5, (IY + d), A", 0xFDCBAF, 1));

        _instructions.Add(0xFDCBB0, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IY, p, Register.B), "RES 6, (IY + d), B", 0xFDCBB0, 1));

        _instructions.Add(0xFDCBB1, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IY, p, Register.C), "RES 6, (IY + d), C", 0xFDCBB1, 1));

        _instructions.Add(0xFDCBB2, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IY, p, Register.D), "RES 6, (IY + d), D", 0xFDCBB2, 1));

        _instructions.Add(0xFDCBB3, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IY, p, Register.E), "RES 6, (IY + d), E", 0xFDCBB3, 1));

        _instructions.Add(0xFDCBB4, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IY, p, Register.H), "RES 6, (IY + d), H", 0xFDCBB4, 1));

        _instructions.Add(0xFDCBB5, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IY, p, Register.L), "RES 6, (IY + d), L", 0xFDCBB5, 1));

        _instructions.Add(0xFDCBB6, new Instruction(p => RES_b_aRRd(0x40, RegisterPair.IY, p), "RES 6, (IY + d)", 0xFDCBB6, 1));

        _instructions.Add(0xFDCBB7, new Instruction(p => RES_b_aRRd_R(0x40, RegisterPair.IY, p, Register.A), "RES 6, (IY + d), A", 0xFDCBB7, 1));

        _instructions.Add(0xFDCBB8, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IY, p, Register.B), "RES 7, (IY + d), B", 0xFDCBB8, 1));

        _instructions.Add(0xFDCBB9, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IY, p, Register.C), "RES 7, (IY + d), C", 0xFDCBB9, 1));

        _instructions.Add(0xFDCBBA, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IY, p, Register.D), "RES 7, (IY + d), D", 0xFDCBBA, 1));

        _instructions.Add(0xFDCBBB, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IY, p, Register.E), "RES 7, (IY + d), E", 0xFDCBBB, 1));

        _instructions.Add(0xFDCBBC, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IY, p, Register.H), "RES 7, (IY + d), H", 0xFDCBBC, 1));

        _instructions.Add(0xFDCBBD, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IY, p, Register.L), "RES 7, (IY + d), L", 0xFDCBBD, 1));

        _instructions.Add(0xFDCBBE, new Instruction(p => RES_b_aRRd(0x80, RegisterPair.IY, p), "RES 7, (IY + d)", 0xFDCBBE, 1));

        _instructions.Add(0xFDCBBF, new Instruction(p => RES_b_aRRd_R(0x80, RegisterPair.IY, p, Register.A), "RES 7, (IY + d), A", 0xFDCBBF, 1));

        _instructions.Add(0xFDCBC0, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IY, p, Register.B), "SET 0, (IY + d), B", 0xFDCBC0, 1));

        _instructions.Add(0xFDCBC1, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IY, p, Register.C), "SET 0, (IY + d), C", 0xFDCBC1, 1));

        _instructions.Add(0xFDCBC2, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IY, p, Register.D), "SET 0, (IY + d), D", 0xFDCBC2, 1));

        _instructions.Add(0xFDCBC3, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IY, p, Register.E), "SET 0, (IY + d), E", 0xFDCBC3, 1));

        _instructions.Add(0xFDCBC4, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IY, p, Register.H), "SET 0, (IY + d), H", 0xFDCBC4, 1));

        _instructions.Add(0xFDCBC5, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IY, p, Register.L), "SET 0, (IY + d), L", 0xFDCBC5, 1));

        _instructions.Add(0xFDCBC6, new Instruction(p => SET_b_aRRd(0x01, RegisterPair.IY, p), "SET 0, (IY + d)", 0xFDCBC6, 1));

        _instructions.Add(0xFDCBC7, new Instruction(p => SET_b_aRRd_R(0x01, RegisterPair.IY, p, Register.A), "SET 0, (IY + d), A", 0xFDCBC7, 1));

        _instructions.Add(0xFDCBC8, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IY, p, Register.B), "SET 1, (IY + d), B", 0xFDCBC8, 1));

        _instructions.Add(0xFDCBC9, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IY, p, Register.C), "SET 1, (IY + d), C", 0xFDCBC9, 1));

        _instructions.Add(0xFDCBCA, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IY, p, Register.D), "SET 1, (IY + d), D", 0xFDCBCA, 1));

        _instructions.Add(0xFDCBCB, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IY, p, Register.E), "SET 1, (IY + d), E", 0xFDCBCB, 1));

        _instructions.Add(0xFDCBCC, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IY, p, Register.H), "SET 1, (IY + d), H", 0xFDCBCC, 1));

        _instructions.Add(0xFDCBCD, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IY, p, Register.L), "SET 1, (IY + d), L", 0xFDCBCD, 1));

        _instructions.Add(0xFDCBCE, new Instruction(p => SET_b_aRRd(0x02, RegisterPair.IY, p), "SET 1, (IY + d)", 0xFDCBCE, 1));

        _instructions.Add(0xFDCBCF, new Instruction(p => SET_b_aRRd_R(0x02, RegisterPair.IY, p, Register.A), "SET 1, (IY + d), A", 0xFDCBCF, 1));

        _instructions.Add(0xFDCBD0, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IY, p, Register.B), "SET 2, (IY + d), B", 0xFDCBD0, 1));

        _instructions.Add(0xFDCBD1, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IY, p, Register.C), "SET 2, (IY + d), C", 0xFDCBD1, 1));

        _instructions.Add(0xFDCBD2, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IY, p, Register.D), "SET 2, (IY + d), D", 0xFDCBD2, 1));

        _instructions.Add(0xFDCBD3, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IY, p, Register.E), "SET 2, (IY + d), E", 0xFDCBD3, 1));

        _instructions.Add(0xFDCBD4, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IY, p, Register.H), "SET 2, (IY + d), H", 0xFDCBD4, 1));

        _instructions.Add(0xFDCBD5, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IY, p, Register.L), "SET 2, (IY + d), L", 0xFDCBD5, 1));

        _instructions.Add(0xFDCBD6, new Instruction(p => SET_b_aRRd(0x04, RegisterPair.IY, p), "SET 2, (IY + d)", 0xFDCBD6, 1));

        _instructions.Add(0xFDCBD7, new Instruction(p => SET_b_aRRd_R(0x04, RegisterPair.IY, p, Register.A), "SET 2, (IY + d), A", 0xFDCBD7, 1));

        _instructions.Add(0xFDCBD8, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IY, p, Register.B), "SET 3, (IY + d), B", 0xFDCBD8, 1));

        _instructions.Add(0xFDCBD9, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IY, p, Register.C), "SET 3, (IY + d), C", 0xFDCBD9, 1));

        _instructions.Add(0xFDCBDA, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IY, p, Register.D), "SET 3, (IY + d), D", 0xFDCBDA, 1));

        _instructions.Add(0xFDCBDB, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IY, p, Register.E), "SET 3, (IY + d), E", 0xFDCBDB, 1));

        _instructions.Add(0xFDCBDC, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IY, p, Register.H), "SET 3, (IY + d), H", 0xFDCBDC, 1));

        _instructions.Add(0xFDCBDD, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IY, p, Register.L), "SET 3, (IY + d), L", 0xFDCBDD, 1));

        _instructions.Add(0xFDCBDE, new Instruction(p => SET_b_aRRd(0x08, RegisterPair.IY, p), "SET 3, (IY + d)", 0xFDCBDE, 1));

        _instructions.Add(0xFDCBDF, new Instruction(p => SET_b_aRRd_R(0x08, RegisterPair.IY, p, Register.A), "SET 3, (IY + d), A", 0xFDCBDF, 1));

        _instructions.Add(0xFDCBE0, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IY, p, Register.B), "SET 4, (IY + d), B", 0xFDCBE0, 1));

        _instructions.Add(0xFDCBE1, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IY, p, Register.C), "SET 4, (IY + d), C", 0xFDCBE1, 1));

        _instructions.Add(0xFDCBE2, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IY, p, Register.D), "SET 4, (IY + d), D", 0xFDCBE2, 1));

        _instructions.Add(0xFDCBE3, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IY, p, Register.E), "SET 4, (IY + d), E", 0xFDCBE3, 1));

        _instructions.Add(0xFDCBE4, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IY, p, Register.H), "SET 4, (IY + d), H", 0xFDCBE4, 1));

        _instructions.Add(0xFDCBE5, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IY, p, Register.L), "SET 4, (IY + d), L", 0xFDCBE5, 1));

        _instructions.Add(0xFDCBE6, new Instruction(p => SET_b_aRRd(0x10, RegisterPair.IY, p), "SET 4, (IY + d)", 0xFDCBE6, 1));

        _instructions.Add(0xFDCBE7, new Instruction(p => SET_b_aRRd_R(0x10, RegisterPair.IY, p, Register.A), "SET 4, (IY + d), A", 0xFDCBE7, 1));

        _instructions.Add(0xFDCBE8, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IY, p, Register.B), "SET 5, (IY + d), B", 0xFDCBE8, 1));

        _instructions.Add(0xFDCBE9, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IY, p, Register.C), "SET 5, (IY + d), C", 0xFDCBE9, 1));

        _instructions.Add(0xFDCBEA, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IY, p, Register.D), "SET 5, (IY + d), D", 0xFDCBEA, 1));

        _instructions.Add(0xFDCBEB, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IY, p, Register.E), "SET 5, (IY + d), E", 0xFDCBEB, 1));

        _instructions.Add(0xFDCBEC, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IY, p, Register.H), "SET 5, (IY + d), H", 0xFDCBEC, 1));

        _instructions.Add(0xFDCBED, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IY, p, Register.L), "SET 5, (IY + d), L", 0xFDCBED, 1));

        _instructions.Add(0xFDCBEE, new Instruction(p => SET_b_aRRd(0x20, RegisterPair.IY, p), "SET 5, (IY + d)", 0xFDCBEE, 1));

        _instructions.Add(0xFDCBEF, new Instruction(p => SET_b_aRRd_R(0x20, RegisterPair.IY, p, Register.A), "SET 5, (IY + d), A", 0xFDCBEF, 1));

        _instructions.Add(0xFDCBF0, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IY, p, Register.B), "SET 6, (IY + d), B", 0xFDCBF0, 1));

        _instructions.Add(0xFDCBF1, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IY, p, Register.C), "SET 6, (IY + d), C", 0xFDCBF1, 1));

        _instructions.Add(0xFDCBF2, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IY, p, Register.D), "SET 6, (IY + d), D", 0xFDCBF2, 1));

        _instructions.Add(0xFDCBF3, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IY, p, Register.E), "SET 6, (IY + d), E", 0xFDCBF3, 1));

        _instructions.Add(0xFDCBF4, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IY, p, Register.H), "SET 6, (IY + d), H", 0xFDCBF4, 1));

        _instructions.Add(0xFDCBF5, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IY, p, Register.L), "SET 6, (IY + d), L", 0xFDCBF5, 1));

        _instructions.Add(0xFDCBF6, new Instruction(p => SET_b_aRRd(0x40, RegisterPair.IY, p), "SET 6, (IY + d)", 0xFDCBF6, 1));

        _instructions.Add(0xFDCBF7, new Instruction(p => SET_b_aRRd_R(0x40, RegisterPair.IY, p, Register.A), "SET 6, (IY + d), A", 0xFDCBF7, 1));

        _instructions.Add(0xFDCBF8, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IY, p, Register.B), "SET 7, (IY + d), B", 0xFDCBF8, 1));

        _instructions.Add(0xFDCBF9, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IY, p, Register.C), "SET 7, (IY + d), C", 0xFDCBF9, 1));

        _instructions.Add(0xFDCBFA, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IY, p, Register.D), "SET 7, (IY + d), D", 0xFDCBFA, 1));

        _instructions.Add(0xFDCBFB, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IY, p, Register.E), "SET 7, (IY + d), E", 0xFDCBFB, 1));

        _instructions.Add(0xFDCBFC, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IY, p, Register.H), "SET 7, (IY + d), H", 0xFDCBFC, 1));

        _instructions.Add(0xFDCBFD, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IY, p, Register.L), "SET 7, (IY + d), L", 0xFDCBFD, 1));

        _instructions.Add(0xFDCBFE, new Instruction(p => SET_b_aRRd(0x80, RegisterPair.IY, p), "SET 7, (IY + d)", 0xFDCBFE, 1));

        _instructions.Add(0xFDCBFF, new Instruction(p => SET_b_aRRd_R(0x80, RegisterPair.IY, p, Register.A), "SET 7, (IY + d), A", 0xFDCBFF, 1));
    }
}