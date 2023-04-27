// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Z80.Processor;

#pragma warning disable CS8509

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDCBFDCBInstructions()
    {
        for (var rp = 0; rp < 2; rp++)
        {
            var registerPair = rp == 0 ? RegisterPair.IX : RegisterPair.IY;

            var baseOpcode = rp == 0 ? 0xDDCB00 : 0xFDCB00;

            for (var registerNumber = 0; registerNumber < 8; registerNumber++)
            {
                var register = registerNumber switch
                {
                    0 => Register.B,
                    1 => Register.C,
                    2 => Register.D,
                    3 => Register.E,
                    4 => Register.H,
                    5 => Register.L,
                    6 => (Register?) null,
                    7 => Register.A
                };

                if (register != null)
                {
                    _instructions.Add(baseOpcode + registerNumber, new Instruction(d => RLC_aRRd_R(registerPair, d, (Register) register), $"RLC ({registerPair} + d), {register}", baseOpcode + registerNumber, 0));

                    _instructions.Add(baseOpcode + 0x08 + registerNumber, new Instruction(d => RRC_aRRd_R(registerPair, d, register), $"RRC ({registerPair} + d), {register}", baseOpcode + 0x08 + registerNumber, 0));

                    _instructions.Add(baseOpcode + 0x10 + registerNumber, new Instruction(d => RL_aRRd_R(registerPair, d, register), $"RL ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x10 + registerNumber, 0));

                    _instructions.Add(baseOpcode + 0x18 + registerNumber, new Instruction(d => RR_aRRd_R(registerPair, d, register), $"RR ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x18 + registerNumber, 0));

                    _instructions.Add(baseOpcode + 0x20 + registerNumber, new Instruction(d => SLA_aRRd_R(registerPair, d, register), $"SLA ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x20 + registerNumber, 0));

                    _instructions.Add(baseOpcode + 0x28 + registerNumber, new Instruction(d => SRA_aRRd_R(registerPair, d, register), $"SRA ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x28 + registerNumber, 0));

                    _instructions.Add(baseOpcode + 0x30 + registerNumber, new Instruction(d => SLL_aRRd_R(registerPair, d, register), $"SLL ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x30 + registerNumber, 0));

                    _instructions.Add(baseOpcode + 0x38 + registerNumber, new Instruction(d => SRL_aRRd_R(registerPair, d, register), $"SRL ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x38 + registerNumber, 0));
                }

                for (var bit = 0; bit < 8; bit++)
                {
                    var innerBit = bit;

                    _instructions.Add(baseOpcode + 0x40 + registerNumber + bit * 8, new Instruction(d => BIT_b_aRRd((byte) (1 << innerBit), registerPair, d), $"BIT {bit}, ({registerPair} + d)", baseOpcode + 0x40 + registerNumber + bit * 8, 0));

                    _instructions.Add(baseOpcode + 0x80 + registerNumber + bit * 8, new Instruction(d => RES_b_aRRd_R((byte) (1 << innerBit), registerPair, d, register), $"RES {bit}, ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x80 + registerNumber + bit * 8, 0));

                    _instructions.Add(baseOpcode + 0xC0 + registerNumber + bit * 8, new Instruction(d => SET_b_aRRd_R((byte) (1 << innerBit), registerPair, d, register), $"SET {bit}, (I{registerPair}X + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0xC0 + registerNumber + bit * 8, 0));
                }
            }
        }
    }
}