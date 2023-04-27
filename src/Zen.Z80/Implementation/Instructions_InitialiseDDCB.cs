﻿// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDCBInstructions()
    {
        for (var registerNumber = 0; registerNumber < 8; registerNumber++)
        {
#pragma warning disable CS8509
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
#pragma warning restore CS8509

            _instructions.Add(0xDDCB00 + registerNumber, new Instruction(d => RLC_aRRd_R(RegisterPair.IX, d, register), $"RLC (IX + d){(register == null ? string.Empty : $", {register}")}", 0xDDCB00 + registerNumber, 0));

            _instructions.Add(0xDDCB08 + registerNumber, new Instruction(d => RRC_aRRd_R(RegisterPair.IX, d, register), $"RRC (IX + d){(register == null ? string.Empty : $", {register}")}", 0xDDCB08 + registerNumber, 0));

            _instructions.Add(0xDDCB10 + registerNumber, new Instruction(d => RL_aRRd_R(RegisterPair.IX, d, register), $"RL (IX + d){(register == null ? string.Empty : $", {register}")}", 0xDDCB10 + registerNumber, 0));
        }

        for (var bit = 0; bit < 8; bit++)
        {
            var innerBit = bit;

            for (var registerNumber = 0; registerNumber < 8; registerNumber++)
            {
#pragma warning disable CS8509
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
#pragma warning restore CS8509

                _instructions.Add(0xDDCB40 + registerNumber + bit * 8, new Instruction(d => BIT_b_aRRd((byte) (1 << innerBit), RegisterPair.IX, d), $"BIT {bit}, (IX + d)", 0xDDCB40 + registerNumber + bit * 8, 0));

                _instructions.Add(0xDDCB80 + registerNumber + bit * 8, new Instruction(d => RES_b_aRRd_R((byte) (1 << innerBit), RegisterPair.IX, d, register), $"RES {bit}, (IX + d){(register == null ? string.Empty : $", {register}")}", 0xDDCB80 + registerNumber + bit * 8, 0));

                _instructions.Add(0xDDCBC0 + registerNumber + bit * 8, new Instruction(d => SET_b_aRRd_R((byte) (1 << innerBit), RegisterPair.IX, d, register), $"SET {bit}, (IX + d){(register == null ? string.Empty : $", {register}")}", 0xDDCBC0 + registerNumber + bit * 8, 0));
            }
        }
    }
}