// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Z80.Processor;

#pragma warning disable CS8509

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseCBInstructions()
    {
        InitialiseCBBitResSetInstructions();

        InitialiseCBBitShiftInstructions();
    }

    private void InitialiseCBBitResSetInstructions()
    {
        for (var bit = 0; bit < 8; bit++)
        {
            var bitMask = (byte) (1 << bit);

            for (var registerNumber = 0; registerNumber < 8; registerNumber++)
            {
                var opCode = 0xCB40 + bit * 8 + registerNumber;

                if (registerNumber != 6)
                {
                    var register = registerNumber switch
                    {
                        0 => Register.B,
                        1 => Register.C,
                        2 => Register.D,
                        3 => Register.E,
                        4 => Register.H,
                        5 => Register.L,
                        7 => Register.A
                    };

                    _instructions.Add(opCode, new Instruction(_ => BIT_b_R(bitMask, register), $"BIT {bit}, {register}", opCode, 0));

                    _instructions.Add(opCode + 0x40, new Instruction(_ => RES_b_R(bitMask, register), $"RES {bit}, {register}", opCode + 0x40, 0));

                    _instructions.Add(opCode + 0x80, new Instruction(_ => SET_b_R(bitMask, register), $"SET {bit}, {register}", opCode + 0x80, 0));
                }
                else
                {
                    _instructions.Add(opCode, new Instruction(_ => BIT_b_aRR(bitMask, RegisterPair.HL), $"BIT {bit}, (HL)", opCode, 0));

                    _instructions.Add(opCode + 0x40, new Instruction(_ => RES_b_aRR(bitMask, RegisterPair.HL), $"RES {bit}, (HL)", opCode + 0x40, 0));

                    _instructions.Add(opCode + 0x80, new Instruction(_ => SET_b_aRR(bitMask, RegisterPair.HL), $"SET {bit}, (HL)", opCode + 0x80, 0));
                }
            }
        }
    }

    private void InitialiseCBBitShiftInstructions()
    {
        for (var registerNumber = 0; registerNumber < 8; registerNumber++)
        {
            var opCode = 0xCB00 + registerNumber;

            if (registerNumber != 6)
            {
                var register = registerNumber switch
                {
                    0 => Register.B,
                    1 => Register.C,
                    2 => Register.D,
                    3 => Register.E,
                    4 => Register.H,
                    5 => Register.L,
                    7 => Register.A
                };

                _instructions.Add(opCode, new Instruction(_ => RLC_R(register), $"RLC {register}", opCode, 0));

                _instructions.Add(opCode + 0x08, new Instruction(_ => RRC_R(register), $"RRC {register}", opCode + 0x08, 0));

                _instructions.Add(opCode + 0x10, new Instruction(_ => RL_R(register), $"RL {register}", opCode + 0x10, 0));

                _instructions.Add(opCode + 0x18, new Instruction(_ => RR_R(register), $"RR {register}", opCode + 0x18, 0));

                _instructions.Add(opCode + 0x20, new Instruction(_ => SLA_R(register), $"SLA {register}", opCode + 0x20, 0));

                _instructions.Add(opCode + 0x28, new Instruction(_ => SRA_R(register), $"SRA {register}", opCode + 0x28, 0));

                _instructions.Add(opCode + 0x30, new Instruction(_ => SLL_R(register), $"SLL {register}", opCode + 0x30, 0));
            }
            else
            {
                _instructions.Add(opCode, new Instruction(_ => RLC_aRR(RegisterPair.HL), "RLC (HL)", opCode, 0));

                _instructions.Add(opCode + 0x08, new Instruction(_ => RRC_aRR(RegisterPair.HL), "RRC (HL)", opCode + 0x08, 0));

                _instructions.Add(opCode + 0x10, new Instruction(_ => RL_aRR(RegisterPair.HL), "RL (HL)", opCode + 0x10, 0));

                _instructions.Add(opCode + 0x18, new Instruction(_ => RR_aRR(RegisterPair.HL), "RR (HL)", opCode + 0x18, 0));

                _instructions.Add(opCode + 0x20, new Instruction(_ => SLA_aRR(RegisterPair.HL), "SLA (HL)", opCode + 0x20, 0));

                _instructions.Add(opCode + 0x28, new Instruction(_ => SRA_aRR(RegisterPair.HL), "SRA (HL)", opCode + 0x28, 0));

                _instructions.Add(opCode + 0x30, new Instruction(_ => SLL_aRR(RegisterPair.HL), "SLL (HL)", opCode + 0x30, 0));
            }
        }
    }
}