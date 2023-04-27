// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

#pragma warning disable CS8509

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseCBInstructions()
    {
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
                _instructions.Add(0xCB00 + registerNumber, new Instruction(_ => RLC_R((Register) register), $"RLC {register}", 0xCB00 + registerNumber, 0));
            }
            else
            {
                _instructions.Add(0xCB00 + registerNumber, new Instruction(_ => RLC_aRR(RegisterPair.HL), "RLC (HL)", 0xCB00 + registerNumber, 0));
            }

            //_instructions.Add(0xCB08 + registerNumber, new Instruction(d => RRC_R(registerPair, d, register), $"RRC ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x08 + registerNumber, 0));

            //_instructions.Add(0xCB10 + registerNumber, new Instruction(d => RL_aRRd_R(registerPair, d, register), $"RL ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x10 + registerNumber, 0));

            //_instructions.Add(0xCB18 + registerNumber, new Instruction(d => RR_aRRd_R(registerPair, d, register), $"RR ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x18 + registerNumber, 0));

            //_instructions.Add(0xCB20 + registerNumber, new Instruction(d => SLA_aRRd_R(registerPair, d, register), $"SLA ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x20 + registerNumber, 0));

            //_instructions.Add(0xCB28 + registerNumber, new Instruction(d => SRA_aRRd_R(registerPair, d, register), $"SRA ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x28 + registerNumber, 0));

            //_instructions.Add(0xCB30 + registerNumber, new Instruction(d => SLL_aRRd_R(registerPair, d, register), $"SLL ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x30 + registerNumber, 0));

            //_instructions.Add(0xCB38 + registerNumber, new Instruction(d => SRL_aRRd_R(registerPair, d, register), $"SRL ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x38 + registerNumber, 0));

            for (var bit = 0; bit < 8; bit++)
            {
                var innerBit = bit;

                //_instructions.Add(0xCB40 + registerNumber + bit * 8, new Instruction(d => BIT_b_aRRd((byte) (1 << innerBit), registerPair, d), $"BIT {bit}, ({registerPair} + d)", baseOpcode + 0x40 + registerNumber + bit * 8, 0));

                //_instructions.Add(0xCB80 + registerNumber + bit * 8, new Instruction(d => RES_b_aRRd_R((byte) (1 << innerBit), registerPair, d, register), $"RES {bit}, ({registerPair} + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0x80 + registerNumber + bit * 8, 0));

                //_instructions.Add(0xCBC0 + registerNumber + bit * 8, new Instruction(d => SET_b_aRRd_R((byte) (1 << innerBit), registerPair, d, register), $"SET {bit}, (I{registerPair}X + d){(register == null ? string.Empty : $", {register}")}", baseOpcode + 0xC0 + registerNumber + bit * 8, 0));
            }
        }
    }
}